namespace RayTracingLib.Traceable
{
    using Common;
    using Common.Numeric;

    public class AABB : ITraceable, ICloneable
    {
        private Point3 min;
        private Point3 max;

        public Point3 Min { get => min; }
        public Point3 Max { get => max; }

        public AABB(Point3 min, Point3 max)
        {
            this.min = min;
            this.max = max;
        }

        public Point3 Center => min + Size / 2;

        public Vector3 Size => max - min;

        public Axis MaximumExtent()
        {
            var size = Size;
            return (size.X > size.Y && size.X > size.Z)
                ? Axis.X
                : (size.Y > size.Z ? Axis.Y : Axis.Z);
        }

        public float SurfaceArea()
        {
            var size = Size;
            return 2 * (size.X * size.Y + size.Y * size.Z + size.Z * size.X);
        }

        public bool Intersect(Ray ray, out HitResult? hitResult)
        {
            hitResult = null;
            if (IntersectP(ray, out var tmin, out var tmax))
            {
                var distance = tmin < Consts.EPS ? tmax : Math.Min(tmin, tmax);
                hitResult = new HitResult()
                {
                    distance = distance,
                    ray = ray,
                    Normal = ComputeNormal(ray.GetPoint(distance))
                };
                return true;
            }
            return false;
        }

        // TODO: find better solution than https://stackoverflow.com/a/16876601/15955690
        private Vector3 ComputeNormal(Point3 point)
        {
            var local = point - Center;
            var size = Size;

            var min = Math.Abs(size.X - Math.Abs(local.X));
            Vector3 normal = new(Math.Sign(local.X), 0, 0);

            var distance = Math.Abs(size.Y - Math.Abs(local.Y));
            if (distance < min)
            {
                min = distance;
                normal = new(0, Math.Sign(local.Y), 0);
            }
            if (Math.Abs(size.Z - Math.Abs(local.Z)) < min)
                normal = new(0, 0, Math.Sign(local.Z));

            return normal;
        }

        public bool IntersectP(Ray ray, out float tmin, out float tmax)
        {
            var tmins = (min - ray.origin) * ray.invDirection;
            var tmaxs = (max - ray.origin) * ray.invDirection;

            tmin = Vector3.Max(Vector3.Min(tmins, tmaxs));
            tmax = Vector3.Min(Vector3.Max(tmins, tmaxs));

            if (tmax < Consts.EPS || tmin > tmax) return false;
            return true;
        }

        public object Clone() => new AABB((Point3)min.Clone(), (Point3)max.Clone());

        public static AABB Union(AABB left, AABB right) =>
            new(
                Point3.Min(left.Min, right.Min),
                Point3.Max(left.Max, right.Max)
            );
    }
}

// var (ymin, ymax) = invDir.Y >= 0 ? (min.Y, max.Y) : (max.Y, min.Y);
// var tymin = (ymin - origin.Y) * invDir.Y;
// var tymax = (ymax - origin.Y) * invDir.Y;

// if (tmin > tymax || tymin > tmax) return false;

// tmin = Math.Min(tmin, tymin);
// tmax = Math.Max(tmax, tymax);

// var (zmin, zmax) = invDir.Z >= 0 ? (min.Z, max.Z) : (max.Z, min.Z);
// var tzmin = (zmin - origin.Z) * invDir.Z;
// var tzmax = (zmax - origin.Z) * invDir.Z;

// if (tmin > tzmax || tzmin > tmax) return false;

// tmin = Math.Min(tmin, tzmin);
// tmax = Math.Max(tmax, tzmax);
