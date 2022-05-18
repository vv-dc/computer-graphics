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

        public HitResult? Trace(Ray ray)
        {
            HitResult? hitResult = null;
            foreach (var sceneObject in sceneObjects)
            {
                if (sceneObject.Intersect(ray, out HitResult? currentHit))
                {
                    if (currentHit?.distance > hitResult?.distance)
                    {
                        continue;
                    }
                    hitResult = currentHit;
                }
            }
            return hitResult;
        }
    }
}