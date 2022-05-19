namespace Core
{
    using Core.Scenario;
    using Core.ArgumentParser;

    public class Program
    {
        public static void Main(string[] args)
        {
            var parser = new CLIArgumentParser() { Predefined = new FigureScenario() };
            parser.Parse(args);
        }
    }
}
