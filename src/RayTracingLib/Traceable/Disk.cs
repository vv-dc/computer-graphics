namespace RayTracingLib.Traceable
{
    using Numeric;
    public class Disk : ITraceable
    {
        private Vector3 normal;
        private Vector3 center;
        private float radius;
        private Plane plane;

        public Disk(Vector3 normal, Vector3 center, float radius)
        {
            this.normal = normal;
            this.center = center;
            this.radius = radius;
            plane = new Plane(normal, center);
        }

        public bool Intersect(Ray ray, out float distance)
        {
            if (plane.Intersect(ray, out distance))
            {
                var point = ray.GetPoint(distance);
                if ((point - center).LengthSquared() < radius * radius)
                {
                    return true;
                }
            }
            distance = 0;
            // GetNormal() => -normal;
            return false;
        }
    }
}