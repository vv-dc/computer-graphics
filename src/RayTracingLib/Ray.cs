namespace RayTracingLib
{
    using Numeric;
    public class Ray
    {
        public readonly Vector3 origin;
        public readonly Vector3 direction;

        public Ray(Vector3 origin, Vector3 direction)
        {
            this.origin = origin;
            this.direction = Vector3.Normalize(direction);
        }

        public Vector3 GetPoint(float distance) =>
            origin + direction * distance;
    }
}