namespace Core.ArgumentParser
{
    using CommandLine;
    using CommandLine.Text;

    using Core.Scenario;

    public class CLIArgumentParser : IArgumentParser
    {
        public IScenario Predefined { get; set; }

        public void Parse(string[] args)
        {
            var parser = new Parser(with => with.HelpWriter = null);
            var parsed = parser.ParseArguments<PredefinedOptions, ModelOptions>(args);
            parsed
                .WithParsed<PredefinedOptions>(options => Predefined.Run())
                .WithParsed<ModelOptions>(options => Predefined.Run())
                .WithNotParsed(errors => DisplayHelp(parsed, errors));
        }

        private void DisplayHelp<T>(ParserResult<T> parsed, IEnumerable<Error> errors)
        {
            var helpText = HelpText.AutoBuild(parsed, help =>
            {
                help.AdditionalNewLineAfterOption = false;
                help.AddDashesToOption = true;
                help.Copyright = "\u001b[1F";
                help.Heading = "Simple CLI raytracer";
                help.AutoHelp = false;
                help.AutoVersion = false;
                return HelpText.DefaultParsingErrorsHandler(parsed, help);
            },
            error => error,
            verbsIndex: true);
            Console.WriteLine(helpText);
        }
    }
}
