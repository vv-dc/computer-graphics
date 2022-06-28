namespace RayTracingLib.Material
{
    using Vector2 = System.Numerics.Vector2;

    using Common.Numeric;
    using RayTracingLib.Material.BRDF;

    public class LambertianMaterial : IMaterial
    {
        private readonly Texture<Color>? colorTexture;
        private readonly Color? color;
        private readonly IBRDF brdf = new LambertianBRDF();

        public LambertianMaterial(Color color)
        {
            this.color = color;
        }

        public LambertianMaterial(Texture<Color> colorTexture)
        {
            this.colorTexture = colorTexture;
        }

        public Color ColorFromUV(Vector2 uv)
        {
            return colorTexture is not null ? colorTexture.GetFromUV(uv) : color!;
        }

        public Color Diffuse(HitResult hitResult, Vector3 wi)
        {
            var wo = hitResult.ray.direction;
            return ColorFromUV(hitResult.uv) * brdf.Diffuse(wi, wo);
        }

        public float Reflect(HitResult hitResult, out Vector3 wi)
        {
            return brdf.Sample(hitResult, out wi);
        }
    }
}
