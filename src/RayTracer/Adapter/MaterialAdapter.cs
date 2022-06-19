namespace RayTracer.Adapter
{
    using Common;
    using Common.Numeric;
    using RayTracingLib;
    using RayTracingLib.Light;
    using RayTracer.Tracer;

    public class MaterialAdapter : IAdapter<Color>
    {
        private const int REFLECT_THRESHOLD = 6;

        private const float REFLECT_BIAS = 0.00001f;

        private const int SAMPLES_PER_PIXES = 3;

        private ITracer tracer;

        private ITracer shadowTracer;

        public MaterialAdapter(ITracer tracer, ITracer shadowTracer)
        {
            this.tracer = tracer;
            this.shadowTracer = shadowTracer;
        }

        public void Init(List<RenderableObject> sceneObjects)
        {
            shadowTracer.Init(sceneObjects);
            tracer.Init(sceneObjects);
        }

        public Color Adapt(List<Light> lights, HitResult? hitResult)
        {
            if (hitResult is null) return Color.Steel; // background
            var color = Color.Black;

            foreach (var light in lights)
            {
                color += ComposeLighting(hitResult, light, REFLECT_THRESHOLD);
            }

            return color;
        }

        private Color ComposeLighting(HitResult hitResult, Light light, int depth)
        {
            return GetDiffuseLighting(hitResult, light) + GetReflectLighting(hitResult, light, depth);
        }

        private Color GetDiffuseLighting(HitResult hitResult, Light light)
        {
            var shading = light.ComputeShading(hitResult);
            var direction = hitResult.ray.direction;
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

            for (var idx = 0; idx < SAMPLES_PER_PIXES; ++idx)
            {
                var attenuation = hitResult.material.Scatter(hitResult, out var wi);
                if (attenuation > 0)
                    composed += ReflectRay(hitResult, wi, light, depth) * attenuation;
            }

            var shading = light.ComputeShading(hitResult);
            return hitResult.material.Color * composed / SAMPLES_PER_PIXES;
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

            var lightIntensity = light.ComputeIntensity(wi);
            return hitResult.material.Color * lightIntensity;
        }
    }
}
