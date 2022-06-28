namespace RayTracer.Adapter
{
    using RayTracer.Tracer;
    using RayTracingLib;
    using RayTracingLib.Light;

    public class IntensityShadowAdapter : IAdapter<Intensity>
    {
        private ITracer shadowTracer;

        public IntensityShadowAdapter(ITracer shadowTracer)
        {
            this.shadowTracer = shadowTracer;
        }

        public void Init(List<RenderableObject> sceneObjects)
        {
            shadowTracer.Init(sceneObjects);
        }

        public Intensity Adapt(List<Light> lights, HitResult? hitResult)
        {
            if (hitResult is null) return Intensity.Background;
            var intensity = 0.0f;

            foreach (var light in lights)
            {
                var shading = light.ComputeShading(hitResult);
                var shadowMultiplier = ShadowUtils.ComputeShadowMultiplier(shadowTracer, hitResult, shading);
                intensity += light.intensity * shadowMultiplier;
            }
            return intensity;
        }
    }
}
