namespace RayTracer.Adapter
{
    using RayTracingLib;
    using RayTracingLib.Numeric;
    using RayTracingLib.Light;

    public class ConsoleAdapter : IAdapter<Intensity>
    {
        public Intensity Adapt(DirectionalLight light, HitResult? hitResult)
        {
            if (hitResult is null) return Intensity.Background;
            return Vector3.Dot(-light.Direction, hitResult.Normal);
        }
    }
}
