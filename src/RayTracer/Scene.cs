namespace RayTracer.Scene {
    using RayTracingLib;
    using RayTracingLib.Traceable;
    using RayTracingLib.Light;

    public class Scene {
        public readonly Camera camera;

        public readonly List<ITraceable> objects = new();

        public Light Light { get; set; }

        public Scene(Camera camera) {
            this.camera = camera;
        }

        public void AddObject(ITraceable sceneObject) {
            objects.Add(sceneObject);
        }
    }
}