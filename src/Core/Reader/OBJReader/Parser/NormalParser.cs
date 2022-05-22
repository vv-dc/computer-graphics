namespace Core.Reader.OBJReader.Parser
{
    using Common.Numeric;

    public class NormalParser : IObjParser
    {
        private const string entityType = "vn";

        public void Parse(string[] parts, ObjState state)
        {
            Vector3 normal = new Vector3(
                float.Parse(parts[1]),
                float.Parse(parts[2]),
                float.Parse(parts[3])
            );
            state.normals.Add(normal);
        }

        public bool CanParse(string type) => type == entityType;
    }
}
