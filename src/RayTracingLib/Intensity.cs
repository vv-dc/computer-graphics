namespace RayTracingLib
{
    public struct Intensity
    {
        public static readonly Intensity Background = -3;

        private readonly float value;

        public Intensity(float value)
        {
            this.value = value;
        }

        public float Value { get => value; }

        public static implicit operator float(Intensity intensity) => intensity.value;

        public static implicit operator Intensity(float intensity) => new Intensity(intensity);
    }
}