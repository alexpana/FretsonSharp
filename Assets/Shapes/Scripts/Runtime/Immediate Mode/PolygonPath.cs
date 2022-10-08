using UnityEngine;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes
{
    public class PolygonPath : PointPath<Vector2>
    {
        private PolygonTriangulation lastUsedTriangulationMode = PolygonTriangulation.EarClipping;

        public PolygonPath()
        {
            _ = 0;
        }

        public void AddPoint(float x, float y)
        {
            AddPoint(new Vector2(x, y));
        }

        public bool EnsureMeshIsReadyToRender(PolygonTriangulation triangulation, out Mesh outMesh)
        {
            if (meshDirty == false) // polygon itself didn't change, but the render state might force us to update
                if (triangulation != lastUsedTriangulationMode)
                    meshDirty = true;

            return base.EnsureMeshIsReadyToRender(out outMesh, () => { TryUpdateMesh(triangulation); });
        }

        private void TryUpdateMesh(PolygonTriangulation triangulation)
        {
            lastUsedTriangulationMode = triangulation;
            // todo: be smarter about this, maybe don't mesh.clear but check point count and whatnot
            ShapesMeshGen.GenPolygonMesh(mesh, path, triangulation);
        }


        // todo: don't make this duplicate code pls
        // unify these functions for PolygonPath and PolylinePath

        #region BezierTo, ArcTo

        /// <summary>
        ///     Adds points of a cubic bezier curve, using the previous point as the starting point
        /// </summary>
        public void BezierTo(Vector2 startTangent, Vector2 endTangent, Vector2 end, int pointCount)
        {
            if (CheckCanAddContinuePoint()) return;
            AddPoints(ShapesMath.CubicBezierPointsSkipFirst(LastPoint, startTangent, endTangent, end, pointCount));
        }

        /// <summary>
        ///     A cubic bezier curve, using the previous point as the starting point. Number of points is given by density in
        ///     number of points per full 360° turn
        /// </summary>
        public void BezierTo(Vector2 startTangent, Vector2 endTangent, Vector2 end, float pointsPerTurn)
        {
            var sampleCount = ShapesConfig.Instance.polylineBezierAngularSumAccuracy * 2 + 1;
            var curveSumDeg = ShapesMath.GetApproximateCurveSum(LastPoint, startTangent, endTangent, end, sampleCount);
            var angSpanTurns = curveSumDeg / 360f;
            var pointCount = Mathf.Max(2,
                Mathf.RoundToInt(angSpanTurns * ShapesConfig.Instance.polylineDefaultPointsPerTurn));
            BezierTo(startTangent, endTangent, end, pointCount);
        }

        /// <summary>
        ///     Adds points of an arc wedged into the corner defined by the previous point, corner, and next, with the given point
        ///     density in number of points per full 360° turn
        /// </summary>
        public void ArcTo(Vector2 corner, Vector2 next, float radius, float pointsPerTurn)
        {
            if (CheckCanAddContinuePoint()) return;
            AddArcPoints(corner, next, radius, true, 0, pointsPerTurn);
        }

        /// <summary>
        ///     Adds points of an arc wedged into the corner defined by the previous point, corner, and next, with the given point
        ///     count
        /// </summary>
        public void ArcTo(Vector2 corner, Vector2 next, float radius, int pointCount)
        {
            if (CheckCanAddContinuePoint()) return;
            AddArcPoints(corner, next, radius, false, pointCount, 0);
        }

        /// <summary>
        ///     Adds points of an arc wedged into the corner defined by the previous point, corner, and next
        /// </summary>
        public void ArcTo(Vector2 corner, Vector2 next, float radius)
        {
            if (CheckCanAddContinuePoint()) return;
            AddArcPoints(corner, next, radius, true, 0, ShapesConfig.Instance.polylineDefaultPointsPerTurn);
        }

        /// <summary>
        ///     Adds points of an arc wedged into the corner defined by the previous point, corner, and next, with the given point
        ///     density in number of points per full 360° turn
        /// </summary>
        public void ArcTo(Vector2 corner, Vector2 next, float radius, float pointsPerTurn, Color color)
        {
            if (CheckCanAddContinuePoint()) return;
            AddArcPoints(corner, next, radius, true, 0, pointsPerTurn);
        }

        private void AddArcPoints(Vector2 corner, Vector2 next, float radius, bool useDensity, int targetPointCount,
            float pointsPerTurn)
        {
            if (radius <= 0.0001f)
            {
                // radius is super small, just add the corner point
                AddPoint(corner);
                return; // pretty much just a straight line. only add the corner point
            }

            var tangentA = (corner - LastPoint).normalized;
            var tangentB = (next - corner).normalized;
            var dot = Vector2.Dot(tangentA, tangentB);

            if (dot > 0.999f)
            {
                AddPoint(corner);
                return; // pretty much just a straight line. only add the corner point
            }

            var normA = ShapesMath.Rotate90CW(tangentA); // normalized
            var normB = ShapesMath.Rotate90CW(tangentB);
            var cornerDir = (normA + normB).normalized;
            var cornerBDot = Vector2.Dot(cornerDir, normB);
            var center = corner + cornerDir * (radius / cornerBDot);
            // calc count here if density based
            if (useDensity)
            {
                var angTurn = Vector2.Angle(normA, normB) / 360f;
                targetPointCount = Mathf.RoundToInt(angTurn * pointsPerTurn);
            }

            AddPoints(ShapesMath.GetArcPoints(-normA, -normB, center, radius, targetPointCount));
        }

        #endregion
    }
}