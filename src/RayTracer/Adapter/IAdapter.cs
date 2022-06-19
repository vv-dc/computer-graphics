namespace RayTracer.Adapter
{
    using RayTracingLib;
    using RayTracingLib.Light;
    using RayTracingLib.Traceable;

    public interface IAdapter<PixelType>
    {
        public PixelType Adapt(List<Light> lights, HitResult? hitResult);

        void Init(List<RenderableObject> sceneObjects);
    }
}
