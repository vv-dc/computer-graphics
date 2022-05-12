namespace RayTracer.Adapter
{
    using RayTracingLib;
    using RayTracingLib.Light;

    public interface IAdapter<PixelType>
    {
        public PixelType Adapt(Light light, HitResult hitResult);
    }
}