namespace RayTracer.Adapter
{
    using Common;
    using Common.Numeric;
    using RayTracingLib;
    using RayTracingLib.Light;
    using RayTracer.Tracer;

    class ShadowUtils
    {
        public static float ComputeShadowMultiplier(ITracer shadowTracer, HitResult hitResult, LightShading shading)
        {
            var cosTheta = Vector3.Dot(-shading.direction, hitResult.Normal);
            var hitPoint = hitResult.ray.GetPoint(hitResult.distance) + hitResult.Normal * Consts.SHADOW_EPS;
            var shadowRay = new Ray(hitPoint, -shading.direction);
            var hit = shadowTracer.Trace(shadowRay, out var shadowHitResult);
            return hit && shadowHitResult!.distance <= shading.distance ? 0 : Math.Max(cosTheta, 0);
        }
    }
}
