namespace RayTracingLib.Traceable
{
    using Numeric;
    public class Plane : ITraceable
    {
        private const float EPS = 1E-9F;
        private Vector3 normal;
        private Vector3 point;

        public Plane(Vector3 normal, Vector3 point)
        {
            this.normal = normal;
            this.point = point;
        }

        public bool Intersect(Ray ray, out HitResult? hitResult)
        {
            var dot = Vector3.Dot(ray.direction, normal);
            if (dot > EPS)
            {
                var direction = ray.origin - point;
                hitResult = new HitResult();
                hitResult.distance = -Vector3.Dot(normal, direction) / dot;
                if (hitResult.distance >= 0)
                {
                    hitResult.Normal = -normal;
                    return true;
                }
            }
            hitResult = null;
            return false;
        }
    }
}