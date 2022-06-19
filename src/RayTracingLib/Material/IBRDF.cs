namespace RayTracingLib.Material
{
    using Common.Numeric;

    public interface IBRDF
    {

        public float Diffuse(Vector3 wi, Vector3 wo);

        public float Sample(HitResult hitResult, out Vector3 wi);
    }
}
