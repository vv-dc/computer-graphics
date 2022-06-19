namespace RayTracer.Adapter
{
    using Common.Numeric;
    using RayTracingLib;
    using RayTracingLib.Light;

    public class IntensityAdapter : IAdapter<Intensity>
    {
        public void Init(List<RenderableObject> sceneObjects) { }

        public Intensity Adapt(List<Light> lights, HitResult? hitResult)
        {
            if (hitResult is null) return Intensity.Background;
            var intensity = 0.0f;

            foreach (var light in lights)
            {
                var shading = light.ComputeShading(hitResult);
                intensity += Vector3.Dot(shading.direction, hitResult.Normal);
            }

            return intensity;
        }
    }
}
