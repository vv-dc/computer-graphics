namespace Core.Scenario
{
    public interface IScenario
    {
        void Run(string? source, string? output, int width, int height);
    }
}
