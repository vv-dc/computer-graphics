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
            OBJMesh state = new OBJMesh();
            var entityTypes = provider.GetEntityTypes();

            foreach (string line in GetLinesIterator(path))
            {
                if (!entityTypes.Any(entityType => line.StartsWith(entityType))) continue;
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
            string entityType = parts[0];
            return provider.GetObjParserNullable(entityType);
        }
    }
}
