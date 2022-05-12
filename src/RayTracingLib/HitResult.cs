namespace RayTracingLib {
    using RayTracingLib.Numeric;

    public class HitResult {
        public float distance;
        private Vector3 normal;

        public Vector3 Normal {
            get => normal;
            set { normal = Vector3.Normalize(value); }
        }
    }
}