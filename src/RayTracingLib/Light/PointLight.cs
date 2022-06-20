namespace RayTracingLib.Light
{
    using Common.Numeric;
    using Common;

    public class PointLight : Light
    {
        private Point3 point;

        public PointLight(Point3 point, Color color, float intensity = 1.0f) : base(color, intensity)
        {
            this.point = point;
        }

        public override LightShading ComputeShading(HitResult hitResult)
        {
            var hitPoint = hitResult.ray.GetPoint(hitResult.distance);
            var direction = hitPoint - point;

            var sqrRadius = direction.LengthSquared();
            var radius = MathF.Sqrt(sqrRadius);

            return new LightShading()
            {
                direction = direction / radius,
                color = GetLightIntensity(sqrRadius),
                distance = radius
            };
        }

        public override Color ComputeIntensity(Vector3 wi)
        {
            return Color.Black;
        }

        private Color GetLightIntensity(float sqrRadius)
        {
            var lightIntensity = intensity / (4 * MathF.PI * sqrRadius);
            return color * lightIntensity;
        }
    }
}
