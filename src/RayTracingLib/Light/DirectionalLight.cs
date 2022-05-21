namespace RayTracingLib.Light
{
    using Common.Numeric;

    public class DirectionalLight
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
