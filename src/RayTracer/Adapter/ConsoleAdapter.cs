namespace RayTracer.Adapter
{
    using RayTracingLib;
    using RayTracingLib.Numeric;
    using RayTracingLib.Light;

    public class ConsoleAdapter : IAdapter<Intensity>
    {
        public static readonly Intensity background = -3;

        public Intensity Adapt(DirectionalLight light, HitResult? hitResult)
        {
            if (hitResult == null)
            {
                return background;
            }
            return -Vector3.Dot(light.Direction, hitResult.Normal);
        }
    }
}
