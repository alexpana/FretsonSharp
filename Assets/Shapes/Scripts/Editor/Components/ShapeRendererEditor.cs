using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes
{
    //[CustomEditor( typeof( ShapeRenderer ) )]
    [CanEditMultipleObjects]
    public class ShapeRendererEditor : Editor
    {
        private const string colorCh = "RGBA";
        private const float COLOR_BTN_SAT = 0.5f;

        private static bool showDepth = false;
        private static bool showStencil = false;

        private static readonly GUIContent blendModeGuiContent = new(
            "Blend Mode",
            "[Opaque] does not support partial transparency, " +
            "but will write to the depth buffer and sort correctly. " +
            "For best results, use MSAA in your project to avoid aliasing " +
            "(note that it may still be aliased in the scene view)\n" +
            "\n" +
            "[Transparent] supports partial transparency, " +
            "but may not sort properly in some cases.\n" +
            "\n" +
            "[Additive] is good for glowing/brightening effects against dark backgrounds\n" +
            "\n" +
            "[Multiplicative] is good for tinting/darkening effects against bright backgrounds"
        );

        private static readonly GUIContent scaleModeGuiContent = new(
            "Scale Mode",
            "[Uniform] mode means thickness will also scale with the transform, regardless of thickness space settings\n\n" +
            "[Coordinate] mode means thickness values will remain the same even when scaling"
        );

        private static readonly GUIContent renderQueueGuiContent = new(
            "Render Queue",
            "The render queue of this object. Default is -1, which means it will use the queue from the shader.\n\n" +
            "[Opaque] = 2450 (AlphaTest)\n" +
            "[All Other Blend Modes] = 3000 (Transparent)"
        );

        private static readonly GUIContent zTestGuiContent = new(
            "Depth Test",
            "How this shape should render depending on the contents of the depth buffer. Note: anything other than [Less Equal] will not use GPU instancing\n\n" +
            "[Less Equal] means it will not render when behind something (default)\n\n" +
            "[Always] will completely ignore the depth buffer, drawing on top of everything else"
        );

        private static readonly GUIContent zOffsetFactorGuiContent = new(
            "Depth Offset Factor",
            "Depth buffer offset factor, taking the slope into account (default is 0)\n\n" +
            "Practically, this is mostly used to be able to have two things on the same plane, but have one still render on top of the other without Z-fighting/flickering.\n" +
            "Setting this to, say, -1, will make this render on top of things in the same plane, setting this to 1 will make it render behind things on the same plane"
        );

        private static readonly GUIContent zOffsetUnitsGuiContent = new(
            "Depth Offset Units",
            "Depth buffer offset, not taking the slope into account (default is 0)\n\n" +
            "I've never found much use of this one, seems like a bad version of Z offset factor? It's mostly here for completeness I guess"
        );

        private static readonly GUIContent colorMaskGuiContent = new(
            "Color Mask",
            "The color channels to render to (default is RGBA)\n\n" +
            "This is useful when you want to specifically exclude writing to alpha, or, when you want it to not render color at all, and only render to stencil"
        );

        private static readonly GUIContent stencilCompGuiContent = new("Compare", "Stencil compare function");
        private static readonly GUIContent stencilOpPassGuiContent = new("Pass", "Stencil Op Pass");
        private static readonly GUIContent stencilIDGuiContent = new("Ref", "Stencil reference ID");

        private static readonly GUIContent stencilReadMaskGuiContent =
            new("Read Mask", "Bitmask for reading stencil values");

        private static readonly GUIContent stencilWriteMaskGuiContent =
            new("Write Mask", "Bitmask for writing stencil values");

        private static readonly Color[] channelColors =
        {
            Color.HSVToRGB(0, COLOR_BTN_SAT, 1),
            Color.HSVToRGB(1f / 3f, COLOR_BTN_SAT, 1),
            Color.HSVToRGB(2f / 3f, COLOR_BTN_SAT, 1),
            Color.HSVToRGB(0, 0, 1)
        };

        private static readonly Color channelUnsetColor = new(0.5f, 0.5f, 0.5f, 1f);
        private readonly SerializedProperty propBlendMode = null;

        // ShapeRenderer
        protected SerializedProperty propColor;
        private readonly SerializedProperty propColorMask = null;
        private readonly SerializedProperty propDetailLevel = null;
        private readonly SerializedProperty propRenderQueue = null;
        private readonly SerializedProperty propScaleMode = null;
        private SerializedProperty propSortingLayer;
        private SerializedProperty propSortingOrder;
        private readonly SerializedProperty propStencilComp = null;
        private readonly SerializedProperty propStencilOpPass = null;
        private readonly SerializedProperty propStencilReadMask = null;
        private readonly SerializedProperty propStencilRefID = null;
        private readonly SerializedProperty propStencilWriteMask = null;
        private readonly SerializedProperty propZOffsetFactor = null;
        private readonly SerializedProperty propZOffsetUnits = null;
        private readonly SerializedProperty propZTest = null;

        // MeshRenderer
        private SerializedObject soRnd;

        private bool updateMeshFromEditorChange = false;

        public virtual void OnEnable()
        {
            soRnd = new SerializedObject(targets.Select(t => ((Component)t).GetComponent<MeshRenderer>() as Object)
                .ToArray());
            propSortingOrder = soRnd.FindProperty("m_SortingOrder");
            propSortingLayer = soRnd.FindProperty("m_SortingLayerID");

            // will assign all null properties, even in derived types
            FindAllProperties();

            // hide mesh filter/renderer components
            foreach (var shape in targets.Cast<ShapeRenderer>())
                shape.HideMeshFilterRenderer();
        }

        public bool HasFrameBounds()
        {
            return true;
        }

        public Bounds OnGetFrameBounds()
        {
            if (serializedObject.isEditingMultipleObjects)
            {
                // this only works for multiselecting shapes of the same type
                // todo: might be able to make a solution using Editor.CreateEditor shenanigans
                var bounds = ((ShapeRenderer)serializedObject.targetObjects[0]).GetWorldBounds();
                for (var i = 1; i < serializedObject.targetObjects.Length; i++)
                    bounds.Encapsulate(((ShapeRenderer)serializedObject.targetObjects[i]).GetWorldBounds());
                return bounds;
            }

            return ((ShapeRenderer)target).GetWorldBounds();
        }

        private void FindAllProperties()
        {
            IEnumerable<FieldInfo> GetFields(Type type)
            {
                return type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                    .Where(x => x.FieldType == typeof(SerializedProperty) && x.Name.StartsWith("m_") == false &&
                                x.GetValue(this) == null);
            }

            var fieldsBase = GetFields(GetType().BaseType);
            var fieldsInherited = GetFields(GetType());

            foreach (var field in fieldsBase.Concat(fieldsInherited))
            {
                var fieldName = char.ToLowerInvariant(field.Name[4]) + field.Name.Substring(5);
                field.SetValue(this, serializedObject.FindProperty(fieldName));
                if (field.GetValue(this) == null)
                    Debug.LogError($"Failed to load {target.GetType()} property: {field.Name} !=> {fieldName}");
            }
        }

        protected void BeginProperties(bool showColor = true, bool canEditDetailLevel = true)
        {
            soRnd.Update();

            using (new ShapesUI.GroupScope())
            {
                updateMeshFromEditorChange = false;

                ShapesUI.SortedEnumPopup<ShapesBlendMode>(blendModeGuiContent, propBlendMode);
                if ((target as ShapeRenderer).HasScaleModes)
                    EditorGUILayout.PropertyField(propScaleMode, scaleModeGuiContent);

                // sorting/depth stuff
                using (new EditorGUI.IndentLevelScope(1))
                {
                    if (showDepth = EditorGUILayout.Foldout(showDepth, "Sorting & Depth"))
                        using (ShapesUI.TempLabelWidth(140))
                        {
                            EditorGUILayout.PropertyField(propRenderQueue, renderQueueGuiContent);
                            ShapesUI.RenderSortingLayerField(propSortingLayer);
                            EditorGUILayout.PropertyField(propSortingOrder);
                            EditorGUILayout.PropertyField(propZTest, zTestGuiContent);
                            EditorGUILayout.PropertyField(propZOffsetFactor, zOffsetFactorGuiContent);
                            EditorGUILayout.PropertyField(propZOffsetUnits, zOffsetUnitsGuiContent);
                        }
                }

                // stencil
                using (new EditorGUI.IndentLevelScope(1))
                {
                    if (showStencil = EditorGUILayout.Foldout(showStencil, "Masking"))
                    {
                        DrawColorMaskButtons();
                        EditorGUILayout.PropertyField(propStencilComp, stencilCompGuiContent);
                        EditorGUILayout.PropertyField(propStencilOpPass, stencilOpPassGuiContent);
                        EditorGUILayout.PropertyField(propStencilRefID, stencilIDGuiContent);
                        EditorGUILayout.PropertyField(propStencilReadMask, stencilReadMaskGuiContent);
                        EditorGUILayout.PropertyField(propStencilWriteMask, stencilWriteMaskGuiContent);
                    }
                }

                // warning box about instancing
                var uniqueCount = 0;
                var instancedCount = 0;
                foreach (var obj in targets.Cast<ShapeRenderer>())
                    if (obj.IsUsingUniqueMaterials)
                        uniqueCount++;
                    else
                        instancedCount++;

                if (uniqueCount > 0)
                {
                    string infix;
                    if (instancedCount == 0)
                        infix = uniqueCount == 1 ? "this object is" : "these objects are";
                    else // mixed selection
                        infix = "some of these objects are";

                    var label = $"Note: {infix} not GPU instanced due to custom depth/mask settings";

                    var wrapLabel = new GUIStyle(EditorStyles.miniLabel);
                    wrapLabel.wordWrap = true;
                    using (ShapesUI.Horizontal)
                    {
                        var icon = EditorGUIUtility.IconContent("console.warnicon.sml");
                        GUILayout.Label(icon);
                        GUILayout.TextArea(label, wrapLabel);
                        if (GUILayout.Button("Reset", EditorStyles.miniButton))
                        {
                            propZTest.enumValueIndex = (int)ShapeRenderer.DEFAULT_ZTEST;
                            propZOffsetFactor.floatValue = ShapeRenderer.DEFAULT_ZOFS_FACTOR;
                            propZOffsetUnits.intValue = ShapeRenderer.DEFAULT_ZOFS_UNITS;
                            propColorMask.intValue = (int)ShapeRenderer.DEFAULT_COLOR_MASK;
                            propStencilComp.enumValueIndex = (int)ShapeRenderer.DEFAULT_STENCIL_COMP;
                            propStencilOpPass.enumValueIndex = (int)ShapeRenderer.DEFAULT_STENCIL_OP;
                            propStencilRefID.intValue = ShapeRenderer.DEFAULT_STENCIL_REF_ID;
                            propStencilReadMask.intValue = ShapeRenderer.DEFAULT_STENCIL_MASK;
                            propStencilWriteMask.intValue = ShapeRenderer.DEFAULT_STENCIL_MASK;
                            propRenderQueue.intValue = ShapeRenderer.DEFAULT_RENDER_QUEUE_AUTO;
                        }
                    }
                }
            }

            if ((target as ShapeRenderer).HasDetailLevels)
                using (new EditorGUI.DisabledScope(canEditDetailLevel == false))
                {
                    if (canEditDetailLevel)
                        using (var chChk = new EditorGUI.ChangeCheckScope())
                        {
                            EditorGUILayout.PropertyField(propDetailLevel);
                            if (chChk.changed)
                                updateMeshFromEditorChange = true;
                        }
                    else
                        EditorGUILayout.TextField(propDetailLevel.displayName, "∞", GUI.skin.label);
                }

            if (showColor)
                PropertyFieldColor();
        }

        protected bool EndProperties()
        {
            var propertiesDidChange = soRnd.ApplyModifiedProperties() | serializedObject.ApplyModifiedProperties();
            if (updateMeshFromEditorChange)
            {
                foreach (var shape in targets.Cast<ShapeRenderer>())
                    shape.UpdateMesh();
                updateMeshFromEditorChange = false;
            }

            return propertiesDidChange;
        }

        protected void PropertyFieldColor()
        {
            EditorGUILayout.PropertyField(propColor);
        }

        protected void PropertyFieldColor(string s)
        {
            EditorGUILayout.PropertyField(propColor, new GUIContent(s));
        }

        protected void PropertyFieldColor(GUIContent content)
        {
            EditorGUILayout.PropertyField(propColor, content);
        }

        private void DrawColorMaskButtons()
        {
            var value = propColorMask.intValue;

            void DoButton(int i)
            {
                var flagValue = 1 << (3 - i); // enum is in ABGR order
                var prevBit = (value & flagValue) > 0;
                GUI.color = prevBit ? channelColors[i] : channelUnsetColor;
                var newBit = GUILayout.Toggle(prevBit, colorCh[i].ToString(), ShapesUI.GetMiniButtonStyle(i, 4),
                    GUILayout.Width(28));
                GUI.color = Color.white;
                if (prevBit != newBit)
                {
                    var sign = prevBit == false ? 1 : -1;
                    propColorMask.intValue += sign * flagValue;
                }
            }

            GUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(colorMaskGuiContent);
            for (var i = 0; i < 4; i++)
                DoButton(i);
            GUILayout.EndHorizontal();
        }
    }
}