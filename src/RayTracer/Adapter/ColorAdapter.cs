namespace RayTracer.Adapter
{

    using Common;
    using RayTracingLib;
    using RayTracingLib.Light;

    public class ColorAdapter : IAdapter<Color>
    {
        private readonly IAdapter<Intensity> intensityAdapter;

        public ColorAdapter(IAdapter<Intensity> intensityAdapter)
        {
            this.intensityAdapter = intensityAdapter;
        }

        public void Init(List<RenderableObject> sceneObjects)
        {
            intensityAdapter.Init(sceneObjects);
        }

        public Color Adapt(List<Light> lights, HitResult? hitResult)
        {
            Intensity intensity = intensityAdapter.Adapt(lights, hitResult);
            if (Math.Abs(intensity - Intensity.Background) < Consts.EPS)
            {
                return Color.Steel;
            }
            return hitResult!.material.Color * Math.Max(intensity, 0);
        }
    }
}
