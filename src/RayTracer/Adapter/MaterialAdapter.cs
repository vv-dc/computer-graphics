namespace RayTracer.Adapter
{
    using Common.Numeric;
    using RayTracingLib;
    using RayTracingLib.Light;
    using RayTracer.Tracer;

    public class MaterialAdapter : IAdapter<Color>
    {
        private const int REFLECT_THRESHOLD = 6;

        private const float REFLECT_BIAS = 1e-5f;

        private const int NUM_SHADOW_RAYS = 4;

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
            var color = lights.Aggregate(Color.Black, (color, light) =>
                color + ComposeLighting(hitResult, light, REFLECT_THRESHOLD));
            return color;
        }

        private Color ComposeEnviromentalLighting(List<Light> lights)
        {
            var color = lights.Aggregate(Color.Black, (color, light) =>
                light is EnvironmentalLight ? color + light.color * light.intensity : color);
            return color;
        }

        private Color ComposeLighting(HitResult hitResult, Light light, int depth)
        {
            return ComposeDiffuseLighting(hitResult, light) + ComposeReflectLighting(hitResult, light, depth);
        }

        private Color ComposeDiffuseLighting(HitResult hitResult, Light light)
        {
            var totalColor = Color.Black;
            for (int idx = 0; idx < NUM_SHADOW_RAYS; ++idx)
            {
                totalColor += GetDiffuseLighting(hitResult, light) / NUM_SHADOW_RAYS;
            }
            return totalColor;
        }

        private Color GetDiffuseLighting(HitResult hitResult, Light light)
        {
            var direction = hitResult.ray.direction;
            var shading = light.ComputeShading(hitResult);
            var color = hitResult.material.Diffuse(hitResult, -shading.direction);
            var shadowMultiplier = ShadowUtils.ComputeShadowMultiplier(shadowTracer, hitResult, shading);
            return color * shadowMultiplier * shading.color;
        }

        private Color ComposeReflectLighting(HitResult hitResult, Light light, int depth)
        {
            if (depth == 0) return Color.Black;

            var reflectionFactor = hitResult.material.Reflect(hitResult, out var wi);
            var composed = reflectionFactor > 0
                ? ReflectRay(hitResult, wi, light, depth) * reflectionFactor
                : Color.Black;

            var shading = light.ComputeShading(hitResult);
            return hitResult.material.ColorFromUV(hitResult.uv) * composed;
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
            return hitResult.material.ColorFromUV(hitResult.uv) * lightIntensity;
        }
    }
}
