namespace RayTracingLib.Traceable
{
    using Numeric;
    public class Disk : ITraceable
    {
        private Vector3 normal;
        private Point3 center;
        private float radius;
        private Plane plane;

        public Disk(Vector3 normal, Point3 center, float radius)
        {
            this.normal = normal;
            this.center = center;
            this.radius = radius;
            plane = new Plane(normal, center);
        }

        public bool Intersect(Ray ray, out HitResult? hitResult)
        {
            if (plane.Intersect(ray, out hitResult))
            {
                var point = ray.GetPoint(hitResult!.distance);
                if ((point - center).LengthSquared() < radius * radius)
                {
                    hitResult.ray = ray;
                    hitResult.Normal -= normal;
                    return true;
                }
            }
            hitResult = null;
            return false;
        }
    }
}