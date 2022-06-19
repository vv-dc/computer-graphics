namespace RayTracingLib.Light
{
    using Common.Numeric;

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
            var difference = hitPoint - point;
            var direction = new Vector3(difference.X, difference.Y, difference.Z);

            var sqrRadius = direction.LengthSquared();
            var radius = (float)Math.Sqrt(sqrRadius);

            return new LightShading()
            {
                direction = direction / radius,
                color = GetLightIntensity(sqrRadius),
                distance = radius
            };
        }

        public override Color ComputeIntensity(Vector3 wi)
        {
            var direction = wi - new Vector3(point.X, point.Y, point.Z);
            var sqrDistance = direction.LengthSquared();
            return GetLightIntensity(sqrDistance);
        }

        private Color GetLightIntensity(float sqrRadius) {
            var lightIntensity = intensity / (4 * MathF.PI * sqrRadius);
            return color * lightIntensity;
        }
    }
}
