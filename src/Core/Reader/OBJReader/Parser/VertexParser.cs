namespace Core.Reader.OBJReader.Parser
{
    using Common.Numeric;

    public class VertexParser : IOBJParser
    {
        public void Parse(string[] parts, OBJMesh state)
        {
            Point3 vertex = new Point3(
                float.Parse(parts[1]),
                float.Parse(parts[2]),
                float.Parse(parts[3])
            );
            state.points.Add(vertex);
        }
    }
}
