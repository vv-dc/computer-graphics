namespace RayTracingLib
{
    public class Color
    {
        public static readonly Color White = new Color(1, 1, 1);
        public static readonly Color Black = new Color(0, 0, 0);
        public static readonly Color Red = new Color(1, 0, 0);
        public static readonly Color Green = new Color(0, 1, 0);
        public static readonly Color Blue = new Color(0, 0, 1);
        public static readonly Color Steel = new Color(0.275f, 0.510f, 0.706f);
        public static readonly Color Mirror = new Color(0.5f, 0.7f, 1);

        public readonly float r;
        public readonly float g;
        public readonly float b;

        public Color(float r, float g, float b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public override string ToString()
        {
            return $"{r} {g} {b}";
        }

        public Color(float value)
        {
            r = value; g = value; b = value;
        }

        public static Color operator *(Color color, float intensity) => new Color(
            color.r * intensity,
            color.g * intensity,
            color.b * intensity
        );

        public static Color operator /(Color left, float intensity) => new Color(
            left.r / intensity,
            left.g / intensity,
            left.b / intensity
        );

        public static Color operator +(Color left, Color right) => new Color(
            left.r + right.r,
            left.g + right.g,
            left.b + right.b
        );

        public static Color operator *(Color left, Color right) => new Color(
            left.r * right.r,
            left.g * right.g,
            left.b * right.b
        );

        public Color Normalize() => new Color(
            (byte)(r * 255),
            (byte)(g * 255),
            (byte)(b * 255)
        );
    }
}
