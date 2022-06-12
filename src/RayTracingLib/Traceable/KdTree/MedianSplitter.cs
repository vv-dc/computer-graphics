namespace RayTracingLib.Traceable.KdTree
{
    using Common;
    public class MedianSplitter : INodeSplitter
    {
        private List<int> primIdxs;
        private List<AABB> primsBounds;
        private int middleIdx;

        public void Init(AABB bounds, List<int> primIdxs, List<AABB> primsBounds)
        {
            this.primIdxs = primIdxs;
            this.primsBounds = primsBounds;
            middleIdx = primIdxs.Count / 2;
        }

        public bool Split(int axis, out float splitPos)
        {
            var centers = primIdxs.Select(idx => primsBounds[idx].Center[axis]).ToArray();
            splitPos = centers.NthSmallestElement(middleIdx);
            return true;
        }
    }
}
