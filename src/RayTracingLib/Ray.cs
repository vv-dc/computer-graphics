namespace RayTracingLib
{
    using Numeric;
    public class Ray
    {
        public readonly Point3 origin;
        public readonly Vector3 direction;

        public Ray(Point3 origin, Vector3 direction)
        {
            this.origin = origin;
            this.direction = Vector3.Normalize(direction);
        }

        public Point3 GetPoint(float distance) =>
            origin + direction * distance;
    }
}