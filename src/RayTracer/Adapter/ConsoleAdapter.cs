namespace RayTracer.Adapter
{
    using RayTracingLib;
    using RayTracingLib.Numeric;
    using RayTracingLib.Light;

    public class ConsoleAdapter : IAdapter<double?>
    {
        public double? Adapt(DirectionalLight light, HitResult hitResult)
        {
            float value = -Vector3.Dot(light.Direction, hitResult.Normal);
            return value;
        }
    }
}
