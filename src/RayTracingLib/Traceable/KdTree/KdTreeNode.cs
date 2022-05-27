namespace RayTracingLib.Traceable.KdTree
{
    public class KdTreeNode
    {
        private Axis? splitAxis;
        private float splitPos;
        private List<int>? primIdxs;
        private KdTreeNode? below;
        private KdTreeNode? above;

        public List<int>? PrimIdxs { get => primIdxs; }
        public Axis? SplitAxis { get => splitAxis; }
        public float SplitPos { get => splitPos; }
        public KdTreeNode? Below { get => below; }
        public KdTreeNode? Above { get => above; }

        public KdTreeNode(Axis? splitAxis, float splitPos, List<int>? primIdxs, KdTreeNode? below, KdTreeNode? above)
        {
            this.splitAxis = splitAxis;
            this.splitPos = splitPos;
            this.primIdxs = primIdxs;
            this.below = below;
            this.above = above;
        }

        public bool IsLeaf => below is null && above is null;

        public static KdTreeNode InitLeaf(List<int> primIdxs) =>
            new(null, 0, primIdxs, null, null);

        public static KdTreeNode InitInterior(
            Axis splitAxis, float splitPos, KdTreeNode below, KdTreeNode above
        ) =>
            new(splitAxis, splitPos, null, below, above);
    }
}