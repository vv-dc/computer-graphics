namespace RayTracingLib.Material
{
    using Common.Numeric;
    using RayTracingLib.Material.BRDF;

    public class LambertianMaterial : Material
    {
        private readonly IBRDF brdf;

        public LambertianMaterial(Color color) : base(color)
        {
            this.brdf = new LambertianBRDF(1.0f);
        }

        public override Color Diffuse(Vector3 wi, Vector3 wo)
        {
            return this.Color * brdf.Diffuse(wi, wo);
        }

        public override float Reflect(HitResult hitResult, out Vector3 wi)
        {
            return brdf.Sample(hitResult, out wi);
        }
    }
}
