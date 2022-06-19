namespace RayTracingLib.Light
{
    using Common.Numeric;

    public class EnvironmentalLight : Light
    {
        public EnvironmentalLight(Color color, float intensity = 1.0f) : base(color, intensity) { }

        public override LightShading ComputeShading(HitResult hitResult)
        {
            return new LightShading()
            {
                direction = hitResult.Normal + Vector3.GetRandomOnHemisphere(hitResult.Normal),
                color = color * intensity,
                distance = float.PositiveInfinity
            };
        }

        public override Color ComputeIntensity(Vector3 wi)
        {
            return color * intensity;
        }
    }
}
