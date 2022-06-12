namespace RayTracingLib.Traceable.KdTree
{
    public interface INodeSplitter
    {
        void Init(AABB bounds, List<int> primIdxs, List<AABB> primsBounds);

        bool Split(int axis, out float splitPos);
    }
}
