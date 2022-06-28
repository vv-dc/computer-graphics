namespace Core.Reader.OBJReader
{
    using System.Collections.Generic;

    using Core.Reader.OBJReader.Parser;

    public class OBJParserProvider
    {
        private Dictionary<string, IOBJParser> parsersMap = new Dictionary<string, IOBJParser>()
        {
            { "f", new FaceParser() },
            { "v", new VertexParser() },
            { "vn", new NormalParser() },
            { "vt", new TextureParser() }
        };

        public IOBJParser? GetObjParserNullable(string type)
        {
            return parsersMap.TryGetValue(type, out var value) ? value : null;
        }

        public List<string> GetEntityTypes() => parsersMap.Keys.ToList();
    }
}
