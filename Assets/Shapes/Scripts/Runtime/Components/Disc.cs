using System;
using UnityEngine;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes
{
    /// <summary>A Disc/Ring/Pie/Arc shape component</summary>
    [ExecuteAlways]
    [AddComponentMenu("Shapes/Disc")]
    public partial class Disc : ShapeRenderer, IDashable
    {
        /// <summary>Various color modes for disc shapes</summary>
        public enum DiscColorMode
        {
            /// <summary>A single color across the entire disc</summary>
            Single,

            /// <summary>A color gradient with one color in the center, and one color on the outer perimeter</summary>
            Radial,

            /// <summary>An angular color gradient</summary>
            Angular,

            /// <summary>A color gradient that is both angular and radial</summary>
            Bilinear
        }

        [SerializeField] private DiscType type = DiscType.Disc;

        [SerializeField] private DiscColorMode colorMode = DiscColorMode.Single;

        [SerializeField] [ShapesColorField(true)]
        private Color colorOuterStart = Color.white;

        [SerializeField] [ShapesColorField(true)]
        private Color colorInnerEnd = Color.white;

        [SerializeField] [ShapesColorField(true)]
        private Color colorOuterEnd = Color.white;

        [SerializeField] private DiscGeometry geometry = DiscGeometry.Flat2D;

        // in-editor serialized field, suppressing "assigned but unused field" warning
#pragma warning disable CS0414
        [SerializeField] private AngularUnit angUnitInput = AngularUnit.Degrees;
#pragma warning restore CS0414

        [SerializeField] private float angRadiansStart = 0;
        [SerializeField] private float angRadiansEnd = ShapesMath.TAU * (3 / 8f);
        [SerializeField] private float radius = 1;
        [SerializeField] private ThicknessSpace radiusSpace = ThicknessSpace.Meters;
        [SerializeField] private float thickness = 0.5f;
        [SerializeField] private ThicknessSpace thicknessSpace = ThicknessSpace.Meters;
        [SerializeField] private ArcEndCap arcEndCaps = ArcEndCap.None;

        /// <summary>Returns true if this is a ring or an arc</summary>
        public bool HasThickness => type.HasThickness();

        /// <summary>Returns true if this is a pie or an arc</summary>
        public bool HasSector => type.HasSector();

        /// <summary>Get or set the type of this disc. Either a solid disc, ring, pie or an arc</summary>
        public DiscType Type
        {
            get => type;
            set
            {
                type = value;
                UpdateMaterial();
                ApplyProperties();
            }
        }

        /// <summary>The color gradient mode to use for this shape</summary>
        public DiscColorMode ColorMode
        {
            get => colorMode;
            set
            {
                colorMode = value;
                ApplyProperties();
            }
        }

        /// <summary>The color of this shape. The alpha channel is used for opacity/intensity in all blend modes</summary>
        public override Color Color
        {
            get => color;
            set
            {
                SetColor(ShapesMaterialUtils.propColor, color = value);
                SetColor(ShapesMaterialUtils.propColorOuterStart, colorOuterStart = value);
                SetColor(ShapesMaterialUtils.propColorInnerEnd, colorInnerEnd = value);
                SetColorNow(ShapesMaterialUtils.propColorOuterEnd, colorOuterEnd = value);
            }
        }

        /// <summary>Get or set the inner color at the start angle of the bilinear color mode</summary>
        public Color ColorInnerStart
        {
            get => color;
            set => SetColorNow(ShapesMaterialUtils.propColor, color = value);
        }

        /// <summary>Get or set the outer color at the start angle of the bilinear color mode</summary>
        public Color ColorOuterStart
        {
            get => colorOuterStart;
            set => SetColorNow(ShapesMaterialUtils.propColorOuterStart, colorOuterStart = value);
        }

        /// <summary>Get or set the outer color at the end angle of the bilinear color mode</summary>
        public Color ColorInnerEnd
        {
            get => colorInnerEnd;
            set => SetColorNow(ShapesMaterialUtils.propColorInnerEnd, colorInnerEnd = value);
        }

        /// <summary>Get or set the outer color at the end angle of the bilinear color mode</summary>
        public Color ColorOuterEnd
        {
            get => colorOuterEnd;
            set => SetColorNow(ShapesMaterialUtils.propColorOuterEnd, colorOuterEnd = value);
        }

        /// <summary>Get or set the outer color when using the radial color mode</summary>
        public Color ColorOuter
        {
            get => ColorOuterStart;
            set
            {
                SetColor(ShapesMaterialUtils.propColorOuterStart, colorOuterStart = value);
                SetColorNow(ShapesMaterialUtils.propColorOuterEnd, colorOuterEnd = value);
            }
        }

        /// <summary>Get or set the inner color when using the radial color mode</summary>
        public Color ColorInner
        {
            get => color;
            set
            {
                SetColor(ShapesMaterialUtils.propColor, color = value);
                SetColorNow(ShapesMaterialUtils.propColorInnerEnd, colorInnerEnd = value);
            }
        }

        /// <summary>Get or set the start angle color when using the angular color mode</summary>
        public Color ColorStart
        {
            get => base.Color;
            set
            {
                SetColor(ShapesMaterialUtils.propColor, color = value);
                SetColorNow(ShapesMaterialUtils.propColorOuterStart, colorOuterStart = value);
            }
        }

        /// <summary>Get or set the end angle color when using the angular color mode</summary>
        public Color ColorEnd
        {
            get => colorInnerEnd;
            set
            {
                SetColor(ShapesMaterialUtils.propColorInnerEnd, colorInnerEnd = value);
                SetColorNow(ShapesMaterialUtils.propColorOuterEnd, colorOuterEnd = value);
            }
        }

        /// <summary>Get or set the type of geometry used (flat or billboarded)</summary>
        public DiscGeometry Geometry
        {
            get => geometry;
            set => SetIntNow(ShapesMaterialUtils.propAlignment, (int)(geometry = value));
        }

        /// <summary>Get or set the start angle (in radians) of pies and arcs</summary>
        public float AngRadiansStart
        {
            get => angRadiansStart;
            set => SetFloatNow(ShapesMaterialUtils.propAngStart, angRadiansStart = value);
        }

        /// <summary>Get or set the end angle (in radians) of pies and arcs</summary>
        public float AngRadiansEnd
        {
            get => angRadiansEnd;
            set => SetFloatNow(ShapesMaterialUtils.propAngEnd, angRadiansEnd = value);
        }

        /// <summary>
        ///     Get or set the radius of this shape in the given RadiusSpace. For arcs and rings, this radius is the distance
        ///     from the origin of the ring to the center of the thickness of the arc/ring
        /// </summary>
        public float Radius
        {
            get => radius;
            set => SetFloatNow(ShapesMaterialUtils.propRadius, radius = Mathf.Max(0f, value));
        }

        /// <summary>Get or set the space in which radius is defined</summary>
        public ThicknessSpace RadiusSpace
        {
            get => radiusSpace;
            set => SetIntNow(ShapesMaterialUtils.propRadiusSpace, (int)(radiusSpace = value));
        }

        /// <summary>this property is obsolete I'm sorry, this was a typo! please use Thickness instead!</summary>
        [Obsolete("this property is obsolete, this was a typo! please use Thickness instead!", true)]
        public float RadiusInner
        {
            get => Thickness;
            set => Thickness = value;
        }

        /// <summary>Get or set the thickness or rings and arcs, in the given ThicknessSpace</summary>
        public float Thickness
        {
            get => thickness;
            set
            {
                SetFloatNow(ShapesMaterialUtils.propThickness, thickness = Mathf.Max(0f, value));
                if (HasThickness && dashed && dashStyle.space == DashSpace.Relative)
                    SetAllDashValues(true);
            }
        }

        /// <summary>Get or set the space in which thickness is defined for rings and arcs</summary>
        public ThicknessSpace ThicknessSpace
        {
            get => thicknessSpace;
            set => SetIntNow(ShapesMaterialUtils.propThicknessSpace, (int)(thicknessSpace = value));
        }

        /// <summary>Get or set the type of end caps to use for arcs</summary>
        public ArcEndCap ArcEndCaps
        {
            get => arcEndCaps;
            set => SetIntNow(ShapesMaterialUtils.propRoundCaps, (int)(arcEndCaps = value));
        }

        internal override bool HasDetailLevels => false;

        private protected override void SetAllMaterialProperties()
        {
            SetInt(ShapesMaterialUtils.propAlignment, (int)geometry);
            SetFloat(ShapesMaterialUtils.propRadius, radius);
            SetInt(ShapesMaterialUtils.propRadiusSpace, (int)radiusSpace);
            SetFloat(ShapesMaterialUtils.propThickness, thickness);
            SetInt(ShapesMaterialUtils.propThicknessSpace, (int)thicknessSpace);
            SetInt(ShapesMaterialUtils.propRoundCaps, (int)arcEndCaps);
            SetFloat(ShapesMaterialUtils.propAngStart, angRadiansStart);
            SetFloat(ShapesMaterialUtils.propAngEnd, angRadiansEnd);
            switch (ColorMode)
            {
                case DiscColorMode.Single:
                    SetColor(ShapesMaterialUtils.propColorOuterStart, base.Color);
                    SetColor(ShapesMaterialUtils.propColorInnerEnd, base.Color);
                    SetColor(ShapesMaterialUtils.propColorOuterEnd, base.Color);
                    break;
                case DiscColorMode.Radial:
                    SetColor(ShapesMaterialUtils.propColorOuterStart, ColorOuterStart);
                    SetColor(ShapesMaterialUtils.propColorInnerEnd, base.Color);
                    SetColor(ShapesMaterialUtils.propColorOuterEnd, ColorOuterStart);
                    break;
                case DiscColorMode.Angular:
                    SetColor(ShapesMaterialUtils.propColorOuterStart, base.Color);
                    SetColor(ShapesMaterialUtils.propColorInnerEnd, ColorInnerEnd);
                    SetColor(ShapesMaterialUtils.propColorOuterEnd, ColorInnerEnd);
                    break;
                case DiscColorMode.Bilinear:
                    SetColor(ShapesMaterialUtils.propColorOuterStart, ColorOuterStart);
                    SetColor(ShapesMaterialUtils.propColorInnerEnd, ColorInnerEnd);
                    SetColor(ShapesMaterialUtils.propColorOuterEnd, ColorOuterEnd);
                    break;
            }

            SetAllDashValues(false);
        }

#if UNITY_EDITOR
        private protected override void ShapeClampRanges()
        {
            radius = Mathf.Max(0f, radius); // disallow negative radius
            thickness = Mathf.Max(0f, thickness); // disallow negative inner radius
            if (matchDashSpacingToSize == false) // this is a lil scary but I think it's okay it's going to be okay
                DashSpacing = DashSpace == DashSpace.FixedCount
                    ? Mathf.Clamp01(DashSpacing)
                    : Mathf.Max(0f, DashSpacing);
        }
#endif

        private protected override Material[] GetMaterials()
        {
            return new[] { ShapesMaterialUtils.GetDiscMaterial(type)[BlendMode] };
        }

        private protected override Bounds GetBounds_Internal()
        {
            if (radiusSpace != ThicknessSpace.Meters)
                return new Bounds(Vector3.zero, Vector3.one);
            // presume 0 world space padding when pixels or noots are used
            var padding = thicknessSpace == ThicknessSpace.Meters ? thickness * .5f : 0f;
            var apothem = HasThickness ? radius + padding : radius;
            var size = apothem * 2;
            return new Bounds(Vector3.zero, new Vector3(size, size, 0f));
        }
    }
}