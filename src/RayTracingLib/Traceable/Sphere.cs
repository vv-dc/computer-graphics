namespace RayTracingLib.Traceable
{
    using Numeric;
    public class Sphere : ITraceable
    {
        private const float EPS = 1E-6F;
        private Vector3 center;
        private float radius;

        public Sphere(Vector3 center, float radius)
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
            if (D < -EPS) return false;

            if (D > EPS)
            {
                var sqrtD = (float)Math.Sqrt(D);

                var root1 = -(halfB - sqrtD);
                var root2 = -(halfB + sqrtD);

                if (root1 > root2)
                {
                    (root1, root2) = (root2, root1);
                }
                if (root1 < 0)
                {
                    root1 = root2;
                    if (root1 < 0) return false;
                }
                hitResult = new HitResult { distance = root1 };
            }
            else
            {
                hitResult = new HitResult { distance = -halfB };
            }
            hitResult.Normal = ray.GetPoint(hitResult.distance) - center;
            return true;
        }
    }
}