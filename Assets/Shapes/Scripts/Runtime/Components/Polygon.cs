﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes
{
    /// <summary>A Polygon shape component</summary>
    [ExecuteAlways]
    [AddComponentMenu("Shapes/Polygon")]
    public partial class Polygon : ShapeRenderer, IFillable
    {
	    /// <summary>
	    ///     <remarks>
	    ///         IMPORTANT: if you modify this list, you need to set meshOutOfDate to true, otherwise your changes won't
	    ///         apply
	    ///     </remarks>
	    /// </summary>
	    [FormerlySerializedAs("polyPoints")] [SerializeField]
        public List<Vector2> points = new()
        {
            new(1f, 0f),
            new(0.5f, 0.86602545f),
            new(-0.5f, 0.8660254f),
            new(-1f, 0f),
            new(-0.5f, -0.86602545f),
            new(0.5f, -0.86602545f)
        };

        // also called alignment
        [SerializeField] private PolygonTriangulation triangulation = PolygonTriangulation.EarClipping;

#if UNITY_EDITOR
        /// <summary>"Please use points instead of PolyPoints - this one is deprecated"</summary>
        [Obsolete("Please use " + nameof(points) + " instead - this one is deprecated", true)]
        public List<Vector2> PolyPoints => points;
#endif
        /// <summary>What triangulation mode to use for this polygon</summary>
        public PolygonTriangulation Triangulation
        {
            get => triangulation;
            set
            {
                triangulation = value;
                meshOutOfDate = true;
            }
        }

        /// <summary>The number of points in this polygon</summary>
        public int Count => points.Count;

        /// <summary>Get or set a polygon point by index</summary>
        public Vector2 this[int i]
        {
            get => points[i];
            set
            {
                points[i] = value;
                meshOutOfDate = true;
            }
        }

        private protected override bool UseCamOnPreCull => true;

        internal override bool HasScaleModes => false;
        internal override bool HasDetailLevels => false;
        private protected override MeshUpdateMode MeshUpdateMode => MeshUpdateMode.SelfGenerated;

        /// <summary>Set a polygon point position by index</summary>
        public void SetPointPosition(int index, Vector2 position)
        {
            if (index < 0 || index >= Count) throw new IndexOutOfRangeException();
            points[index] = position;
            meshOutOfDate = true;
        }

        /// <summary>Sets all points of this polygon to the input points</summary>
        public void SetPoints(IEnumerable<Vector2> points)
        {
            this.points.Clear();
            AddPoints(points);
        }

        /// <summary>Sets all points of this polygon to the input points</summary>
        public void AddPoints(IEnumerable<Vector2> points)
        {
            this.points.AddRange(points);
            meshOutOfDate = true;
        }

        /// <summary>Adds a point to this polygon</summary>
        public void AddPoint(Vector2 point)
        {
            points.Add(point);
            meshOutOfDate = true;
        }

        internal override void CamOnPreCull()
        {
            if (meshOutOfDate)
            {
                meshOutOfDate = false;
                UpdateMesh(true);
            }
        }

        private protected override void SetAllMaterialProperties()
        {
            SetFillProperties();
        }

        private protected override Material[] GetMaterials()
        {
            return new[] { ShapesMaterialUtils.matPolygon[BlendMode] };
        }

        private protected override void GenerateMesh()
        {
            ShapesMeshGen.GenPolygonMesh(Mesh, points, triangulation);
        }

        private protected override Bounds GetBounds_Internal()
        {
            if (points.Count < 2)
                return default;
            var min = Vector3.one * float.MaxValue;
            var max = Vector3.one * float.MinValue;
            foreach (Vector3 pt in points)
            {
                min = Vector3.Min(min, pt);
                max = Vector3.Max(max, pt);
            }

            return new Bounds((max + min) * 0.5f, max - min);
        }

#if UNITY_EDITOR
        [ContextMenu("Reverse Points [DEBUG ONLY]")]
        private void ReversePoints()
        {
            points.Reverse();
            meshOutOfDate = true;
        }

        [ContextMenu("Shift Points Forward [DEBUG ONLY]")]
        private void ShiftForward()
        {
            var newPts = new List<Vector2>();
            newPts.Add(points[points.Count - 1]);
            for (var i = 0; i < points.Count - 1; i++)
                newPts.Add(points[i]);
            points = newPts;
            meshOutOfDate = true;
        }
#endif
    }
}