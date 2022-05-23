namespace RayTracer
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

        public PixelType[,] AsMatrix()
        {
            return this.pixels;
        }

        public byte[] GetBytes()
        {
            byte[] bytes = new byte[Width * Height];
            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                    bytes[y * Width + x] = Convert.ToByte(pixels[y, x]);
                }
            }
            return bytes;
        }
    }
}
