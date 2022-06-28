namespace RayTracer.Tracer
{
    using RayTracingLib;

    public class FirstHitTracer : ITracer
    {
        private List<RenderableObject> sceneObjects = new();

        public void Init(List<RenderableObject> sceneObjects)
        {
            this.sceneObjects = sceneObjects;
        }

        public bool Trace(Ray ray, out HitResult? hitResult)
        {
            foreach (var sceneObject in sceneObjects)
            {
                if (sceneObject.Traceable.Intersect(ray, out hitResult)) return true;
            }
            hitResult = null;
            return false;
        }
    }
}
