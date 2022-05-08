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

        public bool Intersect(Ray ray, out float distance)
        {
            var direction = ray.origin - center;

            var halfB = Vector3.Dot(ray.direction, direction);
            var C = direction.LengthSquared() - radius * radius;

            var D = halfB * halfB - C;
            if (D < -EPS)
            {
                distance = 0;
                return false;
            }
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
                    if (root1 < 0)
                    {
                        distance = 0;
                        return false;
                    }
                }
                distance = root1;
            }
            else
            {
                distance = -halfB;
            }
            // GetNormal() => ray.GetPoint(distance) - center;
            return true;
        }

    }
}