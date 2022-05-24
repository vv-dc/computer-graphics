namespace Core.Reader.OBJReader
{

    using Common.Numeric;
    using RayTracingLib.Traceable;

    public class ObjState : Mesh
    {
        public readonly List<Point3> points = new();
        public readonly List<Vector3> normals = new();
        public readonly List<Parser.Face> faces = new();

        public List<ITraceable> GetTraceables()
        {
            var triangles = new List<ITraceable>(faces.Count);

            foreach (var face in faces)
            {
                var triangle = new Triangle(
                    points[face.points[0]],
                    points[face.points[1]],
                    points[face.points[2]]
                );
                if (face.normals is not null)
                {
                    triangle.SetNormals(
                        normals[face.normals[0]],
                        normals[face.normals[1]],
                        normals[face.normals[2]]
                    );
                }
                triangles.Add(triangle);
            }

            return triangles;
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
