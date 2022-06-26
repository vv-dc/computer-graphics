namespace RayTracingLib.Traceable.KdTree
{
    using Common.Timer;

    public class KdTree : ITraceable
    {
        private const int MIN_NODES = 1024;
        private const int SPLIT_RETRIES = 2; // from 0 to 2

        private List<ITreeTraceable> prims;
        private AABB bounds;
        private List<KdTreeNode> nodes;
        private INodeSplitter splitter;
        private int maxDepth;
        private int maxPrims;

        public KdTree(List<ITreeTraceable> prims, INodeSplitter splitter, int maxDepth = int.MaxValue, int maxPrims = 8)
        {
            this.prims = prims;
            this.splitter = splitter;

            this.maxDepth = Math.Min(maxDepth, DefaultMaxDepth(prims.Count));
            this.maxPrims = Math.Min(maxPrims, DefaultMaxPrims());

            Timer.LogTime(BuildTree, "Tree build");
        }

        private int DefaultMaxDepth(int count) => (int)(8 + 1.3f * MathF.Log2(count));

        private int DefaultMaxPrims() => 8;

        private void BuildTree()
        {
            var idxs = Enumerable.Range(0, prims.Count).ToList();

            List<AABB> primsBounds = new(prims.Count);
            foreach (var prim in prims)
            {
                var primBounds = prim.GetAABB();
                bounds = bounds is null
                    ? primBounds
                    : AABB.Union(bounds, primBounds);

                primsBounds.Add(primBounds);
            }

            nodes = new List<KdTreeNode>(MIN_NODES);
            var nextFreeNode = -1;

            var next = new Stack<(AABB, List<int>, int, int)>(maxDepth);
            next.Push((bounds, idxs, -1, 0));

            while (next.Count > 0)
            {
                var (bounds, primIdxs, parent, depth) = next.Pop();
                ++nextFreeNode;

                if (parent != -1)
                    nodes[parent].Above = nextFreeNode;

                if (primIdxs.Count <= maxPrims || depth >= maxDepth)
                {
                    nodes.Add(KdTreeNode.InitLeaf(primIdxs));
                    continue;
                }

                splitter.Init(bounds, primIdxs, primsBounds);
                var axis = (int)bounds.MaximumExtent();
                var splitted = splitter.Split(axis, out var splitPos);

                if (!splitted && SPLIT_RETRIES > 0)
                {
                    for (int retry = 0; retry < SPLIT_RETRIES; ++retry)
                    {
                        axis = (axis + 1) % 3;
                        splitted = splitter.Split(axis, out splitPos);
                        if (splitted) break;
                    }
                }
                if (!splitted)
                {
                    nodes.Add(KdTreeNode.InitLeaf(primIdxs));
                    continue;
                }

                List<int> belowIdxs = new(primIdxs.Count), aboveIdxs = new(primIdxs.Count);
                foreach (var idx in primIdxs)
                {
                    if (primsBounds[idx].Min[axis] < splitPos)
                    {
                        belowIdxs.Add(idx);
                        if (primsBounds[idx].Max[axis] > splitPos) aboveIdxs.Add(idx);
                    }
                    else aboveIdxs.Add(idx);
                }

                AABB belowBounds = (AABB)bounds.Clone(), aboveBounds = (AABB)bounds.Clone();
                belowBounds.Max[axis] = splitPos; aboveBounds.Min[axis] = splitPos;

                next.Push((aboveBounds, aboveIdxs, nextFreeNode, depth + 1));
                next.Push((belowBounds, belowIdxs, -1, depth + 1));

                nodes.Add(KdTreeNode.InitInterior((Axis)axis, splitPos, nextFreeNode));
            }
        }

        public bool Intersect(Ray ray, out HitResult? hitResult)
        {
            float tmin, tmax;
            hitResult = null;

            if (!bounds.IntersectP(ray, out tmin, out tmax))
                return false;

            var next = new Stack<(KdTreeNode, float, float)>(maxDepth);
            var node = nodes[0];
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
                    var (firstIdx, secondIdx) = belowFirst
                        ? (node.Idx + 1, node.Above)
                        : (node.Above, node.Idx + 1);

                    var (firstNode, secondNode) = (nodes[firstIdx], nodes[secondIdx]);
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
