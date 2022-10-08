using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.DataRenderer2D
{
    /// <summary>
    ///     data container of spline.
    ///     define struct to use animator.
    ///     and define ISpline to use like pointer.
    /// </summary>
    [Serializable]
    public partial struct Spline : IEnumerable<Point>
    {
        public enum LineMode
        {
            SplineMode,
            BezierMode
        }

        [SerializeField] private LineMode mode;

        [SerializeField] private LinePair pair;

        [SerializeField] private List<Point> points;

        public MonoBehaviour owner;

        public LineOption option;

        public static Spline Default =>
            new Spline
            {
                points = new List<Point> { Point.Zero, new(Vector3.right * 10, Vector3.zero, Vector3.zero) },
                mode = LineMode.SplineMode,
                pair = new LinePair(Point.Zero, new Point(Vector3.right, Vector3.zero, Vector3.zero), 0, 1, 0, 1),
                option = LineOption.Default
            };

        public float AllLength
        {
            get
            {
                var length = 0f;
                foreach (var pair in AllPair)
                    length += CurveLength.Auto(pair[0], pair[1]);

                return length;
            }
        }

        public float Length
        {
            get
            {
                var length = 0f;
                foreach (var pair in TargetPairList)
                    length = pair.Length;

                return length;
            }
        }

        public IEnumerator<Point> GetEnumerator()
        {
            if (mode == LineMode.BezierMode)
            {
                yield return pair.n0;
                yield return pair.n1;
            }
            else
            {
                foreach (var p in points)
                    yield return p;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public event Action EditCallBack;

        private Point GetFirstPoint()
        {
            if (mode == LineMode.BezierMode)
                return pair.n0;

            if (points.Count < 1)
                throw new Exception("need more point");

            return points[0];
        }

        private Point GetLastPoint()
        {
            if (mode == LineMode.BezierMode)
                return pair.n1;

            if (points.Count < 1)
                throw new Exception("need more point");

            return points[points.Count - 1];
        }

        private int GetCount()
        {
            if (mode == LineMode.BezierMode)
                return 2;

            return points.Count;
        }

        public Vector3 GetPosition(float ratio)
        {
            ratio = Mathf.Clamp01(ratio);

            var cl = ratio * Length;

            foreach (var pair in TargetPairList)
                if (cl > pair.Length)
                    cl -= pair.Length;
                else
                    return pair.GetPoisition(cl / pair.Length);

            return option.mode == LineOption.Mode.Loop ? GetFirstPoint().position : GetLastPoint().position;
        }

        public Vector3 GetDirection(float ratio)
        {
            ratio = Mathf.Clamp01(ratio);
            var cl = ratio * Length;
            var dir = Vector3.zero;
            foreach (var pair in TargetPairList)
            {
                dir = pair.GetDirection(cl / pair.Length);
                if (cl > pair.Length)
                    cl -= pair.Length;
                else
                    break;
            }

            return dir;
        }

        public IEnumerable<Point> TripleEnumerator()
        {
            if (mode == LineMode.BezierMode)
            {
                yield return pair.n0;
                yield return pair.n1;
            }
            else
            {
                foreach (var p in points)
                    yield return p;

                if (option.mode == LineOption.Mode.Loop) yield return points[0];
            }
        }
    }
}