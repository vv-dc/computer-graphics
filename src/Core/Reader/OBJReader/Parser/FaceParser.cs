namespace Core.Reader.OBJReader.Parser
{
    public class FaceParser : IObjParser
    {
        private const string entityType = "f";

        public void Parse(string[] parts, ObjState state)
        {
            var points = new int[3];
            var normals = new int[3];

            for (int idx = 0; idx < 3; ++idx)
            {
                string[] faceParts = parts[idx + 1].Split("/");
                points[idx] = int.Parse(faceParts[0]) - 1;
                normals[idx] = int.Parse(faceParts[2]) - 1;
            }

            var face = new Face() { normals = normals, points = points };
            state.faces.Add(face);
        }

        public bool CanParse(string type) => type == entityType;
    }
}
