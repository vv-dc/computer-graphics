namespace Core.ArgumentParser
{
    using CommandLine;
    using CommandLine.Text;

    [Verb("predefined", isDefault: true, HelpText = "Render a predefinded (compiled) scene")]
    public class PredefinedOptions
    {
        [Usage(ApplicationAlias = "raytracer")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Render a scene from the predefined scenario", new PredefinedOptions { });
            }
        }
    }

    [Verb("model", HelpText = "Render a 3D model from a file")]
    public class ModelOptions
    {
        [Option('s', "source", Required = true, HelpText = "Path to a file with a 3D model")]
        public string Source { get; set; }

        [Option('o', "output", Required = true, HelpText = "Path to an image file where rendered model will be stored")]
        public string Output { get; set; }

        [Option('w', "width", Required = true, HelpText = "Width of the image")]
        public int Width { get; set; }

        [Option('h', "height", Required = true, HelpText = "Height of the image")]
        public int Height { get; set; }

        [Usage(ApplicationAlias = "raytracer")]

        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example(
                    "Render a model in the cow.obj file and save image to the rendered.ppm file",
                    new ModelOptions { Source = "cow.obj", Output = "rendered.ppm", Width = 100, Height = 100 }
                );
            }
        }
    }
}
