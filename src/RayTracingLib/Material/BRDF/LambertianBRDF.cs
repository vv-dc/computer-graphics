namespace RayTracingLib.Material.BRDF
{
    using Common.Numeric;

    public class LambertianBRDF : IBRDF
    {
        private readonly float coeff;

        public LambertianBRDF(float coeff = 1)
        {
            this.coeff = coeff;
        }

        public float Diffuse(Vector3 wi, Vector3 wo)
        {
            return coeff; /// MathF.PI;
        }

        public float Sample(HitResult hitResult, out Vector3 wi)
        {
            // var wo = hitResult.ray.direction;
            wi = hitResult.Normal + Vector3.GetRandomOnHemisphere(hitResult.Normal);
            // return Diffuse(wi, wo);
            return 0f;
        }
    }
}
