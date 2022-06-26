namespace RayTracer.Adapter
{
    using Common.Numeric;
    using RayTracingLib;
    using RayTracingLib.Light;

    public class IntensityAdapter : IAdapter<Intensity>
    {
        public void Init(List<RenderableObject> sceneObjects) { }

        private Intensity ComputeBackgroundIntensity(List<Light> lights)
        {
            return lights.Aggregate(0.0f, (accum, light) => light is EnvironmentalLight ? accum + light.intensity : accum);
        }

        public Intensity Adapt(List<Light> lights, HitResult? hitResult)
        {
            if (hitResult is null) return ComputeBackgroundIntensity(lights); // Intensity.Background;
            var intensity = 0.0f;

            foreach (var light in lights)
            {
                var shading = light.ComputeShading(hitResult);
                var cosTheta = Vector3.Dot(-shading.direction, hitResult.Normal);
                intensity += light.intensity * MathF.Max(cosTheta, 0);
            }
            return intensity;
        }
    }
}
