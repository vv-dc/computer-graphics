namespace RayTracingLib.Traceable
{
    using Numeric;
    public class Disk : ITraceable
    {
        private Plane plane;
        private float radius;

        public Disk(Vector3 normal, Point3 center, float radius)
        {
            this.radius = radius;
            plane = new Plane(normal, center);
        }

        public bool Intersect(Ray ray, out HitResult? hitResult)
        {
            if (plane.Intersect(ray, out hitResult))
            {
                var point = ray.GetPoint(hitResult!.distance);
                if ((point - plane.Point).LengthSquared() < radius * radius)
                {
                    hitResult.ray = ray;
                    hitResult.Normal -= plane.Normal;
                    return true;
                }
            }
            hitResult = null;
            return false;
        }

        public void Transform(Matrix4x4 matrix)
        {
            plane.Transform(matrix);
            radius *= Vector3.Min(matrix.ExtractScale()); // TODO: find a projection of scale on the disk plane
        }
    }
}