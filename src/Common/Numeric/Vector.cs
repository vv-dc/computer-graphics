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

        public float this[int axis]
        {
            get
            {
                switch (axis)
                {
                    case 0: return X;
                    case 1: return Y;
                    case 2: return Z;
                    default:
                        throw new IndexOutOfRangeException("Axis is out of bound");
                }
            }
        }

        public float Length() => MathF.Sqrt(LengthSquared());

        public float LengthSquared() => Dot(this, this);

        public static Vector3 operator +(Vector3 left, Vector3 right) =>
            new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

        public static Vector3 operator -(Vector3 left, Vector3 right) =>
            new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

        public static Vector3 operator -(Vector3 value) =>
            new(-value.X, -value.Y, -value.Z);

        public static Vector3 operator *(Vector3 value, float factor) =>
            new(value.X * factor, value.Y * factor, value.Z * factor);

        public static Vector3 operator *(Vector3 left, Vector3 right) =>
            new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

        public static Vector3 operator /(Vector3 value, float factor) =>
            new(value.X / factor, value.Y / factor, value.Z / factor);

        public static Vector3 operator /(float factor, Vector3 value) =>
            new(factor / value.X, factor / value.Y, factor / value.Z);

        public static bool operator ==(Vector3 left, Vector3 right) =>
            (left - right).LengthSquared() < Consts.EPS * Consts.EPS;

        public static bool operator !=(Vector3 left, Vector3 right) =>
            !(left == right);

        public static Vector3 Normalize(Vector3 value) =>
            value / value.Length();

        public static float Min(Vector3 value) =>
            Math.Min(value.X, Math.Min(value.Y, value.Z));

        public static Vector3 Min(Vector3 left, Vector3 right) =>
           new(
               Math.Min(left.X, right.X),
               Math.Min(left.Y, right.Y),
               Math.Min(left.Z, right.Z)
           );

        public static float Max(Vector3 value) =>
            Math.Max(value.X, Math.Max(value.Y, value.Z));

        public static Vector3 Max(Vector3 left, Vector3 right) =>
           new(
               Math.Max(left.X, right.X),
               Math.Max(left.Y, right.Y),
               Math.Max(left.Z, right.Z)
           );

        public static Vector3 Cross(Vector3 left, Vector3 right) =>
            new(
                left.Y * right.Z - left.Z * right.Y,
                left.Z * right.X - left.X * right.Z,
                left.X * right.Y - left.Y * right.X
            );

        public static float Dot(Vector3 left, Vector3 right) =>
            left.X * right.X + left.Y * right.Y + left.Z * right.Z;

        public override string ToString() => $"Vector3<({X}, {Y}, {Z})>";

        public static Vector3 Reflect(Vector3 left, Vector3 right)
        {
            var dot = Vector3.Dot(left, right);
            return left - (right * 2 * dot);
        }

        // https://math.stackexchange.com/questions/44689/how-to-find-a-random-axis-or-unit-vector-in-3d
        public static Vector3 GetRandomUnit()
        {
            var rnd = new Random();
            var angle = (float)(rnd.NextDouble() * 2 * MathF.PI);
            var z = (float)(rnd.NextDouble() * 2 - 1);

            var multZ = MathF.Sqrt(1 - z * z);
            var x = multZ * MathF.Cos(angle);
            var y = multZ * MathF.Sin(angle);

            return new(x, y, z);
        }
    }
}
