namespace Core.Reader.OBJReader.Parser
{
    public class FaceParser : IObjParser
    {
        private readonly int[,] quadFaceIdxs = new int[,] { { 0, 1, 2 }, { 0, 2, 3 } };
        private const string entityType = "f";

        public void Parse(string[] parts, ObjState state)
        {
            var numFields = parts[1].Split("/").Length;
            var hasNormals = numFields >= 3;

            var numPoints = parts.Length - 1;
            var points = new int[numPoints];
            var normals = hasNormals ? new int[numPoints] : null;

            for (int idx = 0; idx < numPoints; ++idx)
            {
                string[] faceParts = parts[idx + 1].Split("/");
                points[idx] = MapIdx(int.Parse(faceParts[0]), state.points.Count);
                if (hasNormals)
                {
                    normals![idx] = MapIdx(int.Parse(faceParts[2]), state.normals.Count);
                }
            }
            ExtractFaces(points, normals, state);
        }

        private void ExtractFaces(int[] points, int[]? normals, ObjState state)
        {
            if (points.Length == 3)
            {
                var face = new Face() { normals = normals, points = points };
                state.faces.Add(face);
            }
            else
            {
                var hasNormals = normals is not null;
                for (int faceIdx = 0; faceIdx < 2; ++faceIdx)
                {
                    var faceNormals = hasNormals ? new int[3] : null;
                    var facePoints = new int[3];

                    for (int idx = 0; idx < 3; ++idx)
                    {
                        facePoints[idx] = points[quadFaceIdxs[faceIdx, idx]];
                        if (hasNormals)
                        {
                            faceNormals![idx] = normals![quadFaceIdxs[faceIdx, idx]];
                        }
                    }
                    state.faces.Add(new Face() { normals = faceNormals, points = facePoints });
                }
            }
        }

        private int MapIdx(int idx, int count)
        {
            return idx > 0 ? idx - 1 : count + idx;
        }

        public bool CanParse(string type) => type == entityType;
    }
}
