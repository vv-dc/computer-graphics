namespace RayTracingLib.Traceable.KdTree
{
    public class MedianSplitter : INodeSplitter
    {
        private List<int> primIdxs;
        private List<AABB> primsBounds;

        public void Init(AABB bounds, List<int> primIdxs, List<AABB> primsBounds)
        {
            this.primIdxs = primIdxs;
            this.primsBounds = primsBounds;
        }

        public bool Split(int axis, out float splitPos)
        {
            splitPos = primIdxs.Select(idx => primsBounds[idx].Center[axis]).Average();
            return true;
        }
    }
}

// float numPrims = primIdxs.Count;
// splitPos = 0;
// foreach (var idx in primIdxs)
//     splitPos += primsBounds[idx].Center[axis] / numPrims;
