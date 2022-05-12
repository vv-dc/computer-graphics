namespace RayTracingLib.Light
{
    using Numeric;

    public class DirectionalLight : Light
    {
        private Vector3 direction;

        public DirectionalLight(Vector3 direction)
        {
            this.direction = Vector3.Normalize(direction);
        }

        public Vector3 Direction
        {
            get => direction;
            set { direction = Vector3.Normalize(direction); }
        }
    }
}