namespace RayTracingLib.Traceable
{
    using Numeric;
    public class Triangle : ITraceable
    {
        private const float MT_EPS = 1E-6F;
        private Vector3 v0; // A
        private Vector3 v1; // B
        private Vector3 v2; // C

        public Triangle(Vector3 v0, Vector3 v1, Vector3 v2)
        {
            this.v0 = v0;
            this.v1 = v1;
            this.v2 = v2;
        }

        public bool Intersect(Ray ray, out float distance)
        {
            distance = 0;

            var edge1 = v1 - v0;
            var edge2 = v2 - v0;

            var pvec = Vector3.Cross(ray.direction, edge2);
            var det = Vector3.Dot(edge1, pvec);

            if (Math.Abs(det) < MT_EPS)
            {
                return false;
            }
            var invDet = 1 / det;

            var tvec = ray.origin - v0;
            var u = Vector3.Dot(tvec, pvec) * invDet; // B barycentric

            if (u < 0 || u > 1)
            {
                return false;
            }

            var qvec = Vector3.Cross(tvec, edge1);
            var v = Vector3.Dot(ray.direction, qvec) * invDet; // C barycentric

            if (v < 0 || u + v > 1)
            {
                return false;
            }

            distance = Vector3.Dot(edge2, qvec) * invDet;
            if (distance < 0)
            {
                distance = 0;
                return false;
            }
            // GetNormal() => Vector3.Cross(edge1, edge2);
            return true;
        }
    }
}