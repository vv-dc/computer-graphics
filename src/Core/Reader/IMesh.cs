namespace Core.Reader
{
    using RayTracingLib;

    public interface IMesh : ITransformable
    {
        List<ITreeTraceable> GetTraceables();
    }
}
