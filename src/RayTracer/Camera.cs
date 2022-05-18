namespace RayTracer
{
    using RayTracingLib;
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
            float fovScale = (float)Math.Tan(Consts.RadToDeg * fov / 2);

            // from -1 to 1, square, origin in the center
            float xxScreen = (2 * ((x + 0.5f) / width) - 1);
            float yyScreen = (1 - 2 * ((y + 0.5f) / height));

            float xx = xxScreen * fovScale * AspectRatio;
            float yy = yyScreen * fovScale;

            var rayDir = new Vector3(xx, yy, -1); // TODO: change -1
            return new Ray(point, rayDir);
        }
    }
}
