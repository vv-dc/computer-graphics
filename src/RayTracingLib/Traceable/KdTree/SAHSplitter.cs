namespace RayTracingLib.Traceable.KdTree
{
    using Common.Numeric;

    // https://pbr-book.org/3ed-2018/Primitives_and_Intersection_Acceleration/Kd-Tree_Accelerator
    // https://habr.com/ru/post/312882/
    public class SAHSplitter : INodeSplitter
    {
        private float isectCost;
        private float traversalCost;
        private float emptyBonus; // from 0 to 1
        private int numSplits;

        private AABB bounds;
        private List<int> primIdxs;
        private List<AABB> primsBounds;

        private float invTotalSA;
        private Vector3 size;
        private float parentCost;

        public SAHSplitter(float isectCost = 1, float traversalCost = 1.5f, float emptyBonus = 0, int numSplits = 33)
        {
            this.isectCost = isectCost;
            this.traversalCost = traversalCost;
            this.emptyBonus = emptyBonus;
            this.numSplits = numSplits;
        }

        public void Init(AABB bounds, List<int> primIdxs, List<AABB> primsBounds)
        {
            this.bounds = bounds;
            this.primIdxs = primIdxs;
            this.primsBounds = primsBounds;

            invTotalSA = 1 / bounds.SurfaceArea();
            size = bounds.Size;
            parentCost = isectCost * primIdxs.Count;
        }

        public bool Split(int axis, out float splitPos)
        {
            var splitSize = size[axis] / numSplits;
            var (nsBelow, nsAbove) = ComputeAboveBelowCounts(axis, splitSize);

            var bestCost = float.PositiveInfinity;
            float bestOffset = 0;

            var axis1 = (axis + 1) % 3;
            var axis2 = (axis + 2) % 3;

            var axesSA = size[axis1] * size[axis2];
            var lenSum = size[axis1] + size[axis2];

            for (int idx = 0; idx < numSplits - 1; ++idx)
            {
                var offset = (idx + 1) * splitSize;

                var nBelow = primIdxs.Count - nsAbove[idx + 1];
                var nAbove = primIdxs.Count - nsBelow[idx];

                var pBelow = 2 * (axesSA + offset * lenSum) * invTotalSA;
                var pAbove = 2 * (axesSA + (size[axis] - offset) * lenSum) * invTotalSA;

                var bonus = (nBelow == 0 || nAbove == 0) ? emptyBonus : 0;
                var cost = traversalCost + isectCost * (1 - bonus) * (pBelow * nBelow + pAbove * nAbove);

                if (cost < bestCost)
                {
                    bestCost = cost;
                    bestOffset = offset;
                }
            }
            if (bestCost > 4 * parentCost)
            {
                splitPos = 0;
                return false;
            }
            splitPos = bounds.Min[axis] + bestOffset;
            return true;
        }

        private (int[], int[]) ComputeAboveBelowCounts(int axis, float splitSize)
        {
            var nsBelow = new int[numSplits];
            var nsAbove = new int[numSplits];

            var minBound = bounds.Min[axis];
            // binning
            foreach (var idx in primIdxs)
            {
                var primBounds = primsBounds[idx];

                var minIdx = Math.Clamp((int)((primBounds.Min[axis] - minBound) / splitSize), 0, numSplits - 1); // iLow
                var maxIdx = Math.Clamp((int)((primBounds.Max[axis] - minBound) / splitSize), 0, numSplits - 1); // iHigh

                nsBelow[maxIdx] += 1; // aHigh
                nsAbove[minIdx] += 1; // aLow
            }
            // prefix and postfix sums
            for (int idx = 0; idx < numSplits - 1; ++idx)
            {
                nsBelow[idx + 1] += nsBelow[idx];
                nsAbove[numSplits - (idx + 2)] += nsAbove[numSplits - (idx + 1)];
            }
            return (nsBelow, nsAbove);
        }
    }
}