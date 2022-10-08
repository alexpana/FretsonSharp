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
    public static class ShapesUI
    {
        public const int POS_COLOR_FIELD_LABEL_WIDTH = 32;
        public const int POS_COLOR_FIELD_COLOR_WIDTH = 50;
        public const int POS_COLOR_FIELD_THICKNESS_WIDTH = 52;

        private static Assembly editorAssembly;


        private static readonly GUIContent editButtonContent = new("Check out & edit",
            "This will check out the necessary files in your version control system to allow editing");

        private static readonly GUILayoutOption[] editButtonLayout = { GUILayout.Width(100) };

        private static readonly GUIContent[] vec3Labels = { new("X"), new("Y"), new("Z") };


        // used for sorting layer popup
        private static GUIStyle boldPopupStyle;

        private static readonly GUIContent sortingLayerStyle =
            EditorGUIUtility.TrTextContent("Sorting Layer", "Name of the Renderer's sorting layer");

        private static MethodInfo sortingLayerField;

        private static Rect enumRect = default;


        private static MethodInfo setIconEnabled; // haha long line go brrrr

        private static Assembly EditorAssembly
        {
            get
            {
                if (editorAssembly == null)
                {
                    var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                    editorAssembly = assemblies.First(x => x.FullName.StartsWith("UnityEditor,"));
                }

                return editorAssembly;
            }
        }

        public static EditorGUILayout.HorizontalScope Horizontal => new();
        public static EditorGUILayout.VerticalScope Vertical => new();
        public static GroupScope Group => new();

        private static GUIStyle BoldPopupStyle => boldPopupStyle ??
                                                  (boldPopupStyle = new GUIStyle(EditorStyles.popup)
                                                      { fontStyle = FontStyle.Bold });

        private static MethodInfo SortingLayerField
        {
            get
            {
                if (sortingLayerField == null)
                    sortingLayerField = typeof(EditorGUILayout).GetMethod(
                        "SortingLayerField",
                        BindingFlags.Static | BindingFlags.NonPublic,
                        Type.DefaultBinder,
                        new[] { typeof(GUIContent), typeof(SerializedProperty), typeof(GUIStyle), typeof(GUIStyle) },
                        null
                    );
                return sortingLayerField;
            }
        }

        private static MethodInfo SetIconEnabled => setIconEnabled = setIconEnabled ??
                                                                     Assembly.GetAssembly(typeof(Editor))
                                                                         ?.GetType("UnityEditor.AnnotationUtility")
                                                                         ?.GetMethod("SetIconEnabled",
                                                                             BindingFlags.Static |
                                                                             BindingFlags.NonPublic);

        public static GUIStyle GetMiniButtonStyle(int i, int maxCount)
        {
            if (maxCount > 1)
            {
                if (i == 0)
                    return EditorStyles.miniButtonLeft;
                if (i == maxCount - 1)
                    return EditorStyles.miniButtonRight;
                return EditorStyles.miniButtonMid;
            }

            return EditorStyles.miniButton;
        }

        public static int EnumButtonRow(int currentVal, string[] labels, bool hiddenZeroValue)
        {
            var iOffset = hiddenZeroValue ? 1 : 0;
            var count = labels.Length;
            var returnVal = currentVal;

            GUILayout.BeginHorizontal();
            for (var i = iOffset; i < count; i++)
            {
                var style = GetMiniButtonStyle(i - iOffset, count - iOffset);
                var pressedBefore = i == currentVal;
                var pressedAfter = GUILayout.Toggle(pressedBefore, labels[i], style);
                if (pressedBefore == false && pressedAfter) returnVal = i;

                if (hiddenZeroValue && pressedBefore && pressedAfter == false) returnVal = 0;
            }

            GUILayout.EndHorizontal();

            return returnVal;
        }

        public static void RepaintAllSceneViews()
        {
            foreach (SceneView sceneView in SceneView.sceneViews)
                sceneView.Repaint();
        }


        public static void ShowColorPicker(Action<Color> colorChangedCallback, Color col)
        {
            var colorPickerType = EditorAssembly.GetType("UnityEditor.ColorPicker", true);
            var showColorPicker = colorPickerType.GetMethod("Show", BindingFlags.Static | BindingFlags.Public,
                Type.DefaultBinder, CallingConventions.Any,
                new[] { typeof(Action<Color>), typeof(Color), typeof(bool), typeof(bool) }, null);
            var hdr = ShapesConfig.Instance.useHdrColorPickers;
            showColorPicker.Invoke(null, new object[] { colorChangedCallback, col, true, hdr });
        }


        public static bool DrawTypeSwitchButtons(SerializedProperty prop, GUIContent[] guiContent,
            int[] indexOverride = null, int height = 20)
        {
            int GetEntryCount()
            {
                switch (prop.propertyType)
                {
                    case SerializedPropertyType.Enum: return prop.enumNames.Length;
                    case SerializedPropertyType.Integer: return indexOverride.Length;
                    default: throw new IndexOutOfRangeException("no, illegal >:I");
                }
            }

            var multiselectPressedState = prop.TryGetMultiselectPressedStates(indexOverride, GetEntryCount());

            var changed = false;
            var so = prop.serializedObject;
            var multiselect = so.isEditingMultipleObjects;

            EditorGUI.BeginChangeCheck();
            GUILayoutOption[] buttonLayout = { GUILayout.Height(height), GUILayout.MinWidth(height) };

            void EnumButton(int index)
            {
                var style = GetMiniButtonStyle(index, guiContent.Length);
                bool btnState;
                if (multiselect)
                    btnState = multiselectPressedState[index];
                else
                    btnState = index == prop.GetIntValue(indexOverride) && prop.hasMultipleDifferentValues == false;
                var btnStateNew = GUILayout.Toggle(btnState, guiContent[index], style, buttonLayout);

                var pressedInMultiselect = multiselect && btnState != btnStateNew;
                var pressedInSingleselect = multiselect == false && btnStateNew && btnState == false;

                if (pressedInMultiselect || pressedInSingleselect)
                {
                    prop.SetIntValue(index, indexOverride);
                    changed = true;
                }
            }

            GUILayout.BeginHorizontal();
            for (var i = 0; i < guiContent.Length; i++)
                EnumButton(i);
            GUILayout.EndHorizontal();
            return changed;
        }


        public static void AngleProperty(SerializedProperty prop, string label, SerializedProperty unitProp,
            params GUILayoutOption[] layout)
        {
            var unit = unitProp.hasMultipleDifferentValues ? AngularUnit.Radians : (AngularUnit)unitProp.enumValueIndex;

            using (Horizontal)
            {
                // value field
                using (var chChk = new EditorGUI.ChangeCheckScope())
                {
                    EditorGUI.showMixedValue = prop.hasMultipleDifferentValues;
                    var newValue = EditorGUILayout.FloatField(label, prop.floatValue * unit.FromRadians()) *
                                   unit.ToRadians();
                    if (chChk.changed)
                        prop.floatValue = newValue;
                    EditorGUI.showMixedValue = false;
                }

                // unit suffix
                GUILayout.Label(unit.Suffix(), layout);
            }
        }

        public static void DrawAngleSwitchButtons(SerializedProperty prop)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(" ");
            var angLabels = Screen.width < 300
                ? UIAssets.AngleUnitButtonContentsShort
                : UIAssets.AngleUnitButtonContents;
            DrawTypeSwitchButtons(prop, angLabels, null, 15);
            GUILayout.EndHorizontal();
        }

        public static void EnumToggleProperty(SerializedProperty enumProp, string label)
        {
            using (var chChk = new EditorGUI.ChangeCheckScope())
            {
                EditorGUI.showMixedValue = enumProp.hasMultipleDifferentValues;
                var newValue = EditorGUILayout.Toggle(new GUIContent(label), enumProp.enumValueIndex == 1);
                if (chChk.changed)
                    enumProp.enumValueIndex = newValue.AsInt();
                EditorGUI.showMixedValue = false;
            }
        }

        private static void BeginGroup()
        {
            GUILayout.BeginVertical(EditorStyles.helpBox);
        }

        private static void EndGroup()
        {
            GUILayout.EndVertical();
        }

        public static void PropertyLabelWidth(SerializedProperty prop, string label, float labelWidth,
            params GUILayoutOption[] options)
        {
            using (TempLabelWidth(labelWidth))
            {
                EditorGUILayout.PropertyField(prop, new GUIContent(label), options);
            }
        }

        public static void PropertyFieldWidth(SerializedProperty prop, string label, float fieldWidth,
            params GUILayoutOption[] options)
        {
            using (TempFieldWidth(fieldWidth))
            {
                EditorGUILayout.PropertyField(prop, label == null ? GUIContent.none : new GUIContent(label), options);
            }
        }

        public static TemporaryLabelWidth TempLabelWidth(float width)
        {
            return new TemporaryLabelWidth(width);
        }

        public static TemporaryFieldWidth TempFieldWidth(float width)
        {
            return new TemporaryFieldWidth(width);
        }


        public static void FloatInSpaceField(SerializedProperty value, SerializedProperty space,
            bool spaceEnabled = true)
        {
            using (Horizontal)
            {
                EditorGUILayout.PropertyField(value);
                using (EnabledIf(spaceEnabled))
                {
                    EditorGUILayout.PropertyField(space, GUIContent.none, GUILayout.Width(64));
                }
            }
        }

        private static TemporaryColor TempColor(Color color)
        {
            return new TemporaryColor(color);
        }

        private static EditorGUI.DisabledScope EnabledIf(bool enabled)
        {
            return new EditorGUI.DisabledScope(enabled == false);
        }

        public static void PosColorField(string label, SerializedProperty pos, SerializedProperty col,
            bool colorEnabled = true, bool positionEnabled = true)
        {
            PosColorField(label, () => EditorGUILayout.PropertyField(pos, GUIContent.none), col, colorEnabled,
                positionEnabled);
        }

        public static void PosColorFieldPosOff(string label, Vector3 displayPos, SerializedProperty col,
            bool colorEnabled = true)
        {
            PosColorField(label,
                () => EditorGUILayout.Vector3Field(GUIContent.none, displayPos, GUILayout.MinWidth(32f)), col,
                colorEnabled, false);
        }

        public static void PosColorFieldSpecialOffState(string label, SerializedProperty pos, Vector3 offDisplayPos,
            SerializedProperty col, bool colorEnabled = true, bool positionEnabled = true)
        {
            if (positionEnabled)
                PosColorField(label, pos, col, colorEnabled);
            else
                PosColorFieldPosOff(label, offDisplayPos, col, colorEnabled);
        }

        public static bool CenteredButton(GUIContent content)
        {
            var pressed = false;
            using (Horizontal)
            {
                GUILayout.FlexibleSpace();
                pressed = GUILayout.Button(content, GUILayout.ExpandWidth(false));
                GUILayout.FlexibleSpace();
            }

            return pressed;
        }

        public static void PosColorField(Rect rect, SerializedProperty pos, SerializedProperty col,
            bool colorEnabled = true)
        {
            var rectColor = rect;
            rectColor.xMin = rect.xMax - POS_COLOR_FIELD_COLOR_WIDTH;
            rectColor.xMax = rect.xMax;
            var rectPos = rect;
            rectPos.width -= rectColor.width;
            EditorGUI.PropertyField(rectPos, pos, GUIContent.none);
            using (EnabledIf(colorEnabled))
            using (TempColor(colorEnabled ? Color.white : Color.clear))
            {
                EditorGUI.PropertyField(rectColor, col, GUIContent.none);
            }
        }

        public static void PosThicknessColorField(Rect rect, SerializedProperty pos, SerializedProperty thickness,
            SerializedProperty col, bool colorEnabled = true, bool zEnabled = true)
        {
            const float THICKNESS_MARGIN = 2;
            const float rightSideWidth =
                POS_COLOR_FIELD_COLOR_WIDTH + POS_COLOR_FIELD_THICKNESS_WIDTH + THICKNESS_MARGIN;

            var rectColor = rect;
            rectColor.x = rect.xMax - POS_COLOR_FIELD_COLOR_WIDTH;
            rectColor.width = POS_COLOR_FIELD_COLOR_WIDTH;

            var rectThickness = rect;
            rectThickness.x = rect.xMax - rightSideWidth + THICKNESS_MARGIN;
            rectThickness.width = POS_COLOR_FIELD_THICKNESS_WIDTH;

            var rectPos = rect;
            rectPos.width -= rightSideWidth;

            Vector3Field(rectPos, pos, zEnabled);
            using (TempLabelWidth(18))
            {
                EditorGUI.PropertyField(rectThickness, thickness, new GUIContent("Th", "thickness"));
            }

            using (EnabledIf(colorEnabled))
            using (TempColor(colorEnabled ? Color.white : Color.clear))
            {
                EditorGUI.PropertyField(rectColor, col, GUIContent.none);
            }
        }

        private static void Vector3Field(Rect rect, SerializedProperty v, bool zEnabled = true)
        {
            if (zEnabled)
            {
                EditorGUI.PropertyField(rect, v, GUIContent.none);
            }
            else
            {
                var x = v.FindPropertyRelative("x");
                var y = v.FindPropertyRelative("y");
                var z = v.FindPropertyRelative("z");
                float[] values = { x.floatValue, y.floatValue, z.floatValue };
                bool[] enabledStates = { true, true, false };

                using (var chChk = new EditorGUI.ChangeCheckScope())
                {
                    rect.height = 16; // matching Unity's vector 3 field in 2018.4
                    MultiFloatField(rect, vec3Labels, values, enabledStates);
                    if (chChk.changed)
                    {
                        x.floatValue = values[0];
                        y.floatValue = values[1];
                        // z.floatValue = values[2]; // never necessary since it's disabled anyway
                    }
                }
            }
        }

        // from: https://github.com/Unity-Technologies/UnityCsReference/blob/master/Editor/Mono/EditorGUI.cs
        private static void MultiFloatField(Rect position, GUIContent[] subLabels, float[] values, bool[] enabledStates,
            float prefixLabelWidth = -1)
        {
            var eCount = values.Length;
            const int kSpacingSubLabel = 4;
            var w = (position.width - (eCount - 1) * kSpacingSubLabel) / eCount;
            var nr = new Rect(position) { width = w };
            var t = EditorGUIUtility.labelWidth;
            var l = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            for (var i = 0; i < values.Length; i++)
                using (new EditorGUI.DisabledScope(enabledStates[i] == false))
                {
                    EditorGUIUtility.labelWidth =
                        prefixLabelWidth > 0 ? prefixLabelWidth : CalcPrefixLabelWidth(subLabels[i]);
                    values[i] = EditorGUI.FloatField(nr, subLabels[i], values[i]);
                    nr.x += w + kSpacingSubLabel;
                }

            EditorGUIUtility.labelWidth = t;
            EditorGUI.indentLevel = l;
        }


        // from: https://github.com/Unity-Technologies/UnityCsReference/blob/master/Editor/Mono/EditorGUI.cs
        private static float CalcPrefixLabelWidth(GUIContent label, GUIStyle style = null)
        {
            if (style == null) style = EditorStyles.label;
            return style.CalcSize(label).x;
        }

        private static void PosColorField(string label, Action field, SerializedProperty col, bool colorEnabled = true,
            bool positionEnabled = true)
        {
            using (Horizontal)
            {
                GUILayout.Label(label, GUILayout.Width(POS_COLOR_FIELD_LABEL_WIDTH));
                using (EnabledIf(positionEnabled))
                {
                    field();
                }

                using (EnabledIf(colorEnabled))
                using (TempColor(colorEnabled ? Color.white : Color.clear))
                {
                    EditorGUILayout.PropertyField(col, GUIContent.none, GUILayout.Width(POS_COLOR_FIELD_COLOR_WIDTH));
                }
            }
        }

        public static void RenderSortingLayerField(SerializedProperty sortingLayer)
        {
            var hasPrefabOverride = sortingLayer.HasPrefabOverride();
            SortingLayerField.Invoke(null, new object[]
            {
                sortingLayerStyle,
                sortingLayer,
                hasPrefabOverride ? BoldPopupStyle : EditorStyles.popup,
                hasPrefabOverride ? EditorStyles.boldLabel : EditorStyles.label
            });
        }

        // presumes enum is of int type
        public static void SortedEnumPopup<T>(GUIContent label, SerializedProperty prop, string[] enumLabels = null)
            where T : Enum
        {
            var fields = typeof(T).GetFields()
                .Where(fi => fi.IsStatic)
                .OrderBy(fi => fi.MetadataToken)
                .ToArray();
            var displayOrder = fields.Select(x => Convert.ToInt32((T)x.GetValue(null))).ToArray();
            if (enumLabels == null)
                enumLabels = fields.Select(x => ((T)x.GetValue(null)).GetDescription()).ToArray();
            var valuesSorted = (int[])Enum.GetValues(typeof(T));
            var currentValue = valuesSorted[prop.enumValueIndex]; // enum index to value
            var displayIndex = Array.IndexOf(displayOrder, currentValue); // value to display index

            void SetPropertyValue(int newDisplayIndex)
            {
                var newValue = displayOrder[newDisplayIndex]; // display index to value
                prop.enumValueIndex = Array.IndexOf(valuesSorted, newValue); // value to enum index
                prop.serializedObject.ApplyModifiedProperties();
            }

            using (Horizontal)
            {
                EditorGUILayout.PrefixLabel(label);
                if (GUILayout.Button(prop.hasMultipleDifferentValues ? "-" : enumLabels[displayIndex].Replace("_", ""),
                        EditorStyles.popup))
                {
                    var menu = new GenericMenu();
                    for (var i = 0; i < enumLabels.Length; i++)
                    {
                        var displayName = enumLabels[i];
                        var addSeparator = displayName.EndsWith("_");
                        if (addSeparator)
                            displayName = displayName.Substring(0, displayName.Length - 1);
                        var j = i;
                        menu.AddItem(new GUIContent(displayName),
                            prop.hasMultipleDifferentValues == false && i == displayIndex, () => SetPropertyValue(j));
                        if (addSeparator)
                            menu.AddSeparator(string.Empty);
                    }

                    menu.DropDown(enumRect);
                }

                if (Event.current.type == EventType.Repaint)
                    enumRect = GUILayoutUtility.GetLastRect();
            }
        }

        public static void SetGizmoIconEnabled(Type type, bool on)
        {
            if (SetIconEnabled == null) return;
            const int MONO_BEHAVIOR_CLASS_ID = 114; // https://docs.unity3d.com/Manual/ClassIDReference.html
            SetIconEnabled.Invoke(null, new object[] { MONO_BEHAVIOR_CLASS_ID, type.Name, on ? 1 : 0 });
        }

        private struct TemporaryColor : IDisposable
        {
            private static readonly Stack<Color> temporaryColors = new();

            public TemporaryColor(Color color)
            {
                temporaryColors.Push(GUI.color);
                GUI.color = color;
            }

            public void Dispose()
            {
                GUI.color = temporaryColors.Pop();
            }
        }

        public class GroupScope : IDisposable
        {
            public GroupScope()
            {
                BeginGroup();
            }

            public void Dispose()
            {
                EndGroup();
            }
        }

        public readonly struct AssetEditScope : IDisposable
        {
            public AssetEditScope(Object asset)
            {
                if (ShapesIO.IsUsingVcWithCheckoutEnabled)
                {
                    var canEdit = ShapesIO.AssetCanBeEdited(asset);
                    GUILayout.BeginVertical();
                    using (Horizontal)
                    {
                        if (canEdit)
                        {
                            using (new EditorGUI.DisabledScope(true))
                            {
                                GUILayout.Toggle(true, editButtonContent, EditorStyles.miniButton,
                                    editButtonLayout); // just, show that, you know
                            }

                            GUILayout.Label("(open for editing)", EditorStyles.miniLabel);
                        }
                        else
                        {
                            if (GUILayout.Button(editButtonContent, EditorStyles.miniButton, editButtonLayout))
                                _ = ShapesIO.TryMakeAssetsEditable(asset); // will show error if it fails
                            GUILayout.Label("(currently locked by version control)", EditorStyles.miniLabel);
                        }
                    }

                    EditorGUI.BeginDisabledGroup(canEdit == false);
                }
            }

            public void Dispose()
            {
                if (ShapesIO.IsUsingVcWithCheckoutEnabled)
                {
                    EditorGUI.EndDisabledGroup();
                    GUILayout.EndVertical();
                }
            }
        }

        public struct TemporaryLabelWidth : IDisposable
        {
            private static readonly Stack<float> temporaryWidths = new();

            public TemporaryLabelWidth(float width)
            {
                temporaryWidths.Push(EditorGUIUtility.labelWidth);
                EditorGUIUtility.labelWidth = width;
            }

            public void Dispose()
            {
                EditorGUIUtility.labelWidth = temporaryWidths.Pop();
            }
        }

        public struct TemporaryFieldWidth : IDisposable
        {
            private static readonly Stack<float> temporaryWidths = new();

            public TemporaryFieldWidth(float width)
            {
                temporaryWidths.Push(EditorGUIUtility.fieldWidth);
                EditorGUIUtility.fieldWidth = width;
            }

            public void Dispose()
            {
                EditorGUIUtility.fieldWidth = temporaryWidths.Pop();
            }
        }
    }
}