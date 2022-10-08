using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes
{
    public class SceneRectEditor : SceneEditGizmos
    {
        private static bool isEditing;

        private readonly BoxBoundsHandle boxHandle = ShapesHandles.InitBoxHandle();

        public SceneRectEditor(Editor parentEditor)
        {
            this.parentEditor = parentEditor;
        }

        protected override bool IsEditing
        {
            get => isEditing;
            set => isEditing = value;
        }

        public bool DoSceneHandles(Rectangle rect)
        {
            if (IsEditing == false)
                return false;

            var holdingAlt = (Event.current.modifiers & EventModifiers.Alt) != 0;
            var showControls = holdingAlt == false;

            // set up matrix
            var gizmoToWorld = Matrix4x4.TRS(default, rect.transform.rotation, Vector3.one);
            var worldToGizmo = gizmoToWorld.inverse;

            Vector2 GizmoToRect(Vector3 gizmoPt)
            {
                var wPos = gizmoToWorld.MultiplyPoint(gizmoPt);
                return rect.transform.InverseTransformPoint(wPos);
            }

            Vector3 RectToGizmo(Vector2 rectPt)
            {
                var wPos = rect.transform.TransformPoint(rectPt);
                return worldToGizmo.MultiplyPoint(wPos);
            }

            Vector2 GizmoToRectVec(Vector3 gizmoPt)
            {
                var wPos = gizmoToWorld.MultiplyVector(gizmoPt);
                return rect.transform.InverseTransformVector(wPos);
            }

            Vector3 RectToGizmoVec(Vector2 rectPt)
            {
                var wPos = rect.transform.TransformVector(rectPt);
                return worldToGizmo.MultiplyVector(wPos);
            }

            Vector2 GetCenter()
            {
                return GizmoToRect(boxHandle.center);
            }

            Vector2 GetSize()
            {
                return GizmoToRectVec(boxHandle.size);
            }

            Vector2 GetBottomLeft()
            {
                return GetCenter() - GetSize() / 2;
            }

            Vector2 RectCenter()
            {
                return rect.Pivot == RectPivot.Center ? Vector2.zero : RectSize() / 2;
            }

            Vector2 RectSize()
            {
                return new(rect.Width, rect.Height);
            }

            Vector2 RectBottomLeft()
            {
                return RectCenter() - RectSize() / 2;
            }

            using (new Handles.DrawingScope(ShapesHandles.GetHandleColor(rect.Color), gizmoToWorld))
            {
                boxHandle.size = RectToGizmoVec(new Vector2(rect.Width, rect.Height));
                boxHandle.center = RectToGizmo(RectCenter());
                var prevBottomLeft = GetBottomLeft();

                using (var chchk = new EditorGUI.ChangeCheckScope())
                {
                    boxHandle.DrawHandle();
                    if (chchk.changed)
                    {
                        Undo.RecordObject(rect, "edit rectangle");
                        var newSize = GetSize();
                        rect.Width = newSize.x;
                        rect.Height = newSize.y;
                        if (rect.Pivot == RectPivot.Corner)
                        {
                            var rtf = rect.transform;
                            Undo.RecordObject(rtf, "edit rectangle");
                            var newBottomLeft = GetBottomLeft();
                            var delta = newBottomLeft - prevBottomLeft;
                            var parent = rtf.parent;
                            var deltaWorld = rtf.TransformVector(delta);
                            var deltaParent = parent != null ? parent.InverseTransformVector(deltaWorld) : deltaWorld;
                            rtf.localPosition += deltaParent;
                        }

                        return true;
                    }
                }

                if (rect.IsRounded)
                {
                    var gizUp = RectToGizmoVec(Vector3.up);
                    var gizRight = RectToGizmoVec(Vector3.right);
                    var gizLeft = -gizRight;
                    var gizDown = -gizUp;

                    ( Vector3 a, Vector3 b )[] gizDirs =
                    {
                        (gizRight, gizUp),
                        (gizDown, gizRight),
                        (gizLeft, gizDown),
                        (gizUp, gizLeft)
                    };

                    var rectBl = RectBottomLeft();
                    Vector3[] gizCorners =
                    {
                        RectToGizmo(rectBl),
                        RectToGizmo(rectBl + Vector2.up * rect.Height),
                        RectToGizmo(rectBl + Vector2.up * rect.Height + Vector2.right * rect.Width),
                        RectToGizmo(rectBl + Vector2.right * rect.Width)
                    };

                    var maxRadius = Mathf.Min(rect.Width, rect.Height) / 2f;

                    for (var i = 0; i < 4; i++)
                    {
                        var prevRadius = rect.CornerRadiusMode == Rectangle.RectangleCornerRadiusMode.Uniform
                            ? rect.CornerRadius
                            : rect.CornerRadii[i];
                        var radiusGizmoSpace = RectToGizmoVec(prevRadius * Vector2.one).x;
                        var cornerGizmoSpace = gizCorners[i];

                        // markers
                        var innerCornerGizmoSpace = cornerGizmoSpace + (gizDirs[i].a + gizDirs[i].b) * radiusGizmoSpace;
                        Handles.DrawWireArc(innerCornerGizmoSpace, Vector3.forward, -gizDirs[i].a, 90f,
                            radiusGizmoSpace);
                        using (new Handles.DrawingScope(new Color(Handles.color.r, Handles.color.g, Handles.color.b,
                                   0.5f)))
                        {
                            Handles.DrawLine(innerCornerGizmoSpace, cornerGizmoSpace + gizDirs[i].a * radiusGizmoSpace);
                            Handles.DrawLine(innerCornerGizmoSpace, cornerGizmoSpace + gizDirs[i].b * radiusGizmoSpace);
                        }

                        Handles.DrawLine(innerCornerGizmoSpace,
                            innerCornerGizmoSpace - (gizDirs[i].a + gizDirs[i].b) * radiusGizmoSpace / Mathf.Sqrt(2));

                        //Handles.DrawWireDisc( cornerGizmoSpace + (gizDirs[i].a + gizDirs[i].b ) * radiusGizmoSpace, Vector3.forward, prevRadius );

                        if (showControls == false)
                            continue;
                        using (var chchk = new EditorGUI.ChangeCheckScope())
                        {
                            var handlePos = innerCornerGizmoSpace;
                            var size = HandleUtility.GetHandleSize(handlePos) * 0.15f;
                            var gizDir = gizDirs[i].a + gizDirs[i].b; // diagonal
                            var newPosGizmoSpace = Handles.Slider(handlePos, gizDir, size, Handles.CubeHandleCap, 0f);
                            if (chchk.changed)
                            {
                                Undo.RecordObject(rect, "edit rectangle corner radius");
                                var deltaGizmoSpace = newPosGizmoSpace - cornerGizmoSpace;
                                var newRadius = Mathf.Abs(GizmoToRectVec(deltaGizmoSpace).x);
                                if (rect.CornerRadiusMode == Rectangle.RectangleCornerRadiusMode.Uniform)
                                {
                                    rect.CornerRadius = Mathf.Min(newRadius, maxRadius);
                                }
                                else
                                {
                                    var radii = rect.CornerRadii;
                                    radii[i] = Mathf.Min(newRadius, maxRadius);
                                    rect.CornerRadii = radii;
                                }

                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}