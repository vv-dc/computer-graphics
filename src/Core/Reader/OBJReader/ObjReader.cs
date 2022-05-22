namespace Core.Reader.OBJReader
{
    using System.Text;

    using Core.Reader.OBJReader.Parser;
    using RayTracer;
    using RayTracingLib.Traceable;

    public class OBJReader
    {
        private static List<IObjParser> parsers = new List<IObjParser>(){
            new VertexParser(),
            new NormalParser(),
            new FaceParser()
        };

        public void Read(Scene scene, string path)
        {
            List<ITraceable> sceneObjects = ParseObjects(path);
            scene.SetObjects(sceneObjects);
        }

        private List<ITraceable> ParseObjects(string path)
        {
            ObjState state = new ObjState();

            foreach (string line in GetLinesIterator(path))
            {
                if (line.StartsWith("#")) continue; // skip comment
                string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                IObjParser? parser = GetParserNullable(parts);
                parser?.Parse(parts, state);
            }

            return state.objects;
        }

        private IEnumerable<string> GetLinesIterator(string path)
        {
            return File.ReadLines(path, Encoding.UTF8);
        }

        private IObjParser? GetParserNullable(string[] parts)
        {
            string entityType = parts[0];
            return parsers.Find((parser) => parser.CanParse(entityType));
        }
    }
}
