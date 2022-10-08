using System;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes
{
    public class SceneDiscEditor : SceneEditGizmos
    {
        private static bool isEditing;

        private static ShapeRenderer currentSceneShape;

        private readonly ArcHandle arcHandleEnd = ShapesHandles.InitAngularHandle();
        private readonly ArcHandle arcHandleRadius = ShapesHandles.InitRadialHandle();
        private readonly ArcHandle arcHandleStart = ShapesHandles.InitAngularHandle();
        private readonly ArcHandle arcHandleThicknessInner = ShapesHandles.InitRadialHandle();
        private readonly ArcHandle arcHandleThicknessOuter = ShapesHandles.InitRadialHandle();

        public SceneDiscEditor(Editor parentEditor)
        {
            this.parentEditor = parentEditor;
        }

        protected override bool IsEditing
        {
            get => isEditing;
            set => isEditing = value;
        }

        private static ThicknessSpace RadiusSpace
        {
            get
            {
                switch (currentSceneShape)
                {
                    case Disc d: return d.RadiusSpace;
                    case RegularPolygon rp: return rp.RadiusSpace;
                    default: return default;
                }
            }
        }

        private static ThicknessSpace ThicknessSpace
        {
            get
            {
                switch (currentSceneShape)
                {
                    case Disc d: return d.ThicknessSpace;
                    case RegularPolygon rp: return rp.ThicknessSpace;
                    default: return default;
                }
            }
        }

        private static bool HasThickness
        {
            get
            {
                switch (currentSceneShape)
                {
                    case Disc d: return d.HasThickness;
                    case RegularPolygon rp: return rp.Border;
                    default: return default;
                }
            }
        }

        private static float Thickness
        {
            get
            {
                switch (currentSceneShape)
                {
                    case Disc d: return d.Thickness;
                    case RegularPolygon rp: return rp.Thickness;
                    default: return default;
                }
            }
            set
            {
                if (currentSceneShape is Disc d) d.Thickness = value;
                else if (currentSceneShape is RegularPolygon rp) rp.Thickness = value;
            }
        }

        private static float Radius
        {
            get
            {
                switch (currentSceneShape)
                {
                    case Disc d: return d.Radius;
                    case RegularPolygon rp: return rp.Radius;
                    default: return default;
                }
            }
            set
            {
                if (currentSceneShape is Disc d) d.Radius = value;
                else if (currentSceneShape is RegularPolygon rp) rp.Radius = value;
            }
        }

        private Color GetAvgDiscColor(Disc disc)
        {
            switch (disc.ColorMode)
            {
                case Disc.DiscColorMode.Single: return disc.Color;
                case Disc.DiscColorMode.Radial: return (disc.ColorOuter + disc.ColorInner) / 2f;
                case Disc.DiscColorMode.Angular: return (disc.ColorStart + disc.ColorEnd) / 2f;
                case Disc.DiscColorMode.Bilinear:
                    return (disc.ColorInnerStart + disc.ColorInnerEnd + disc.ColorOuterStart + disc.ColorOuterEnd) / 2f;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private Color GetHandleColor(ShapeRenderer shape)
        {
            if (shape is Disc disc)
                return ShapesHandles.GetHandleColor(GetAvgDiscColor(disc));
            return shape.Color;
        }


        public bool DoSceneHandles(ShapeRenderer shape)
        {
            if (IsEditing == false)
                return false;
            currentSceneShape = shape;
            if (RadiusSpace != ThicknessSpace.Meters)
                return false;

            var holdingShift = (Event.current.modifiers & EventModifiers.Shift) != 0;
            var editInnerOuterRadius = holdingShift;

            // set up matrix
            var rootDir = shape.transform.right;
            var discNormal = shape.transform.forward;
            var rot = Quaternion.LookRotation(rootDir, discNormal);
            var mtx = Matrix4x4.TRS(shape.transform.position, rot, Vector3.one); // todo: scale?

            using (new Handles.DrawingScope(GetHandleColor(shape), mtx))
            {
                // thickness handles
                if (HasThickness && ThicknessSpace == ThicknessSpace.Meters)
                {
                    using (var chchk = new EditorGUI.ChangeCheckScope())
                    {
                        arcHandleThicknessOuter.radius = Radius + Thickness * 0.5f;
                        arcHandleThicknessOuter.DrawHandle();
                        if (chchk.changed)
                        {
                            Undo.RecordObject(shape, "edit disc");
                            if (editInnerOuterRadius)
                            {
                                var prevInnerRadius = Radius - Thickness * 0.5f;
                                var newOuterRadius = arcHandleThicknessOuter.radius;
                                Radius = (prevInnerRadius + newOuterRadius) / 2;
                                Thickness = newOuterRadius - prevInnerRadius;
                            }
                            else
                            {
                                Thickness = (arcHandleThicknessOuter.radius - Radius) * 2;
                            }
                        }
                    }

                    // inner radius
                    if (editInnerOuterRadius)
                        using (var chchk = new EditorGUI.ChangeCheckScope())
                        {
                            arcHandleThicknessInner.radius = Radius - Thickness * 0.5f;
                            arcHandleThicknessInner.DrawHandle();
                            if (chchk.changed)
                            {
                                Undo.RecordObject(shape, "edit disc");
                                var prevOuterRadius = Radius + Thickness * 0.5f;
                                var newInnerRadius = arcHandleThicknessInner.radius;
                                Radius = (newInnerRadius + prevOuterRadius) / 2;
                                Thickness = prevOuterRadius - newInnerRadius;
                            }
                        }
                }

                // radius handle
                using (var chchk = new EditorGUI.ChangeCheckScope())
                {
                    arcHandleRadius.radius = Radius;
                    arcHandleRadius.DrawHandle();
                    if (chchk.changed)
                    {
                        Undo.RecordObject(shape, "edit disc radius");
                        Radius = arcHandleRadius.radius;
                    }
                }


                // angle handles
                if (shape is Disc disc)
                    if (disc.HasSector && editInnerOuterRadius == false)
                    {
                        arcHandleEnd.angle = disc.AngRadiansEnd * Mathf.Rad2Deg;
                        arcHandleStart.angle = disc.AngRadiansStart * Mathf.Rad2Deg;

                        foreach (var arcHandle in new[] { arcHandleStart, arcHandleEnd })
                        {
                            var radius = Radius;
                            if (ThicknessSpace == ThicknessSpace.Meters && HasThickness)
                                radius += Thickness / 2f;
                            arcHandle.radius = radius;
                            arcHandle.wireframeColor = Color.clear;
                            arcHandle.radiusHandleSizeFunction = pos => 0f; // no radius handle

                            using (var chchk = new EditorGUI.ChangeCheckScope())
                            {
                                arcHandle.DrawHandle();
                                if (chchk.changed)
                                {
                                    Undo.RecordObject(shape, "edit disc angle");
                                    //disc.Radius = arcHandle.radius;
                                    if (arcHandle == arcHandleEnd)
                                        disc.AngRadiansEnd = arcHandle.angle * Mathf.Deg2Rad;
                                    else
                                        disc.AngRadiansStart = arcHandle.angle * Mathf.Deg2Rad;
                                }
                            }
                        }
                    }
            }

            return false;
        }
    }
}