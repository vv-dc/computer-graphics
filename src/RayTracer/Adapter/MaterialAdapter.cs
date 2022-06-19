namespace RayTracer.Adapter
{
    using Common;
    using Common.Numeric;
    using RayTracingLib;
    using RayTracingLib.Light;
    using RayTracer.Tracer;

    public class MaterialAdapter : IAdapter<Color>
    {
        private const int REFLECT_TRASHOLD = 9;

        private const float REFLECT_BIAS = 0.00001f;

        private ITracer tracer;

        private ITracer shadowTracer;

        public MaterialAdapter(ITracer tracer, ITracer shadowTracer)
        {
            this.tracer = tracer;
            this.shadowTracer = shadowTracer;
        }

        public void Init(List<RenderableObject> sceneObjects) { }

        public Color Adapt(Light light, HitResult? hitResult)
        {
            if (hitResult is null) return Color.Steel; // background
            return ComposeLighting(hitResult, light, REFLECT_TRASHOLD);
        }

        private Color ComposeLighting(HitResult hitResult, Light light, int depth)
        {
            return GetDiffuseLighting(hitResult, light) + GetReflectLighting(hitResult, light, depth);
        }

        private Color GetDiffuseLighting(HitResult hitResult, Light light)
        {
            var shading = light.ComputeShading(hitResult);
            var direction = hitResult.ray.direction; // it's not reflected, so wi = wo
            var color = hitResult.material.Diffuse(direction, direction);
            var shadowMultiplier = GetShadowMultiplier(hitResult, shading);
            return color * shadowMultiplier * shading.color;
        }

        private float GetShadowMultiplier(HitResult hitResult, LightShading shading)
        {
            var intensity = Vector3.Dot(-shading.direction, hitResult.Normal);
            var hitPoint = hitResult.ray.GetPoint(hitResult.distance) + hitResult.Normal * Consts.SHADOW_EPS;
            var shadowRay = new Ray(hitPoint, -shading.direction);
            var hit = shadowTracer.Trace(shadowRay, out var shadowHitResult);
            return hit ? 0 : Math.Max(intensity, 0);
        }

        private Color GetReflectLighting(HitResult hitResult, Light light, int depth)
        {
            var composed = Color.Black;
            if (depth == 0) return composed;

            var attenuation = hitResult.material.Scatter(hitResult, out var wi);
            composed = ReflectRay(hitResult, wi, light, depth) * attenuation;

            return hitResult.material.Color * composed;
        }

        private Color ReflectRay(HitResult hitResult, Vector3 wi, Light light, int depth)
        {
            var dot = Vector3.Dot(hitResult.Normal, wi);
            var sign = Math.Sign(dot);

            var point = hitResult.ray.GetPoint(hitResult.distance);
            var next = point + hitResult.Normal * sign * REFLECT_BIAS;
            var nextRay = new Ray(next, wi);

            if (tracer.Trace(nextRay, out var nextHit))
            {
                return ComposeLighting(nextHit!, light, depth - 1);
            }

            return hitResult.material.Color * Math.Max(dot, 0);
        }
    }
}
