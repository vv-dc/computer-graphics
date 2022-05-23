namespace RayTracingLib.Traceable
{
    using Common;
    using Common.Numeric;

    public class Sphere : ITraceable
    {
        private Point3 center;
        private float radius;

        public Point3 Center { get => center; }
        public float Radius { get => radius; }

        public Sphere(Point3 center, float radius)
        {
            this.center = center;
            this.radius = radius;
        }

        public bool Intersect(Ray ray, out HitResult? hitResult)
        {
            var direction = ray.origin - center;
            hitResult = null;

            var halfB = Vector3.Dot(ray.direction, direction);
            var C = direction.LengthSquared() - radius * radius;

            var D = halfB * halfB - C;
            if (D < -Consts.EPS)
            {
                return false;
            }
            float distance;
            if (D > Consts.EPS)
            {
                var sqrtD = MathF.Sqrt(D);

                var root1 = -(halfB - sqrtD);
                var root2 = -(halfB + sqrtD);

                if (root1 > root2)
                {
                    (root1, root2) = (root2, root1);
                }
                if (root1 < Consts.EPS)
                {
                    root1 = root2;
                    if (root1 < Consts.EPS) return false;
                }
                distance = root1;
            }
            else
            {
                distance = -halfB;
                if (distance < Consts.EPS)
                {
                    return false;
                }
            }
            hitResult = new HitResult()
            {
                distance = distance,
                ray = ray,
                Normal = ray.GetPoint(distance) - center,
            };
            return true;
        }

        public void Transform(Matrix4x4 matrix)
        {
            center = matrix * center;
            radius *= Vector3.Min(matrix.ExtractScale());
        }
    }
}
