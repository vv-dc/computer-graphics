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
            return coeff / MathF.PI;
        }

        public float Sample(HitResult hitResult, out Vector3 wi)
        {
            var wo = hitResult.ray.direction;
            wi = hitResult.Normal + ComposeRandomUnit(3);
            return Diffuse(wi, wo);
        }

        private Vector3 ComposeRandomUnit(int samples)
        {
            var composed = new Vector3(0, 0, 0);
            for (var idx = 0; idx < samples; ++idx)
            {
                composed += Vector3.GetRandomUnit();
            }
            return composed / samples;
        }
    }
}
