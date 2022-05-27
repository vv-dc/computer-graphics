namespace RayTracingLib.Traceable.KdTree
{
    public class MiddleSplitter : INodeSplitter
    {
        private AABB bounds;

        public void Init(AABB bounds, List<int> primIdxs, List<AABB> primsBounds)
        {
            this.bounds = bounds;
        }

        public bool Split(int axis, out float splitPos)
        {
            splitPos = bounds.Center[axis];
            return true;
        }
    }
}