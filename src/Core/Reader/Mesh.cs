namespace Core.Reader
{
    using Common.Numeric;
    using RayTracingLib.Traceable;

    public interface Mesh
    {
        List<ITraceable> GetTraceables();

        void Transform(Matrix4x4 matrix);
    }
}
