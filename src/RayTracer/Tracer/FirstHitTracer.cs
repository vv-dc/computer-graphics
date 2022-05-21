namespace RayTracer.Tracer
{
    using RayTracingLib;
    using RayTracingLib.Traceable;

    public class FirstHitTracer : ITracer
    {
        private List<ITraceable> sceneObjects = new();

        public void Init(List<ITraceable> sceneObjects)
        {
            this.sceneObjects = sceneObjects;
        }

        public bool Trace(Ray ray, out HitResult? hitResult)
        {
            foreach (var sceneObject in sceneObjects)
            {
                if (sceneObject.Intersect(ray, out hitResult)) return true;
            }
            hitResult = null;
            return false;
        }
    }
}
