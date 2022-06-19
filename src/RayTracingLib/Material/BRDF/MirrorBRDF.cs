namespace RayTracingLib.Material.BRDF
{
    using Common.Numeric;

    public class MirrorBRDF : IBRDF
    {

        public float Diffuse(Vector3 wi, Vector3 wo)
        {
            return 0;
        }

        public float Sample(HitResult hitResult, out Vector3 wi)
        {
            var wo = hitResult.ray.direction;
            var normal = hitResult.Normal;

            wi = Vector3.Reflect(wo, hitResult.Normal);
            return 1; //Vector3.Dot(wi, normal);
        }
    }
}
