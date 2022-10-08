using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes
{
    public static class ShapesMath
    {
        // for a similar bunch of extra math functions,
        // check out Mathfs at https://github.com/FreyaHolmer/Mathfs!

        private const MethodImplOptions INLINE = MethodImplOptions.AggressiveInlining;

        public const float TAU = 6.28318530718f;

        [MethodImpl(INLINE)]
        public static float Frac(float x)
        {
            return x - Mathf.Floor(x);
        }

        [MethodImpl(INLINE)]
        public static float Eerp(float a, float b, float t)
        {
            return Mathf.Pow(a, 1 - t) * Mathf.Pow(b, t);
        }

        [MethodImpl(INLINE)]
        public static float SmoothCos01(float x)
        {
            return Mathf.Cos(x * Mathf.PI) * -0.5f + 0.5f;
        }

        [MethodImpl(INLINE)]
        public static Vector2 AngToDir(float angRad)
        {
            return new(Mathf.Cos(angRad), Mathf.Sin(angRad));
        }

        [MethodImpl(INLINE)]
        public static float DirToAng(Vector2 dir)
        {
            return Mathf.Atan2(dir.y, dir.x);
        }

        [MethodImpl(INLINE)]
        public static Vector2 Rotate90CW(Vector2 v)
        {
            return new(v.y, -v.x);
        }

        [MethodImpl(INLINE)]
        public static Vector2 Rotate90CCW(Vector2 v)
        {
            return new(-v.y, v.x);
        }

        [MethodImpl(INLINE)]
        public static Vector4 AtLeast0(Vector4 v)
        {
            return new(Mathf.Max(0, v.x), Mathf.Max(0, v.y), Mathf.Max(0, v.z), Mathf.Max(0, v.w));
        }

        [MethodImpl(INLINE)]
        public static float MaxComp(Vector4 v)
        {
            return Mathf.Max(Mathf.Max(Mathf.Max(v.y, v.x), v.z), v.w);
        }

        [MethodImpl(INLINE)]
        public static bool HasNegativeValues(Vector4 v)
        {
            return v.x < 0 || v.y < 0 || v.z < 0 || v.w < 0;
        }

        [MethodImpl(INLINE)]
        public static float Determinant(Vector2 a, Vector2 b)
        {
            return a.x * b.y - a.y * b.x;
        }

        [MethodImpl(INLINE)]
        public static float Luminance(Color c)
        {
            return c.r * 0.2126f + c.g * 0.7152f + c.b * 0.0722f;
        }

        public static float GetLineSegmentProjectionT(Vector3 a, Vector3 b, Vector3 p)
        {
            var disp = b - a;
            return Vector3.Dot(p - a, disp) / Vector3.Dot(disp, disp);
        }

        [MethodImpl(INLINE)]
        public static PolylinePoint WeightedSum(Vector4 w, PolylinePoint a, PolylinePoint b, PolylinePoint c,
            PolylinePoint d)
        {
            return w.x * a + w.y * b + w.z * c + w.w * d;
        }

        [MethodImpl(INLINE)]
        public static Vector3 WeightedSum(Vector4 w, Vector3 a, Vector3 b, Vector3 c, Vector3 d)
        {
            return w.x * a + w.y * b + w.z * c + w.w * d;
        }

        [MethodImpl(INLINE)]
        public static Vector2 WeightedSum(Vector4 w, Vector2 a, Vector2 b, Vector2 c, Vector2 d)
        {
            return w.x * a + w.y * b + w.z * c + w.w * d;
        }

        [MethodImpl(INLINE)]
        public static Color WeightedSum(Vector4 w, Color a, Color b, Color c, Color d)
        {
            return w.x * a + w.y * b + w.z * c + w.w * d;
        }

        public static bool PointInsideTriangle(Vector2 a, Vector2 b, Vector2 c, Vector2 point, float aMargin = 0f,
            float bMargin = 0f, float cMargin = 0f)
        {
            var d0 = Determinant(Dir(a, b), Dir(a, point));
            var d1 = Determinant(Dir(b, c), Dir(b, point));
            var d2 = Determinant(Dir(c, a), Dir(c, point));
            var b0 = d0 < cMargin;
            var b1 = d1 < aMargin;
            var b2 = d2 < bMargin;
            return b0 == b1 && b1 == b2; // on the same side of all halfspaces, this can only happen inside
        }

        /// <summary>Direction from a to b, skipping intermediate Vector2s for speed</summary>
        [MethodImpl(INLINE)]
        internal static Vector2 Dir(Vector2 a, Vector2 b)
        {
            var dx = b.x - a.x;
            var dy = b.y - a.y;
            var mag = Mathf.Sqrt(dx * dx + dy * dy);
            return new Vector2(dx / mag, dy / mag);
        }

        public static float PolygonSignedArea(List<Vector2> pts)
        {
            var count = pts.Count;
            var sum = 0f;
            for (var i = 0; i < count; i++)
            {
                var a = pts[i];
                var b = pts[(i + 1) % count];
                sum += (b.x - a.x) * (b.y + a.y);
            }

            return sum;
        }

        public static Vector2 Rotate(Vector2 v, float angRad)
        {
            var ca = Mathf.Cos(angRad);
            var sa = Mathf.Sin(angRad);
            return new Vector2(ca * v.x - sa * v.y, sa * v.x + ca * v.y);
        }

        private static float DeltaAngleRad(float a, float b)
        {
            return Mathf.Repeat(b - a + Mathf.PI, TAU) - Mathf.PI;
        }

        public static float InverseLerpAngleRad(float a, float b, float v)
        {
            var angBetween = DeltaAngleRad(a, b);
            b = a + angBetween; // removes any a->b discontinuity
            var h = a + angBetween * 0.5f; // halfway angle
            v = h + DeltaAngleRad(h, v); // get offset from h, and offset by h
            return Mathf.InverseLerp(a, b, v);
        }


        [MethodImpl(INLINE)]
        private static Vector2 Lerp(Vector2 a, Vector2 b, Vector2 t)
        {
            return new(Mathf.Lerp(a.x, b.x, t.x), Mathf.Lerp(a.y, b.y, t.y));
        }

        [MethodImpl(INLINE)]
        public static Vector2 Lerp(Rect r, Vector2 t)
        {
            return new(Mathf.Lerp(r.xMin, r.xMax, t.x), Mathf.Lerp(r.yMin, r.yMax, t.y));
        }

        [MethodImpl(INLINE)]
        private static Vector2 InverseLerp(Vector2 a, Vector2 b, Vector2 v)
        {
            return (v - a) / (b - a);
        }

        [MethodImpl(INLINE)]
        public static Vector2 InverseLerp(Rect r, Vector2 pt)
        {
            return new(Mathf.InverseLerp(r.xMin, r.xMax, pt.x), Mathf.InverseLerp(r.yMin, r.yMax, pt.y));
        }

        [MethodImpl(INLINE)]
        private static Vector2 Remap(Vector2 iMin, Vector2 iMax, Vector2 oMin, Vector2 oMax, Vector2 value)
        {
            return Lerp(oMin, oMax, InverseLerp(iMin, iMax, value));
        }

        [MethodImpl(INLINE)]
        public static Vector2 Remap(Rect iRect, Rect oRect, Vector2 iPos)
        {
            return Remap(iRect.min, iRect.max, oRect.min, oRect.max, iPos);
        }

        public static Vector3 Abs(Vector3 v)
        {
            return new(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
        }

        // source: https://answers.unity.com/questions/421968/normal-distribution-random.html
        public static float RandomGaussian(float min = 0.0f, float max = 1.0f)
        {
            float u;
            float s;
            do
            {
                u = 2f * Random.value - 1f;
                var v = 2f * Random.value - 1f;
                s = u * u + v * v;
            } while (s >= 1f);

            var std = u * Mathf.Sqrt(-2.0f * Mathf.Log(s) / s);
            var mean = (min + max) / 2.0f;
            var sigma = (max - mean) / 3.0f;
            return Mathf.Clamp(std * sigma + mean, min, max);
        }

        public static Vector3 GetRandomPerpendicularVector(Vector3 a)
        {
            Vector3 b;
            do
            {
                b = Random.onUnitSphere;
            } while (Mathf.Abs(Vector3.Dot(a, b)) > 0.98f);

            return b;
        }

        // arc utils
        public static IEnumerable<PolylinePoint> GetArcPoints(PolylinePoint a, PolylinePoint b, Vector3 normA,
            Vector3 normB, Vector3 center, float radius, int count)
        {
            count = Mathf.Max(2, count); // at least 2

            PolylinePoint DirToPt(Vector3 dir, float t)
            {
                var p = t <= 0 ? a : t >= 1 ? b : PolylinePoint.Lerp(a, b, t);
                p.point = center + dir * radius;
                return p;
            }

            yield return DirToPt(normA, 0);
            for (var i = 1; i < count - 1; i++)
            {
                var t = i / (count - 1f);
                yield return DirToPt(Vector3.Slerp(normA, normB, t), t);
            }

            yield return DirToPt(normB, 1);
        }

        public static IEnumerable<Vector3> GetArcPoints(Vector3 normA, Vector3 normB, Vector3 center, float radius,
            int count)
        {
            count = Mathf.Max(2, count); // at least 2

            Vector3 DirToPt(Vector3 dir)
            {
                return center + dir * radius;
            }

            yield return DirToPt(normA);
            for (var i = 1; i < count - 1; i++)
            {
                var t = i / (count - 1f);
                yield return DirToPt(Vector3.Slerp(normA, normB, t));
            }

            yield return DirToPt(normB);
        }

        public static IEnumerable<Vector2> GetArcPoints(Vector2 normA, Vector2 normB, Vector2 center, float radius,
            int count)
        {
            count = Mathf.Max(2, count); // at least 2

            Vector2 DirToPt(Vector2 dir)
            {
                return center + dir * radius;
            }

            yield return DirToPt(normA);
            for (var i = 1; i < count - 1; i++)
            {
                var t = i / (count - 1f);
                yield return DirToPt(Vector3.Slerp(normA, normB, t)); // todo: vec2 slerp?
            }

            yield return DirToPt(normB);
        }

        // bezier utils
        public static IEnumerable<PolylinePoint> CubicBezierPointsSkipFirst(PolylinePoint a, PolylinePoint b,
            PolylinePoint c, PolylinePoint d, int count)
        {
            // skip first point
            for (var i = 1; i < count - 1; i++)
            {
                var t = i / (count - 1f);
                yield return CubicBezier(a, b, c, d, t);
            }

            yield return d;
        }

        public static IEnumerable<PolylinePoint> CubicBezierPointsSkipFirstMatchStyle(PolylinePoint style, Vector3 a,
            Vector3 b, Vector3 c, Vector3 d, int count)
        {
            // skip first point
            for (var i = 1; i < count - 1; i++)
            {
                var t = i / (count - 1f);
                var pp = style;
                pp.point = CubicBezier(a, b, c, d, t);
                yield return pp;
            }

            var ppEnd = style;
            ppEnd.point = d;
            yield return ppEnd;
        }

        public static IEnumerable<Vector3> CubicBezierPointsSkipFirst(Vector3 a, Vector3 b, Vector3 c, Vector3 d,
            int count)
        {
            // skip first point
            for (var i = 1; i < count - 1; i++)
            {
                var t = i / (count - 1f);
                yield return CubicBezier(a, b, c, d, t);
            }

            yield return d;
        }

        public static IEnumerable<Vector2> CubicBezierPointsSkipFirst(Vector2 a, Vector2 b, Vector2 c, Vector2 d,
            int count)
        {
            // skip first point
            for (var i = 1; i < count - 1; i++)
            {
                var t = i / (count - 1f);
                yield return CubicBezier(a, b, c, d, t);
            }

            yield return d;
        }

        public static Vector4 GetCubicBezierWeights(float t)
        {
            var omt = 1f - t;
            var omt2 = omt * omt;
            var t2 = t * t;
            return new Vector4(
                omt2 * omt, // (1-t)³
                3f * omt2 * t, // 3(1-t)²t
                3f * omt * t2, // 3(1-t)t²
                t2 * t // t³
            );
        }

        public static PolylinePoint CubicBezier(PolylinePoint a, PolylinePoint b, PolylinePoint c, PolylinePoint d,
            float t)
        {
            if (t <= 0f) return a;
            if (t >= 1f) return d;
            return WeightedSum(GetCubicBezierWeights(t), a, b, c, d);
        }

        public static Vector3 CubicBezier(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
        {
            if (t <= 0f) return a;
            if (t >= 1f) return d;
            return WeightedSum(GetCubicBezierWeights(t), a, b, c, d);
        }

        public static Vector2 CubicBezier(Vector2 a, Vector2 b, Vector2 c, Vector2 d, float t)
        {
            if (t <= 0f) return a;
            if (t >= 1f) return d;
            return WeightedSum(GetCubicBezierWeights(t), a, b, c, d);
        }

        public static Vector3 CubicBezierDerivative(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
        {
            var omt = 1f - t;
            var omt2 = omt * omt;
            var t2 = t * t;
            return
                a * (-3 * omt2) +
                b * (9 * t2 - 12 * t + 3) +
                c * (6 * t - 9 * t2) +
                d * (3 * t2);
        }

        public static float GetApproximateCurveSum(Vector3 a, Vector3 b, Vector3 c, Vector3 d, int vertCount)
        {
            var tangents = new Vector2[vertCount];
            for (var i = 0; i < vertCount; i++)
            {
                var t = i / (vertCount - 1f);
                tangents[i] = CubicBezierDerivative(a, b, c, d, t);
            }

            var angSum = 0f;
            for (var i = 0; i < vertCount - 1; i++)
                angSum += Vector2.Angle(tangents[i], tangents[i + 1]);

            return angSum;
        }
    }
}