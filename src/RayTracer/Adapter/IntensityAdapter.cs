namespace RayTracer.Adapter
{
    using Common.Numeric;
    using RayTracingLib;
    using RayTracingLib.Light;
    using RayTracingLib.Traceable;

    public class IntensityAdapter : IAdapter<Intensity>
    {
        public void Init(List<ITraceable> sceneObjects) { }

        public Intensity Adapt(DirectionalLight light, HitResult? hitResult)
        {
            if (hitResult is null) return Intensity.Background;
            return Vector3.Dot(-light.Direction, hitResult.Normal);
        }
    }
}
