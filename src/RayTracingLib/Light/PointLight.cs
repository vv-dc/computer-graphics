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
            var difference = point - hitPoint;
            var direction = new Vector3(difference.X, difference.Y, difference.Z);

            var sqrDistance = direction.LengthSquared();
            var distance = (float)Math.Sqrt(sqrDistance);
            var lightIntensity = intensity / (4 * MathF.PI * sqrDistance);

            return new LightShading()
            {
                direction = direction / distance,
                color = color * lightIntensity,
                distance = distance
            };
        }

        public override Color ComputeIntensity(Vector3 wi)
        {
            return Color.Black;
        }
    }
}
