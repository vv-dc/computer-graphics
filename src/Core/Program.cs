namespace Core
{
    using Core.ArgumentParser;
    using Core.Scenario;

    public class Program
    {
        public static void Main(string[] args)
        {
            var parser = new CLIArgumentParser() { Predefined = new ConsoleScenario() };
            parser.Parse(args);
        }
    }
}
