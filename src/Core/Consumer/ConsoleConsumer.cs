namespace Core.Consumer
{
    using System.Text;

    using RayTracer.Adapter;
    using RayTracingLib;

    public class ConsoleConsumer : IConsumer<Intensity>
    {
        public void Consume(Image<Intensity> image, string? target)
        {
            string imageString = MapImageToString(image);
            Console.Write(imageString);
        }

        public string MapImageToString(Image<Intensity> image)
        {
            var stringBuilder = new StringBuilder();

            for (var y = 0; y < image.Height; ++y)
            {
                for (var x = 0; x < image.Width; ++x)
                {
                    char character = MapCharacter(image[y, x]);
                    stringBuilder.Append(character);
                }
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }

        private char MapCharacter(float value)
        {
            if (Math.Abs(value - ConsoleAdapter.background) < Consts.EPS) return '-'; // background
            if (value < -Consts.EPS) return ' '; // 0
            if (value < 0.2) return '.';
            if (value < 0.5) return '*';
            if (value < 0.8) return 'O';
            return '#'; // >= 0.8
        }
    }
}
