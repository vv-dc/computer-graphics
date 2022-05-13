namespace Core
{
    using Core.Scenario;

    public class Program
    {
        public static void Main(string[] args)
        {
            var scenario = new FigureScenario();
            scenario.Run(args);
        }
    }
}
