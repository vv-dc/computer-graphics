namespace RayTracingLib
{
    using RayTracingLib.Numeric;

    public class Camera
    {
        public readonly int width;
        public readonly int height;
        private float fov;

        public float AspectRatio => width / (float)height;

        private readonly Vector3 point;

        private readonly Vector3 direction;


        public Camera(int width, int height, float fov, Vector3 point, Vector3 direction)
        {
            this.width = width;
            this.height = height;
            this.fov = fov;
            this.point = point;
            this.direction = direction;
        }

        public Ray CastRay(int x, int y)
        {
            float angle = (float)Math.Tan(Math.PI * 0.5f * fov / 180.0);
            float xx = (2 * ((x + 0.5f) / width) - 1) * angle * AspectRatio;
            float yy = (1 - 2 * ((y + 0.5f) / height)) * angle;

            Vector3 rayDir = Vector3.Normalize(new Vector3(xx, yy, -1));
            return new Ray(point, rayDir);
        }
    }
}
