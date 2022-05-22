namespace Core.Reader.OBJReader.Parser
{
    using Common.Numeric;

    public class VertexParser : IObjParser
    {
        private const string entityType = "v";

        public void Parse(string[] parts, ObjState state)
        {
            Point3 vertex = new Point3(
                float.Parse(parts[1]),
                float.Parse(parts[2]),
                float.Parse(parts[3])
            );
            state.points.Add(vertex);
        }

        public bool CanParse(string type) => type == entityType;
    }
}
