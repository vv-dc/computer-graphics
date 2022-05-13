namespace RayTracer.Tracer
{
    using RayTracingLib;
    using RayTracingLib.Traceable;
    using RayTracer;

    public class BasicTracer : ITracer
    {
        private List<ITraceable> sceneObjects = new();

        public void Init(List<ITraceable> sceneObjects)
        {
            this.sceneObjects = sceneObjects;
        }

        public bool Trace(Ray ray, out HitResult? hitResult)
        {
            HitResult? result = null;

            foreach (var sceneObject in sceneObjects)
            {
                if (sceneObject.Intersect(ray, out HitResult? currentHit))
                {
                    if (currentHit?.distance > result?.distance) continue;
                    result = currentHit;
                }
            }

            hitResult = result;
            return result is not null;
        }
    }
}