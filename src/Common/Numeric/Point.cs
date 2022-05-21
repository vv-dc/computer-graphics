namespace Common.Numeric
{
    public class Point3
    {
        public float X { get; }
        public float Y { get; }
        public float Z { get; }

        public Point3(float x, float y, float z)
        {
            X = x; Y = y; Z = z;
        }

        public Point3(float value)
        {
            X = Y = Z = value;
        }

        public static Vector3 operator -(Point3 left, Point3 right) =>
            new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

        public static Point3 operator +(Point3 left, Vector3 right) =>
            new Point3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
    }
}
