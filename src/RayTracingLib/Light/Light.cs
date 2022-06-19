namespace RayTracingLib.Light
{
    using Common.Numeric;

    public abstract class Light
    {
        public Color color;

        public float intensity;

        protected Light(Color color, float intensity)
        {
            this.color = color;
            this.intensity = intensity;
        }

        public abstract LightShading ComputeShading(HitResult hitResult);

        public abstract Color ComputeIntensity(Vector3 wi);
    }

    public struct LightShading
    {
        public Vector3 direction;
        public float distance;
        public Color color;
    }
}
