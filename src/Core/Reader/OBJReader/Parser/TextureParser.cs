namespace Core.Reader.OBJReader.Parser
{
    using System.Numerics;

    public class TextureParser : IOBJParser
    {
        public void Parse(string[] parts, OBJMesh state)
        {
            Vector2 uv = new Vector2(
                float.Parse(parts[1]),
                float.Parse(parts[2])
            );
            state.uvs.Add(uv);
        }
    }
}
