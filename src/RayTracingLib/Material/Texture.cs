namespace RayTracingLib.Material
{
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.PixelFormats;

    using System.Numerics;

    using Color = RayTracingLib.Color;

    public class Texture<Type>
    {
        private readonly Type[,] values;
        public float Height => values.GetLength(0);
        public float Width => values.GetLength(1);

        public Texture(Type[,] values)
        {
            this.values = values;
        }

        public Texture(int width, int height)
        {
            this.values = new Type[width, height];
        }

        public Type this[int y, int x]
        {
            get => values[y, x];
            set => values[y, x] = value;
        }

        public Type GetFromUV(Vector2 uv)
        {
            return this[(int)MathF.Floor(uv.Y * Height), (int)MathF.Floor(uv.X * Width)];
        }

        public static Texture<Color> FromImage(string path)
        {
            Image<Rgb24> image = Image.Load<Rgb24>(path);

            var texture = new Texture<Color>(image.Width, image.Height);
            for (int y = 0; y < image.Height; ++y)
            {
                for (int x = 0; x < image.Width; ++x)
                {
                    var pixel = image[x, y];
                    texture[y, x] = new Color((float)pixel.R / 255, (float)pixel.G / 255, (float)pixel.B / 255);
                }
            }
            return texture;
        }
    }
}
