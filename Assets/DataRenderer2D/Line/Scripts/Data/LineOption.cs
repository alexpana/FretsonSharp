using System;
using UnityEngine;

namespace geniikw.DataRenderer2D
{
    [Serializable]
    public struct LineOption
    {
        public enum LineJointOption
        {
            round,
            intersect
        }

        public enum Mode
        {
            Noraml = 0,
            Loop = 1,
            RoundEdge = 2
        }

        [Range(0, 1)] public float startRatio;

        [Range(0, 1)] public float endRatio;

        public Mode mode;

        [Range(0.1f, 100f)] public float divideLength;

        [Range(5, 180)] public float divideAngle;

        public Gradient color; //class reference type;

        public LineJointOption jointOption;

        public float DivideAngle => Mathf.Clamp(divideAngle, 5, 180);

        public float DivideLength => Mathf.Clamp(divideLength, 0.1f, 100);

        public static LineOption Default =>
            new LineOption
            {
                startRatio = 0f,
                endRatio = 1f,
                mode = Mode.Noraml,
                divideLength = 1f,
                divideAngle = 10f,
                color = new Gradient()
            };
    }
}