namespace RayTracingLib.Material
{
    using Vector2 = System.Numerics.Vector2;

    using Common.Numeric;
    using RayTracingLib.Material.BRDF;

    public class MirrorMaterial : IMaterial
    {
        private readonly Texture<float>? refTexture;
        private readonly Color color = Color.White;
        private readonly IBRDF brdf = new MirrorBRDF();

        public MirrorMaterial() { }

        public MirrorMaterial(Texture<float> refTexture)
        {
            this.refTexture = refTexture;
        }

        public Color ColorFromUV(Vector2 uv) => color;

        public Color Diffuse(HitResult hitResult, Vector3 wi)
        {
            var wo = hitResult.ray.direction;
            return ColorFromUV(hitResult.uv) * brdf.Diffuse(wi, wo);
        }

        public float Reflect(HitResult hitResult, out Vector3 wi)
        {
            var factor = refTexture is not null ? refTexture.GetFromUV(hitResult.uv) : 1.0f;
            return factor * brdf.Sample(hitResult, out wi);
        }
    }
}
