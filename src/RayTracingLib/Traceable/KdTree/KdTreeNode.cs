namespace RayTracingLib.Traceable.KdTree
{
    public class KdTreeNode
    {
        private Axis? splitAxis;
        private float splitPos;
        private List<int>? primIdxs;
        private int idx;
        private int above;

        public Axis? SplitAxis { get => splitAxis; }
        public float SplitPos { get => splitPos; }
        public List<int>? PrimIdxs { get => primIdxs; }
        public int Idx { get => idx; }
        public int Above { get => above; set => above = value; }

        public KdTreeNode(Axis? splitAxis, float splitPos, List<int>? primIdxs, int idx, int above)
        {
            this.splitAxis = splitAxis;
            this.splitPos = splitPos;
            this.primIdxs = primIdxs;
            this.idx = idx;
            this.above = above;
        }

        public bool IsLeaf => primIdxs is not null;

        public static KdTreeNode InitLeaf(List<int> primIdxs) =>
            new(null, 0, primIdxs, -1, -1);

        public static KdTreeNode InitInterior(Axis splitAxis, float splitPos, int idx) =>
            new(splitAxis, splitPos, null, idx, -1);
    }
}
