namespace Core.Reader.OBJReader.Parser
{
    using Common.Numeric;

    public class NormalParser : IOBJParser
    {
        public void Parse(string[] parts, OBJMesh state)
        {
            Vector3 normal = new Vector3(
                float.Parse(parts[1]),
                float.Parse(parts[2]),
                float.Parse(parts[3])
            );
            state.normals.Add(normal);
        }
    }
}
