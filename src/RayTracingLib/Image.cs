namespace RayTracingLib
{
    public class Image<PixelType>
    {
        public int Width => pixels.GetLength(1);

        public int Height => pixels.GetLength(0);

        private readonly PixelType[,] pixels;

        public Image(int width, int height)
        {
            this.pixels = new PixelType[height, width];
        }

        public PixelType this[int x, int y]
        {
            get => pixels[x, y];
            set => pixels[x, y] = value;
        }
    }
}