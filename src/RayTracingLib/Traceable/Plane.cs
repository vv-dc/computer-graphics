namespace RayTracingLib.Traceable
{
    using Numeric;
    public class Plane : ITraceable
    {
        private Vector3 normal;
        private Point3 point;

        public Vector3 Normal { get => normal; }
        public Point3 Point { get => point; }

        public Plane(Vector3 normal, Point3 point)
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
                    hitResult = new HitResult()
                    {
                        distance = distance,
                        ray = ray,
                        Normal = -normal
                    };
                    return true;
                }
            }
            hitResult = null;
            return false;
        }

        public void Transform(Matrix4x4 matrix)
        {
            point = matrix * point;
            normal = Vector3.Normalize(matrix * normal);
        }
    }
}