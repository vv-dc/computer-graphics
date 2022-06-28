namespace Core.Reader.OBJReader.Parser
{
    public class FaceParser : IOBJParser
    {
        public void Parse(string[] parts, OBJMesh state)
        {
            var verticesNumber = parts.Length - 1;
            var vertices = new List<FaceVertex>(verticesNumber);

            for (int idx = 0; idx < verticesNumber; ++idx)
            {
                var faceVertex = ParseVertex(parts[idx + 1], state);
                vertices.Add(faceVertex);
            }

            state.faces.Add(new Face() { vertices = vertices });
        }

        private FaceVertex ParseVertex(string part, OBJMesh state)
        {
            string[] faceParts = part.Split("/");
            var faceVertex = new FaceVertex();

            faceVertex.pointIdx = MapIdx(int.Parse(faceParts[0]), state.points.Count);

            if (faceParts.Length > 1 && !string.IsNullOrEmpty(faceParts[1]))
                faceVertex.uvIdx = MapIdx(int.Parse(faceParts[1]), state.uvs.Count);

            if (faceParts.Length > 2)
                faceVertex.normalIdx = MapIdx(int.Parse(faceParts[2]), state.normals.Count);

            return faceVertex;
        }

        private int MapIdx(int idx, int count)
        {
            return idx > 0 ? idx - 1 : count + idx;
        }
    }
}
