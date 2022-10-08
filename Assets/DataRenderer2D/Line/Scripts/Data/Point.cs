using System;
using UnityEngine;

namespace geniikw.DataRenderer2D
{
    /// <summary>
    ///     define each point
    /// </summary>
    [Serializable]
    public struct Point
    {
        public Vector3 position;
        public Vector3 previousControlOffset;
        public Vector3 nextControlOffset;

        [Range(0, 100)] public float width;
        //todo : move normalVector from LineOption to Point.
        //public float normal;

        public Point(Vector3 pos, Vector3 next, Vector3 prev, float width = 2)
        {
            position = pos;
            previousControlOffset = prev;
            nextControlOffset = next;

            this.width = width;
        }

        public Vector3 PreviousControlPoisition => previousControlOffset + position;

        public Vector3 NextControlPosition => nextControlOffset + position;

        public static Point Zero => new Point(Vector3.zero, Vector3.zero, Vector3.zero);
    }
}