namespace Core.Reader
{
    using RayTracingLib;

    public interface Mesh : ITransformable
    {
        List<ITreeTraceable> GetTraceables();
    }
}
