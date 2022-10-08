using System;
using UnityEngine;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes
{
    /// <summary>A Cone shape component</summary>
    [ExecuteAlways]
    [AddComponentMenu("Shapes/Cone")]
    public class Cone : ShapeRenderer
    {
        [SerializeField] private float radius = 1;
        [SerializeField] private float length = 1.5f;
        [SerializeField] private ThicknessSpace sizeSpace = ThicknessSpace.Meters;
        [SerializeField] private bool fillCap = true;

        /// <summary>The radius of the base of the cone</summary>
        public float Radius
        {
            get => radius;
            set => SetFloatNow(ShapesMaterialUtils.propRadius, radius = Mathf.Max(0f, value));
        }

        /// <summary>The length/height of this cone</summary>
        public float Length
        {
            get => length;
            set => SetFloatNow(ShapesMaterialUtils.propLength, length = Mathf.Max(0f, value));
        }

        /// <summary>this property is obsolete I'm sorry! this was a typo, please use SizeSpace instead!</summary>
        [Obsolete("this property is obsolete I'm sorry! this was a typo, please use SizeSpace instead!", true)]
        public ThicknessSpace RadiusSpace
        {
            get => SizeSpace;
            set => SizeSpace = value;
        }

        /// <summary>The space in which radius and length is defined in</summary>
        public ThicknessSpace SizeSpace
        {
            get => sizeSpace;
            set => SetIntNow(ShapesMaterialUtils.propSizeSpace, (int)(sizeSpace = value));
        }

        /// <summary>Whether or not the base cap should be filled</summary>
        public bool FillCap
        {
            get => fillCap;
            set
            {
                fillCap = value;
                UpdateMesh(true);
            }
        }

        internal override bool HasDetailLevels => true;
        internal override bool HasScaleModes => false;

        private protected override void SetAllMaterialProperties()
        {
            SetFloat(ShapesMaterialUtils.propRadius, radius);
            SetFloat(ShapesMaterialUtils.propLength, length);
            SetInt(ShapesMaterialUtils.propSizeSpace, (int)sizeSpace);
        }

        private protected override void ShapeClampRanges()
        {
            radius = Mathf.Max(0f, radius);
            length = Mathf.Max(0f, length);
        }

        private protected override Material[] GetMaterials()
        {
            return new[] { ShapesMaterialUtils.matCone[BlendMode] };
        }

        private protected override Mesh GetInitialMeshAsset()
        {
            return fillCap
                ? ShapesMeshUtils.ConeMesh[(int)detailLevel]
                : ShapesMeshUtils.ConeMeshUncapped[(int)detailLevel];
        }

        private protected override Bounds GetBounds_Internal()
        {
            if (sizeSpace != ThicknessSpace.Meters)
                return new Bounds(Vector3.zero, Vector3.one);
            return new Bounds(Vector3.zero, new Vector3(radius * 2, radius * 2, length));
        }
    }
}