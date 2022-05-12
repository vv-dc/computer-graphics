namespace Core.Consumer
{
    using System.Text;
    using RayTracingLib;

    class ConsoleConsumer : IConsumer<double?>
    {
        public void Consume(Image<double?> image, string? target)
        {
            string imageString = MapImageToString(image);
            Console.Write(imageString);
        }

        public string MapImageToString(Image<double?> image)
        {
            var stringBuilder = new StringBuilder();

            for (var y = 0; y < image.Height; ++y)
            {
                for (var x = 0; x < image.Width; ++x)
                {
                    double? color = image[y, x];
                    char character = color is null ? ' ' : MapCharacter((double)color);
                    stringBuilder.Append(character);
                }
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }

        private char MapCharacter(double value)
        {
            if (value < 0) return ' ';
            if (value < 0.2) return '.';
            if (value < 0.5) return '*';
            if (value < 0.8) return 'O';
            return '#'; // = 0 || >= 0.8
        }
    }
}
