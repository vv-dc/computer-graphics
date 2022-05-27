namespace RayTracingLib.Traceable.KdTree
{
    public interface INodeSplitter
    {
        float Split(int axis, AABB bounds, List<int> primIdxs, List<AABB> primsBounds);
    }
}