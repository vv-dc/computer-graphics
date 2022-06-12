namespace RayTracer.Tracer
{
    using RayTracingLib;
    using RayTracingLib.Traceable;

    public class BasicTracer : ITracer
    {
        private List<ITraceable> sceneObjects = new();

        public void Init(List<ITraceable> sceneObjects)
        {
            this.sceneObjects = sceneObjects;
        }

        public bool Trace(Ray ray, out HitResult? hitResult)
        {
            hitResult = null;
            foreach (var sceneObject in sceneObjects)
                if (sceneObject.Intersect(ray, out HitResult? currentHit))
                {
                    if (currentHit?.distance >= hitResult?.distance)
                        continue;
                    hitResult = currentHit;
                }
            return hitResult is not null;
        }
    }
}
