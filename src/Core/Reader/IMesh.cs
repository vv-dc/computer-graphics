namespace Core.Reader
{
    using Common.Numeric;
    using RayTracingLib.Traceable;

    public interface IMesh
    {
        List<ITraceable> GetTraceables();

        void Transform(Matrix4x4 matrix);
    }
}
