namespace Core.Reader.OBJReader
{
    using Vector2 = System.Numerics.Vector2;

    using Common.Numeric;
    using Core.Reader;
    using Core.Reader.OBJReader.Parser;
    using RayTracingLib;
    using RayTracingLib.Traceable;

    public class OBJMesh : IMesh
    {
        public readonly List<Point3> points = new();
        public readonly List<Vector2> uvs = new();
        public readonly List<Vector3> normals = new();
        public readonly List<Parser.Face> faces = new();

        public List<ITreeTraceable> GetTraceables()
        {
            var triangles = new List<ITreeTraceable>(faces.Count);

            foreach (var face in faces)
            {
                var vertices = face.vertices;
                var splittedTriangles = SplitVerticesToTriangle(vertices);
                triangles.AddRange(splittedTriangles);
            }

            return triangles;
        }

        private List<Triangle> SplitVerticesToTriangle(List<FaceVertex> vertices)
        {
            if (vertices.Count == 3)
            {
                return new List<Triangle>() {
                    BuildTriangle(vertices[0], vertices[1], vertices[2])
                };
            }
            return new List<Triangle>() {
                BuildTriangle(vertices[0], vertices[1], vertices[2]),
                BuildTriangle(vertices[0], vertices[2], vertices[3])
            };
        }

        private Triangle BuildTriangle(FaceVertex a, FaceVertex b, FaceVertex c)
        {
            var triangle = new Triangle(
                points[a.pointIdx],
                points[b.pointIdx],
                points[c.pointIdx]
            );
            if (a.normalIdx is not null && b.normalIdx is not null && c.normalIdx is not null)
            {
                triangle.SetNormals(
                    normals[(int)a.normalIdx],
                    normals[(int)b.normalIdx],
                    normals[(int)c.normalIdx]
                );
            }
            else
            {
                triangle.SetDefaultNormals();
            }
            if (a.uvIdx is not null && b.uvIdx is not null && c.uvIdx is not null)
            {
                triangle.SetUV(
                    uvs[(int)a.uvIdx],
                    uvs[(int)b.uvIdx],
                    uvs[(int)c.uvIdx]
                );
            }
            return triangle;
        }

        public void Transform(Matrix4x4 matrix)
        {
            for (int i = 0; i < normals.Count; ++i)
                normals[i] = Vector3.Normalize(matrix * normals[i]);

            for (int i = 0; i < points.Count; ++i)
                points[i] = matrix * points[i];
        }
    }
}
