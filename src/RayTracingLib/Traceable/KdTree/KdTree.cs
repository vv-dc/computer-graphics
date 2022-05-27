namespace RayTracingLib.Traceable.KdTree
{
    using Common.Timer;

    public class KdTree : ITraceable
    {
        private List<ITreeTraceable> prims;
        private AABB bounds;
        private KdTreeNode root;
        private INodeSplitter splitter;
        private int maxDepth;
        private int maxPrims;

        public KdTree(List<ITreeTraceable> prims, INodeSplitter splitter, int maxDepth = int.MaxValue, int maxPrims = 1)
        {
            this.prims = prims;
            this.splitter = splitter;
            this.maxDepth = maxDepth;
            this.maxPrims = maxPrims;
            Timer.LogTime(BuildTree, "Tree build");
        }

        private void BuildTree()
        {
            var primIdxs = Enumerable.Range(0, prims.Count).ToList();

            List<AABB> primsBounds = new();
            foreach (var prim in prims)
            {
                var primBounds = prim.GetAABB();
                bounds = bounds is null
                    ? primBounds
                    : AABB.Union(bounds, primBounds);

                primsBounds.Add(primBounds);
            }
            root = BuildBranch(bounds!, primIdxs, primsBounds);
        }

        private KdTreeNode BuildBranch(AABB bounds, List<int> primIdxs, List<AABB> primsBounds, int depth = 0)
        {
            if (primIdxs.Count <= maxPrims || depth >= maxDepth)
                return KdTreeNode.InitLeaf(primIdxs);

            var axis = (int)bounds.MaximumExtent();
            var splitPos = splitter.Split(axis, bounds, primIdxs, primsBounds);

            List<int> belowIdxs = new(), aboveIdxs = new();
            foreach (var idx in primIdxs)
            {
                if (primsBounds[idx].Min[axis] <= splitPos)
                {
                    belowIdxs.Add(idx);
                    if (primsBounds[idx].Max[axis] > splitPos) aboveIdxs.Add(idx);
                }
                else aboveIdxs.Add(idx);
            }

            var belowBounds = (AABB)bounds.Clone();
            belowBounds.Max[axis] = splitPos;

            var aboveBounds = (AABB)bounds.Clone();
            aboveBounds.Min[axis] = splitPos;

            var nextNode = KdTreeNode.InitInterior(
                (Axis)axis,
                splitPos,
                BuildBranch(belowBounds, belowIdxs, primsBounds, depth + 1),
                BuildBranch(aboveBounds, aboveIdxs, primsBounds, depth + 1)
            );
            return nextNode;
        }

        public bool Intersect(Ray ray, out HitResult? hitResult)
        {
            float tmin, tmax;
            hitResult = null;

            if (!bounds.IntersectP(ray, out tmin, out tmax))
                return false;

            var next = new Stack<(KdTreeNode, float, float)>();
            var node = root;
            while (node is not null)
            {
                if (node.IsLeaf)
                {
                    foreach (var idx in node.PrimIdxs!)
                        if (prims[idx].Intersect(ray, out var currentHit))
                        {
                            if (currentHit?.distance >= hitResult?.distance)
                                continue;
                            hitResult = currentHit;
                        }
                    if (next.Count > 0)
                        (node, tmin, tmax) = next.Pop();
                    else break;
                }
                else
                {
                    var axis = (int)node.SplitAxis!;
                    var tplane = (node.SplitPos - ray.origin[axis]) * ray.invDirection[axis];

                    var belowFirst = (
                        ray.origin[axis] < node.SplitPos! || (ray.origin[axis] == node.SplitPos! && ray.direction[axis] <= 0)
                    );
                    var (firstNode, secondNode) = belowFirst
                        ? (node.Below!, node.Above!)
                        : (node.Above!, node.Below!);

                    if (tplane > tmax || tplane <= 0)
                        node = firstNode;
                    else if (tmin > tplane)
                        node = secondNode;
                    else
                    {
                        next.Push((secondNode, tplane, tmax));
                        tmax = tplane;
                        node = firstNode;
                    }
                }
            }
            return hitResult is not null;
        }
    }
}
