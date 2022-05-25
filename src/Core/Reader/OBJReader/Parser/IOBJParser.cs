namespace Core.Reader.OBJReader.Parser
{
    public interface IOBJParser
    {
        void Parse(string[] parts, OBJMesh state);
    }
}
