namespace RayTracingLib.Traceable
{
    using Common.Numeric;

    public interface ITraceable
    {
        bool Intersect(Ray ray, out HitResult? hitResult);

        void Transform(Matrix4x4 matrix);
    }
}
