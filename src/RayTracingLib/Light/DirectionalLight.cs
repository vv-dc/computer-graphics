namespace RayTracingLib.Light
{
    using Common.Numeric;

    public class DirectionalLight : Light
    {
        private Vector3 direction;

        public DirectionalLight(Vector3 direction, Color color, float intensity = 1.0f) : base(color, intensity)
        {
            this.direction = Vector3.Normalize(direction);
        }

        public Vector3 Direction
        {
            get => direction;
            set { direction = Vector3.Normalize(direction); }
        }

        public override LightShading ComputeShading(HitResult hitResult)
        {
            return new LightShading()
            {
                direction = this.direction,
                color = this.color * this.intensity,
                distance = float.PositiveInfinity,
            };
        }
    }
}
