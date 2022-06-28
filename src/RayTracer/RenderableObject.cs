namespace RayTracer
{
    using RayTracingLib;
    using RayTracingLib.Material;

    public class RenderableObject
    {
        public ITraceable Traceable { get; }

        public IMaterial Material { get; }

        public RenderableObject(ITraceable traceable, IMaterial material)
        {
            Material = material;
            Traceable = traceable;
        }
    }
}
