namespace RayTracer
{
    public class Color
    {
        public static readonly Color White = new Color(255, 255, 255);
        public static readonly Color Black = new Color(0, 0, 0);
        public static readonly Color Red = new Color(255, 0, 0);
        public static readonly Color Green = new Color(0, 255, 0);
        public static readonly Color Blue = new Color(0, 0, 255);
        public static readonly Color Steel = new Color(70, 130, 180);

        public readonly byte r;
        public readonly byte g;
        public readonly byte b;

        public Color(byte r, byte g, byte b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public Color(byte value)
        {
            r = value; g = value; b = value;
        }
    }
}
