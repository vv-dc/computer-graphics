namespace RayTracer.Adapter
{
    using RayTracingLib;
    using RayTracingLib.Light;
    using RayTracingLib.Traceable;

    public interface IAdapter<PixelType>
    {
        public PixelType Adapt(DirectionalLight light, HitResult? hitResult);

        void Init(List<ITraceable> sceneObjects);
    }
}
