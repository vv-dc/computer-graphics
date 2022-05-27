namespace RayTracingLib.Traceable.KdTree
{
    public class MiddleNodeSplitter : INodeSplitter
    {
        public float Split(int axis, AABB bounds, List<int> primIdxs, List<AABB> primsBounds) =>
            bounds.Center[axis];
    }
}