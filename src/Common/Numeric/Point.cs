namespace Common.Numeric
{
    public class Point3 : ICloneable
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Point3(float x, float y, float z)
        {
            X = x; Y = y; Z = z;
        }

        public Point3(float value)
        {
            X = Y = Z = value;
        }

        public object Clone() => new Point3(X, Y, Z);

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
            set
            {
                switch (axis)
                {
                    case 0: X = value; break;
                    case 1: Y = value; break;
                    case 2: Z = value; break;
                    default:
                        throw new IndexOutOfRangeException("Axis is out of bound");
                }
            }
        }

        public static Vector3 operator -(Point3 left, Point3 right) =>
            new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

        public static Point3 operator +(Point3 left, Vector3 right) =>
            new Point3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

        public static Point3 Min(Point3 left, Point3 right) =>
            new(
                Math.Min(left.X, right.X),
                Math.Min(left.Y, right.Y),
                Math.Min(left.Z, right.Z)
            );

        public static Point3 Max(Point3 left, Point3 right) =>
            new(
                Math.Max(left.X, right.X),
                Math.Max(left.Y, right.Y),
                Math.Max(left.Z, right.Z)
            );
    }
}
