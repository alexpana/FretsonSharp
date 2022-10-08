using UnityEngine;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes
{
    /// <summary>A Torus shape component</summary>
    [ExecuteAlways]
    [AddComponentMenu("Shapes/Torus")]
    public class Torus : ShapeRenderer
    {
        [SerializeField] private float radius = 1;
        [SerializeField] private float thickness = 0.5f;
        [SerializeField] private ThicknessSpace thicknessSpace = ThicknessSpace.Meters;
        [SerializeField] private ThicknessSpace radiusSpace = ThicknessSpace.Meters;
        // in-editor serialized field, suppressing "assigned but unused field" warning
#pragma warning disable CS0414
        [SerializeField] private AngularUnit angUnitInput = AngularUnit.Degrees;
#pragma warning restore CS0414

        [SerializeField] private float angRadiansStart = 0;
        [SerializeField] private float angRadiansEnd = ShapesMath.TAU;

        /// <summary>The major radius of this torus</summary>
        public float Radius
        {
            get => radius;
            set => SetFloatNow(ShapesMaterialUtils.propRadius, radius = Mathf.Max(0f, value));
        }

        /// <summary>The thickness of this torus</summary>
        public float Thickness
        {
            get => thickness;
            set => SetFloatNow(ShapesMaterialUtils.propThickness, thickness = Mathf.Max(0f, value));
        }

        /// <summary>The space in which Thickness is defined</summary>
        public ThicknessSpace ThicknessSpace
        {
            get => thicknessSpace;
            set => SetIntNow(ShapesMaterialUtils.propThicknessSpace, (int)(thicknessSpace = value));
        }

        /// <summary>The space in which Radius is defined</summary>
        public ThicknessSpace RadiusSpace
        {
            get => radiusSpace;
            set => SetIntNow(ShapesMaterialUtils.propThicknessSpace, (int)(radiusSpace = value));
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

        internal override bool HasDetailLevels => true;

        private protected override void SetAllMaterialProperties()
        {
            SetFloat(ShapesMaterialUtils.propRadius, radius);
            SetFloat(ShapesMaterialUtils.propThickness, thickness);
            SetInt(ShapesMaterialUtils.propThicknessSpace, (int)thicknessSpace);
            SetInt(ShapesMaterialUtils.propRadiusSpace, (int)radiusSpace);
            SetFloat(ShapesMaterialUtils.propAngStart, angRadiansStart);
            SetFloat(ShapesMaterialUtils.propAngEnd, angRadiansEnd);
        }

        private protected override void ShapeClampRanges()
        {
            radius = Mathf.Max(0f, radius);
            thickness = Mathf.Max(0f, thickness);
        }

        private protected override Material[] GetMaterials()
        {
            return new[] { ShapesMaterialUtils.matTorus[BlendMode] };
        }

        private protected override Mesh GetInitialMeshAsset()
        {
            return ShapesMeshUtils.TorusMesh[(int)detailLevel];
        }

        private protected override Bounds GetBounds_Internal()
        {
            if (radiusSpace != ThicknessSpace.Meters)
                return new Bounds(default, Vector3.one);
            // presume 0 world space padding when pixels or noots are used
            var padding = thicknessSpace == ThicknessSpace.Meters ? thickness : 0f;
            var xySize = radius * 2 + padding;
            return new Bounds(Vector3.zero, new Vector3(xySize, xySize, padding));
        }
    }
}