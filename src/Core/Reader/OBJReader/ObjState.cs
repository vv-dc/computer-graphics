namespace Core.Reader.OBJReader
{

    using Common.Numeric;
    using RayTracingLib.Traceable;

    public class ObjState
    {
        public readonly List<Point3> points = new();
        public readonly List<Vector3> normals = new();
        public readonly List<ITraceable> objects = new();
    }
}
