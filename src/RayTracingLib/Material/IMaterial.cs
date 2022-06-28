namespace RayTracingLib.Material
{
    using Vector2 = System.Numerics.Vector2;

    using Common.Numeric;

    public interface IMaterial
    {
        Color ColorFromUV(Vector2 uv);

        Color Diffuse(HitResult hitResult, Vector3 wi);

        float Reflect(HitResult hitResult, out Vector3 wi);
    }
}
