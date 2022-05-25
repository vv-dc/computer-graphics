namespace Core.Reader.OBJReader
{
    using System.Text;

    using Core.Reader.OBJReader.Parser;
    using RayTracer;

    public class OBJReader
    {
        private readonly OBJParserProvider provider;

        public OBJReader(OBJParserProvider provider)
        {
            this.provider = provider;
        }

        public IMesh Read(Scene scene, string path)
        {
            return ParseObjects(path);
        }

        private IMesh ParseObjects(string path)
        {
            OBJMesh state = new OBJMesh();

            foreach (string line in GetLinesIterator(path))
            {
                if (line.StartsWith("#")) continue; // skip comment
                string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                IOBJParser? parser = GetParserNullable(parts);
                parser?.Parse(parts, state);
            }

            return state;
        }

        private IEnumerable<string> GetLinesIterator(string path)
        {
            return File.ReadLines(path, Encoding.UTF8);
        }

        private IOBJParser? GetParserNullable(string[] parts)
        {
            if (parts.Length == 0) return null;
            string entityType = parts[0];
            return provider.GetObjParserNullable(entityType);
        }
    }
}
