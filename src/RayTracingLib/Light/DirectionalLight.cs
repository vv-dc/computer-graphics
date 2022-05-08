namespace RayTracingLib.Light
{
    using Numeric;
    class DirectionalLight
    {
        private Vector3 direction;

        DirectionalLight(Vector3 direction)
        {
            this.direction = direction;
        }

        public Vector3 Direction
        {
            get => direction;
            set => Vector3.Normalize(direction);
        }
    }
}