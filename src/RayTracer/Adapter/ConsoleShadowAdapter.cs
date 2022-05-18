namespace RayTracer.Adapter
{
    using RayTracer.Tracer;
    using RayTracingLib;
    using RayTracingLib.Numeric;
    using RayTracingLib.Light;
    using RayTracingLib.Traceable;

    public class ConsoleShadowAdapter : IAdapter<Intensity>
    {
        private ITracer shadowTracer;

        public ConsoleShadowAdapter(ITracer shadowTracer)
        {
            this.shadowTracer = shadowTracer;
        }

        public void Init(List<ITraceable> sceneObjects)
        {
            shadowTracer.Init(sceneObjects);
        }

        public Intensity Adapt(DirectionalLight light, HitResult? hitResult)
        {
            if (hitResult is null) return Intensity.Background;
            var intensity = Vector3.Dot(-light.Direction, hitResult.Normal);

            var hitPoint = hitResult.ray.GetPoint(hitResult.distance) + hitResult.Normal * Consts.EPS;
            var shadowRay = new Ray(hitPoint, -light.Direction);
            var hit = shadowTracer.Trace(shadowRay, out var shadowHitResult);

            return hit ? 0 : intensity;
        }
    }
}
