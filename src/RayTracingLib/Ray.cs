namespace RayTracingLib
{
    using Common.Numeric;

    public class Ray
    {
        public readonly Point3 origin;
        public readonly Vector3 direction;
        public readonly Vector3 invDirection;

        public Ray(Point3 origin, Vector3 direction)
        {
            this.origin = origin;
            this.direction = Vector3.Normalize(direction);
            this.invDirection = 1 / direction;
        }

        public Point3 GetPoint(float distance) =>
            origin + direction * distance;
    }
}
