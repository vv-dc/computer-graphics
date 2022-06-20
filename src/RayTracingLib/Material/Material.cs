namespace RayTracingLib.Material
{
    using Common.Numeric;
    using RayTracingLib.Light;

    public abstract class Material
    {
        public Color Color { get; }

        protected Material(Color color)
        {
            this.Color = color;
        }

        public abstract Color Diffuse(Vector3 wi, Vector3 wo);

        public abstract float Reflect(HitResult hitResult, out Vector3 wi);
    }
}
