namespace RayTracer.Adapter
{

    using RayTracer.Tracer;
    using RayTracingLib;
    using RayTracingLib.Light;

    public class ColorAdapter : IAdapter<Color>
    {
        private const int NUM_SHADOW_RAYS = 4;
        private readonly ITracer shadowTracer;

        public ColorAdapter(ITracer shadowTracer)
        {
            this.shadowTracer = shadowTracer;
        }

        public void Init(List<RenderableObject> sceneObjects)
        {
            shadowTracer.Init(sceneObjects);
        }

        private Color ComputeBackgroundColor(List<Light> lights)
        {
            return lights.Aggregate(Color.Black, (accum, light) =>
                light is EnvironmentalLight ? accum + light.color * light.intensity : accum);
        }

        public Color Adapt(List<Light> lights, HitResult? hitResult)
        {
            if (hitResult is null) return Color.Steel; // ComputeBackgroundColor(lights);
            var color = Color.Black;

            foreach (var light in lights)
            {
                var perLightColor = Color.Black;
                for (int idx = 0; idx < NUM_SHADOW_RAYS; ++idx)
                {
                    var shading = light.ComputeShading(hitResult);
                    var shadowMultiplier = ShadowUtils.ComputeShadowMultiplier(shadowTracer, hitResult, shading);
                    perLightColor += shading.color * shadowMultiplier / NUM_SHADOW_RAYS;
                }
                color += perLightColor;
            }
            return hitResult!.material.Color * color;
        }
    }
}
