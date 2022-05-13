namespace RayTracer.Adapter
{
    using RayTracingLib;
    using RayTracingLib.Light;

    public interface IAdapter<PixelType>
    {
        public PixelType Adapt(DirectionalLight light, HitResult hitResult);
    }
}
