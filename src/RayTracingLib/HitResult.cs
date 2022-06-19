namespace RayTracingLib
{
    using Common.Numeric;

    public class HitResult
    {
        public float distance;
        public Ray ray;
        private Vector3 normal; // TODO: replace with normal
        public Material.Material material;

        public Vector3 Normal
        {
            get => normal;
            set => normal = Vector3.Normalize(value);
        }
    }
}
