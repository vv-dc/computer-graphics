namespace RayTracer.Tracer
{
    using RayTracingLib;

    public class BasicTracer : ITracer
    {
        private List<RenderableObject> sceneObjects = new();

        public void Init(List<RenderableObject> objects)
        {
            this.sceneObjects = objects;
        }

        public bool Trace(Ray ray, out HitResult? hitResult)
        {
            hitResult = null;
            foreach (var sceneObject in sceneObjects)
                if (sceneObject.Traceable.Intersect(ray, out HitResult? currentHit))
                {
                    if (currentHit?.distance >= hitResult?.distance) continue;
                    hitResult = currentHit;
                    hitResult!.material = sceneObject.Material;
                }
            return hitResult is not null;
        }
    }
}
