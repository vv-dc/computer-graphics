namespace Common.Numeric
{
    public class Vector3
    {
        public float X { get; }
        public float Y { get; }
        public float Z { get; }

        public Vector3(float x, float y, float z)
        {
            X = x; Y = y; Z = z;
        }

        public Vector3(float value)
        {
            X = Y = Z = value;
        }

        public float Length() => (float)Math.Sqrt(LengthSquared());

        public float LengthSquared() => Dot(this, this);

        public static Vector3 operator +(Vector3 left, Vector3 right) =>
            new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

        public static Vector3 operator -(Vector3 left, Vector3 right) =>
            new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

        public static Vector3 operator -(Vector3 value) =>
            new(-value.X, -value.Y, -value.Z);

        public static Vector3 operator *(Vector3 value, float factor) =>
            new(value.X * factor, value.Y * factor, value.Z * factor);

        public static Vector3 operator /(Vector3 value, float factor) =>
            new(value.X / factor, value.Y / factor, value.Z / factor);

        public static bool operator ==(Vector3 left, Vector3 right) =>
            (left - right).LengthSquared() < Consts.EPS * Consts.EPS;

        public static bool operator !=(Vector3 left, Vector3 right) =>
            !(left == right);

        public static Vector3 Normalize(Vector3 value) =>
            value / value.Length();

        public static float Min(Vector3 value) =>
            Math.Min(value.X, Math.Min(value.Y, value.Z));

        public static Vector3 Cross(Vector3 left, Vector3 right) =>
            new(
                left.Y * right.Z - left.Z * right.Y,
                left.Z * right.X - left.X * right.Z,
                left.X * right.Y - left.Y * right.X
            );

        public static float Dot(Vector3 left, Vector3 right) =>
            left.X * right.X + left.Y * right.Y + left.Z * right.Z;

        public override string ToString() => $"Vector3<({X}, {Y}, {Z})>";
    }
}
