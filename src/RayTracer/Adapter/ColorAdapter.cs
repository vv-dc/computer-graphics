namespace RayTracer.Adapter
{

    using Common;
    using RayTracingLib;
    using RayTracingLib.Light;
    using RayTracingLib.Traceable;

    public class ColorAdapter : IAdapter<Color>
    {
        private readonly IAdapter<Intensity> intensityAdapter;

        public ColorAdapter(IAdapter<Intensity> intensityAdapter)
        {
            this.intensityAdapter = intensityAdapter;
        }

        public void Init(List<ITraceable> sceneObjects)
        {
            intensityAdapter.Init(sceneObjects);
        }

        public Color Adapt(DirectionalLight light, HitResult? hitResult)
        {
            Intensity intensity = intensityAdapter.Adapt(light, hitResult);
            if (Math.Abs(intensity - Intensity.Background) < Consts.EPS)
            {
                return Color.Steel;
            }
            byte value = (byte)(Math.Max(intensity, 0) * 255);
            return new Color(value);
        }
    }
}
