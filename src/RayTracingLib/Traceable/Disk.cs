namespace RayTracingLib.Traceable
{
    using Common.Numeric;

    public class Disk : ITraceable, ITransformable
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
            HitResult? planeHit = null;
            hitResult = null;
            if (plane.Intersect(ray, out planeHit))
            {
                var point = ray.GetPoint(planeHit!.distance);
                if ((point - plane.Point).LengthSquared() < radius * radius)
                {
                    hitResult = new HitResult()
                    {
                        distance = planeHit.distance,
                        ray = ray,
                        Normal = -plane.Normal
                    };
                    return true;
                }
            }
            return false;
        }

        public void Transform(Matrix4x4 matrix)
        {
            plane.Transform(matrix);
            radius *= Vector3.Min(matrix.ExtractScale()); // TODO: find a projection of scale on the disk plane
        }
    }
}
