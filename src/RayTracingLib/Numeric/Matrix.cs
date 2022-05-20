namespace RayTracingLib.Numeric
{
    public class Matrix4x4
    {
        public float M11, M12, M13, M14;
        public float M21, M22, M23, M24;
        public float M31, M32, M33, M34;
        public float M41, M42, M43, M44;

        public Matrix4x4(
            float m11, float m12, float m13, float m14,
            float m21, float m22, float m23, float m24,
            float m31, float m32, float m33, float m34,
            float m41, float m42, float m43, float m44
        )
        {
            M11 = m11; M12 = m12; M13 = m13; M14 = m14;
            M21 = m21; M22 = m22; M23 = m23; M24 = m24;
            M31 = m31; M32 = m32; M33 = m33; M34 = m34;
            M41 = m41; M42 = m42; M43 = m43; M44 = m44;
        }

        private Matrix4x4() { }

        public Vector3 ExtractScale() =>
            new Vector3(
                new Vector3(M11, M12, M13).Length(),
                new Vector3(M21, M22, M23).Length(),
                new Vector3(M31, M32, M33).Length()
            );

        public static Matrix4x4 Identity() =>
            new(
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1
            );

        public static Matrix4x4 CreateTranslation(float X, float Y, float Z) =>
            new(
                1, 0, 0, X,
                0, 1, 0, Y,
                0, 0, 1, Z,
                0, 0, 0, 1
            );

        public static Matrix4x4 CreateScale(float X, float Y, float Z) =>
            new(
                X, 0, 0, 0,
                0, Y, 0, 0,
                0, 0, Z, 0,
                0, 0, 0, 1
            );

        public static Matrix4x4 CreateScale(float value) =>
            CreateScale(value, value, value);

        public static Matrix4x4 CreateScale(float X, float Y, float Z, Point3 center) =>
            new(
                X, 0, 0, center.X * (1 - X),
                0, Y, 0, center.Y * (1 - Y),
                0, 0, Z, center.Z * (1 - Z),
                0, 0, 0, 1
            );

        public static Matrix4x4 CreateScale(float value, Point3 center) =>
            CreateScale(value, value, value, center);

        public static Matrix4x4 CreateRotationX(float angle) // in radians
        {
            var cos = (float)Math.Cos(angle);
            var sin = (float)Math.Sin(angle);

#pragma warning disable format
            return new(
                1,  0,    0,  0,
                0, cos, -sin, 0,
                0, sin,  cos, 0,
                0,  0,    0,  1
            );
#pragma warning restore format
        }

        public static Matrix4x4 CreateRotationX(float angle, Point3 center) // in radians
        {
            var cos = (float)Math.Cos(angle);
            var sin = (float)Math.Sin(angle);

#pragma warning disable format
            return new(
                1,  0,    0,  0,
                0, cos, -sin, center.Y * (1 - cos) + center.Z * sin,
                0, sin,  cos, center.Z * (1 - cos) - center.Y * sin,
                0,  0,    0,  1
            );
#pragma warning restore format
        }

        public static Matrix4x4 CreateRotationY(float angle) // in radians
        {
            var cos = (float)Math.Cos(angle);
            var sin = (float)Math.Sin(angle);

#pragma warning disable format
            return new(
                 cos, 0, sin, 0,
                  0,  1,  0,  0,
                -sin, 0, cos, 0,
                  0,  0,  0,  1
            );
#pragma warning restore format
        }

        public static Matrix4x4 CreateRotationY(float angle, Point3 center) // in radians
        {
            var cos = (float)Math.Cos(angle);
            var sin = (float)Math.Sin(angle);

#pragma warning disable format
            return new(
                 cos, 0, sin, center.X * (1 - cos) - center.Z * sin,
                  0,  1,  0,  0,
                -sin, 0, cos, center.Z * (1 - cos) + center.X * sin,
                  0,  0,  0,  1
            );
#pragma warning restore format
        }

        public static Matrix4x4 CreateRotationZ(float angle) // in radians
        {
            var cos = (float)Math.Cos(angle);
            var sin = (float)Math.Sin(angle);

#pragma warning disable format
            return new(
                cos, -sin, 0, 0,
                sin,  cos, 0, 0,
                 0,    0,  1, 0,
                 0,    0,  0, 1
            );
#pragma warning restore format
        }

        public static Matrix4x4 CreateRotationZ(float angle, Point3 center) // in radians
        {
            var cos = (float)Math.Cos(angle);
            var sin = (float)Math.Sin(angle);

#pragma warning disable format
            return new(
                cos, -sin, 0, center.X * (1 - cos) + center.Y * sin,
                sin,  cos, 0, center.Y * (1 - cos) - center.X * sin,
                 0,    0,  1, 0,
                 0,    0,  0, 1
            );
#pragma warning restore format
        }

        public static Matrix4x4 operator *(Matrix4x4 left, Matrix4x4 right) =>
            new()
            {
                M11 = left.M11 * right.M11 + left.M12 * right.M21 + left.M13 * right.M31 + left.M14 * right.M41,
                M12 = left.M11 * right.M12 + left.M12 * right.M22 + left.M13 * right.M32 + left.M14 * right.M42,
                M13 = left.M11 * right.M13 + left.M12 * right.M23 + left.M13 * right.M33 + left.M14 * right.M43,
                M14 = left.M11 * right.M14 + left.M12 * right.M24 + left.M13 * right.M34 + left.M14 * right.M44,

                M21 = left.M21 * right.M11 + left.M22 * right.M21 + left.M23 * right.M31 + left.M24 * right.M41,
                M22 = left.M21 * right.M12 + left.M22 * right.M22 + left.M23 * right.M32 + left.M24 * right.M42,
                M23 = left.M21 * right.M13 + left.M22 * right.M23 + left.M23 * right.M33 + left.M24 * right.M43,
                M24 = left.M21 * right.M14 + left.M22 * right.M24 + left.M23 * right.M34 + left.M24 * right.M44,

                M31 = left.M31 * right.M11 + left.M32 * right.M21 + left.M33 * right.M31 + left.M34 * right.M41,
                M32 = left.M31 * right.M12 + left.M32 * right.M22 + left.M33 * right.M32 + left.M34 * right.M42,
                M33 = left.M31 * right.M13 + left.M32 * right.M23 + left.M33 * right.M33 + left.M34 * right.M43,
                M34 = left.M31 * right.M14 + left.M32 * right.M24 + left.M33 * right.M34 + left.M34 * right.M44,

                M41 = left.M41 * right.M11 + left.M42 * right.M21 + left.M43 * right.M31 + left.M44 * right.M41,
                M42 = left.M41 * right.M12 + left.M42 * right.M22 + left.M43 * right.M32 + left.M44 * right.M42,
                M43 = left.M41 * right.M13 + left.M42 * right.M23 + left.M43 * right.M33 + left.M44 * right.M43,
                M44 = left.M41 * right.M14 + left.M42 * right.M24 + left.M43 * right.M34 + left.M44 * right.M44
            };

        public static Vector3 operator *(Matrix4x4 left, Vector3 right) =>
            new Vector3(
                left.M11 * right.X + left.M12 * right.Y + left.M13 * right.Z,
                left.M21 * right.X + left.M22 * right.Y + left.M23 * right.Z,
                left.M31 * right.X + left.M32 * right.Y + left.M33 * right.Z
            );

        public static Point3 operator *(Matrix4x4 left, Point3 right) =>
            new Point3(
                left.M11 * right.X + left.M12 * right.Y + left.M13 * right.Z + left.M14,
                left.M21 * right.X + left.M22 * right.Y + left.M23 * right.Z + left.M24,
                left.M31 * right.X + left.M32 * right.Y + left.M33 * right.Z + left.M34
            );
    }
}