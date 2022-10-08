using UnityEngine;

namespace Battlehub.SplineEditor
{
    public class Greedy : MonoBehaviour
    {
        public void OnFork(ForkEventArgs args)
        {
            var min = float.MaxValue;
            var minIndex = -1;
            if (args.NextCurveIndex != -1) min = args.Spline.EvalCurveLength(args.NextCurveIndex);

            for (var i = 0; i < args.Branches.Length; ++i)
            {
                var branch = args.Branches[i];
                var len = branch.EvalCurveLength(0);
                if (len < min)
                {
                    min = len;
                    minIndex = i;
                }
            }

            args.SelectBranchIndex = minIndex;
        }
    }
}