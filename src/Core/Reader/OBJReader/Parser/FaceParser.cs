namespace Core.Reader.OBJReader.Parser
{
    using Common.Numeric;
    using RayTracingLib.Traceable;

    public class FaceParser : IObjParser
    {
        private const string entityType = "f";

        public void Parse(string[] parts, ObjState state)
        {
            List<Point3> points = new List<Point3>(3);
            List<Vector3> normals = new List<Vector3>(3);

            for (int idx = 0; idx < 3; ++idx)
            {
                string[] faceParts = parts[idx + 1].Split("/");
                points.Add(state.points[int.Parse(faceParts[0]) - 1]);
                normals.Add(state.normals[int.Parse(faceParts[2]) - 1]);
            }

            var triangle = new Triangle(points[0], points[1], points[2]);
            state.objects.Add(triangle);
        }

        public bool CanParse(string type) => type == entityType;
    }
}
