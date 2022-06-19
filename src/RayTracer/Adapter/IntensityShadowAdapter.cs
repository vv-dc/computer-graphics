namespace RayTracer.Adapter
{
    using Common;
    using Common.Numeric;
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
                var current = Vector3.Dot(-shading.direction, hitResult.Normal);

                var hitPoint = hitResult.ray.GetPoint(hitResult.distance) + hitResult.Normal * Consts.SHADOW_EPS;
                var shadowRay = new Ray(hitPoint, -shading.direction);
                var hit = shadowTracer.Trace(shadowRay, out var shadowHitResult);
                intensity += hit ? 0 : current;
            }

            return intensity;
        }
    }
}
