namespace RayTracer
{
    using RayTracingLib;
    using RayTracingLib.Light;

    public class Scene
    {
        public readonly Camera camera;

        public List<RenderableObject> objects { get; private set; }

        public Light Light { get; set; }

        public Scene(Camera camera)
        {
            this.camera = camera;
            this.objects = new List<RenderableObject>();
        }

        public void AddObject(RenderableObject sceneObject)
        {
            objects.Add(sceneObject);
        }

        public void SetObjects(List<RenderableObject> sceneObjects)
        {
            // shallow copy
            objects = new List<RenderableObject>(sceneObjects);
        }
    }
}
