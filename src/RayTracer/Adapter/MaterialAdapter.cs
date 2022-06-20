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

        private const int DIFFUSE_SAMPLES = 4;

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
            if (hitResult is null) return ComposeEnviromentalLighting(lights);
            var color = Color.Black;

            foreach (var light in lights)
            {
                color += ComposeLighting(hitResult, light, REFLECT_THRESHOLD);
            }

            return color;
        }

        private Color ComposeEnviromentalLighting(List<Light> lights)
        {
            var color = Color.Black;
            foreach (var light in lights)
            {
                if (light is not EnvironmentalLight) continue;
                color += light.color * light.intensity;
            }
            return color;
        }

        private Color ComposeLighting(HitResult hitResult, Light light, int depth)
        {
            return ComposeDiffuseLighting(hitResult, light) + ComposeReflectLighting(hitResult, light, depth);
        }

        private Color ComposeDiffuseLighting(HitResult hitResult, Light light)
        {
            var totalColor = Color.Black;
            for (int idx = 0; idx < DIFFUSE_SAMPLES; ++idx)
            {
                totalColor += GetDiffuseLighting(hitResult, light);
            }
            return totalColor / DIFFUSE_SAMPLES;
        }

        private Color GetDiffuseLighting(HitResult hitResult, Light light)
        {
            var direction = hitResult.ray.direction;
            var shading = light.ComputeShading(hitResult);
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
            return hit && shadowHitResult!.distance <= shading.distance ? 0 : Math.Max(intensity, 0);
        }

        private Color ComposeReflectLighting(HitResult hitResult, Light light, int depth)
        {
            if (depth == 0) return Color.Black;

            var attenuation = hitResult.material.Reflect(hitResult, out var wi);
            var composed = attenuation > 0
                ? ReflectRay(hitResult, wi, light, depth) * attenuation
                : Color.Black;

            var shading = light.ComputeShading(hitResult);
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

            var lightIntensity = light.ComputeIntensity(wi);
            return hitResult.material.Color * lightIntensity;
        }
    }
}
