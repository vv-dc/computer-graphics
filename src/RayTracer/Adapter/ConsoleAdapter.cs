namespace RayTracer.Adapter
{
    using RayTracingLib;
    using RayTracingLib.Numeric;
    using RayTracingLib.Light;
    using RayTracingLib.Traceable;

    public class ConsoleAdapter : IAdapter<Intensity>
    {
        public void Init(List<ITraceable> sceneObjects) { }

        public Intensity Adapt(DirectionalLight light, HitResult? hitResult)
        {
            if (hitResult is null) return Intensity.Background;
            return Vector3.Dot(-light.Direction, hitResult.Normal);
        }
    }
}
