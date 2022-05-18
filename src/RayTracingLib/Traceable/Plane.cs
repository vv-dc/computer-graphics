namespace RayTracingLib.Traceable
{
    using Numeric;
    public class Plane : ITraceable
    {
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
            if (dot > Consts.EPS)
            {
                var direction = ray.origin - point;
                var distance = -Vector3.Dot(normal, direction) / dot;
                if (distance > 0)
                {
                    hitResult = new HitResult() { distance = distance, Normal = -normal };
                    return true;
                }
            }
            hitResult = null;
            return false;
        }
    }
}