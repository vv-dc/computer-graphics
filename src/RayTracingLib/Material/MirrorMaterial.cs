namespace RayTracingLib.Material
{
    using Common.Numeric;
    using RayTracingLib.Light;
    using RayTracingLib.Material.BRDF;

    public class MirrorMaterial : Material
    {
        private IBRDF brdf;

        public MirrorMaterial() : base(Color.White)
        {
            this.brdf = new MirrorBRDF();
        }

        public override Color Diffuse(Vector3 wi, Vector3 wo)
        {
            return this.Color * brdf.Diffuse(wi, wo);
        }

        public override float Scatter(HitResult hitResult, out Vector3 wi)
        {
            return brdf.Sample(hitResult, out wi);
        }
    }
}
