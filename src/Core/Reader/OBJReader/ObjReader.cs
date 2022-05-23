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

        public Mesh Read(Scene scene, string path)
        {
            return ParseObjects(path);
        }

        private Mesh ParseObjects(string path)
        {
            ObjState state = new ObjState();

            foreach (string line in GetLinesIterator(path))
            {
                if (line.StartsWith("#")) continue; // skip comment
                string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                IObjParser? parser = GetParserNullable(parts);
                parser?.Parse(parts, state);
            }

            return state;
        }

        private IEnumerable<string> GetLinesIterator(string path)
        {
            return File.ReadLines(path, Encoding.UTF8);
        }

        private IObjParser? GetParserNullable(string[] parts)
        {
            if (parts.Length == 0) return null;
            string entityType = parts[0];
            return parsers.Find((parser) => parser.CanParse(entityType));
        }
    }
}
