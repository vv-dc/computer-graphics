namespace RayTracingLib
{
    using Common.Numeric;

    public interface ITransformable
    {
        void Transform(Matrix4x4 matrix);
    }
}
