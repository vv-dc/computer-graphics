namespace RayTracingLib.Traceable
{
    using Common;
    using Common.Numeric;

    public class Triangle : ITraceable
    {
        private Point3 v0; // A
        private Point3 v1; // B
        private Point3 v2; // C

        private Vector3 n0;
        private Vector3 n1;
        private Vector3 n2;

        public Triangle(Point3 v0, Point3 v1, Point3 v2)
        {
            this.v0 = v0;
            this.v1 = v1;
            this.v2 = v2;
            this.SetDefaultNormals();
        }

        public void SetNormals(Vector3 n0, Vector3 n1, Vector3 n2)
        {
            this.n0 = Vector3.Normalize(n0);
            this.n1 = Vector3.Normalize(n1);
            this.n2 = Vector3.Normalize(n2);
        }

        public void SetDefaultNormals()
        {
            var edge1 = v1 - v0;
            var edge2 = v2 - v0;
            var normal = Vector3.Cross(edge1, edge2);
            n0 = n1 = n2 = normal;
        }

        public bool Intersect(Ray ray, out HitResult? hitResult)
        {
            hitResult = null;

            var edge1 = v1 - v0;
            var edge2 = v2 - v0;

            var pvec = Vector3.Cross(ray.direction, edge2);
            var det = Vector3.Dot(edge1, pvec);

            if (Math.Abs(det) < Consts.EPS)
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

            var distance = Vector3.Dot(edge2, qvec) * invDet;
            if (distance < 0)
            {
                return false;
            }
            hitResult = new HitResult
            {
                distance = distance,
                ray = ray,
                Normal = n0 * (1 - u - v) + n1 * u + n2 * v,
            };
            return true;
        }

        public void Transform(Matrix4x4 matrix)
        {
            v0 = matrix * v0;
            v1 = matrix * v1;
            v2 = matrix * v2;
            n0 = Vector3.Normalize(matrix * n0);
            n1 = Vector3.Normalize(matrix * n1);
            n2 = Vector3.Normalize(matrix * n2);
        }
    }
}
