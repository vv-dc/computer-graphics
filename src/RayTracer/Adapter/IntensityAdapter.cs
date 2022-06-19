namespace RayTracer.Adapter
{
    using Common.Numeric;
    using RayTracingLib;
    using RayTracingLib.Light;

    public class IntensityAdapter : IAdapter<Intensity>
    {
        public void Init(List<RenderableObject> sceneObjects) { }

        public Intensity Adapt(Light light, HitResult? hitResult)
        {
            if (hitResult is null) return Intensity.Background;
            var shading = light.ComputeShading(hitResult);
            return Vector3.Dot(-shading.direction, hitResult.Normal);
        }
    }
}
