namespace RayTracingLib.Traceable
{
    using RayTracingLib.Numeric;

    public class Cylinder : ITraceable
    {
        private Point3 point1;
        private Point3 point2;
        private float radius;

        public Point3 Point1 { get => point1; }
        public Point3 Point2 { get => point2; }
        public float Radius { get => radius; }

        public Cylinder(Point3 point1, Point3 point2, float radius)
        {
            this.point1 = point1;
            this.point2 = point2;
            this.radius = radius;
        }

        public bool Intersect(Ray ray, out HitResult? hitResult)
        {
            // Console.WriteLine($"\nOrigin: {ray.origin}, Direction: {ray.direction}");
            hitResult = null;

            var h = point2 - point1;
            var hLen2 = h.LengthSquared();
            if (hLen2 < Consts.EPS)
            {
                return false;
            }
            var point0 = ray.origin;
            var l = point0 - point1;
            var d = ray.direction;

            var lh = Vector3.Dot(l, h);
            var dh = Vector3.Dot(d, h);
            var invHLen2 = 1 / hLen2;

            var A = d.LengthSquared() - dh * dh * invHLen2;
            var halfB = Vector3.Dot(d, l) - lh * dh * invHLen2;
            var C = l.LengthSquared() - lh * lh * invHLen2 - radius * radius;

            var D = halfB * halfB - A * C;
            if (D < Consts.EPS)
            {
                return false;
            }
            var sqrtD = (float)Math.Sqrt(D);

            var root1 = -(halfB - sqrtD) / A;
            var root2 = -(halfB + sqrtD) / A;

            if (root1 > root2)
            {
                (root1, root2) = (root2, root1);
            }
            // Console.WriteLine($"Root1: {root1}, Root2: {root2}");
            var alpha1 = (lh + root1 * dh) * invHLen2; // ?
            var alpha2 = (lh + root2 * dh) * invHLen2; // ?

            var outside = alpha1 >= 0 && alpha1 <= 1;
            var inside = alpha2 >= 0 && alpha2 <= 1;

            float distance = 0, alpha = 0, direction = 0;
            Action setOutside = () =>
            {
                alpha = alpha1;
                distance = root1;
                direction = 1;
            };
            Action setInside = () =>
            {
                alpha = alpha2;
                distance = root2;
                direction = -1;
            };

            // Console.WriteLine($"Outside: {outside} ({alpha1}), Inside: {inside} ({alpha2})");
            if (!outside && !inside) return false;

            if (outside && !inside)
            {
                if (root1 < Consts.EPS) return false;
                setOutside();
            }
            else if (!outside && inside)
            {
                if (root2 < Consts.EPS) return false;
                setInside();
            }
            else
            {
                if (root1 < Consts.EPS)
                {
                    if (root2 < Consts.EPS) return false;
                    setInside();
                }
                else setOutside();
            }
            hitResult = new HitResult()
            {
                distance = distance,
                ray = ray,
                Normal = (l - h * alpha + d * distance) * direction,
            };
            return true;
        }
        public void Transform(Matrix4x4 matrix)
        {
            point1 = matrix * point1;
            point2 = matrix * point2;
            radius *= Vector3.Min(matrix.ExtractScale()); // TODO: the same as for disk
        }
    }
}