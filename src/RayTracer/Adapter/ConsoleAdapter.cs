namespace RayTracer.Adapter
{
    using RayTracingLib;
    using RayTracingLib.Numeric;
    using RayTracingLib.Light;

    public class ConsoleAdapter : IAdapter<double?>
    {
        public double? Adapt(Light light, HitResult hitResult)
        {
            float value = -Vector3.Dot((light as DirectionalLight).Direction, hitResult.Normal);
            return value;
        }
    }
}