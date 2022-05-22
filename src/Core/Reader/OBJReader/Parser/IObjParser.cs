namespace Core.Reader.OBJReader.Parser
{
    public interface IObjParser
    {
        void Parse(string[] parts, ObjState state);

        bool CanParse(string type);
    }
}
