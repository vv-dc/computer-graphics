namespace RayTracingLib
{
    using Vector2 = System.Numerics.Vector2;
    using Common.Numeric;

    public class HitResult
    {
        public float distance;
        public Ray ray;
        public Vector2 uv = Vector2.Zero;
        private Vector3 normal;
        public Material.IMaterial material;

        public Vector3 Normal
        {
            get => normal;
            set => normal = Vector3.Normalize(value);
        }
    }
}
