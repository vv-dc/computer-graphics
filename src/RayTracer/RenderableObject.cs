namespace RayTracer
{
    using RayTracingLib;
    using RayTracingLib.Material;

    public class RenderableObject
    {
        public ITraceable Traceable { get; }

        public Material Material { get; }

        public RenderableObject(ITraceable traceable, Material material)
        {
            Material = material;
            Traceable = traceable;
        }
    }
}
