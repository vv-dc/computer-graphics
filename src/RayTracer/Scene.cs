namespace RayTracer
{
    using RayTracingLib.Light;
    using RayTracingLib.Traceable;

    public class Scene
    {
        public readonly Camera camera;

        public List<ITraceable> objects { get; private set; }

        public DirectionalLight Light { get; set; }

        public Scene(Camera camera)
        {
            this.camera = camera;
            this.objects = new List<ITraceable>();
        }

        public void AddObject(ITraceable sceneObject)
        {
            objects.Add(sceneObject);
        }

        public void SetObjects(List<ITraceable> sceneObjects)
        {
            // shallow copy
            objects = new List<ITraceable>(sceneObjects);
        }
    }
}
