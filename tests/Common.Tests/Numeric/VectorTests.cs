namespace Common.Tests.Numeric
{
    using System;
    using System.Collections.Generic;

    using Xunit;

    using Common;
    using Common.Numeric;

    public class VectorTests
    {
        public static IEnumerable<object[]> GetSumData()
        {
            yield return new object[] {
                new Vector3(-11, 3, 7), new Vector3(5, 6, -3), new Vector3(-6, 9, 4)
            };
        }

        [Theory]
        [MemberData(nameof(GetSumData))]
        public void SumTest(Vector3 left, Vector3 right, Vector3 expected)
        {
            Assert.True(expected == left + right);
            Assert.True(expected == right + left);
        }

        public static IEnumerable<object[]> GetDotData()
        {
            yield return new object[] {
                new Vector3(1, 0, 0), new Vector3(0, 1, 0), 0F
            };
            yield return new object[] {
                new Vector3(1, 0, 0), new Vector3(-1, 0, 0), -1F
            };
            yield return new object[] {
                new Vector3(1, 0, 0), new Vector3(1, 0, 0), 1F
            };
            yield return new object[] {
                new Vector3(4, -2, 1), new Vector3(-7, 3, 12), -22F
            };
        }

        [Theory]
        [MemberData(nameof(GetDotData))]
        public void DotTest(Vector3 left, Vector3 right, float expected)
        {
            Assert.Equal(expected, Vector3.Dot(left, right));
        }

        public static IEnumerable<object[]> GetCrossData()
        {
            yield return new object[] {
                new Vector3(2, 1, -6), new Vector3(0, 0, 0)
            };
            yield return new object[] {
                new Vector3(3, -5, 2), new Vector3(-1, 7, 4)
            };
            yield return new object[] {
                new Vector3(1, 0, 0), new Vector3(0, 1, 0)
            };
        }

        public static float GetAngleSine(Vector3 left, Vector3 right)
        {
            var lenProduct = (float)Math.Sqrt(left.LengthSquared() * right.LengthSquared());
            if (lenProduct < Consts.EPS * Consts.EPS)
            {
                return 1F;
            }
            var angle = Vector3.Dot(left, right) / lenProduct;
            return (float)Math.Sqrt(1 - Math.Pow(angle, 2));
        }

        [Theory]
        [MemberData(nameof(GetCrossData))]
        public void CrossTest(Vector3 left, Vector3 right)
        {
            var cross = Vector3.Cross(left, right);
            var area = left.Length() * right.Length() * GetAngleSine(left, right);

            Assert.Equal(area, cross.Length());
            Assert.Equal(0, Vector3.Dot(left, cross));
            Assert.Equal(0, Vector3.Dot(right, cross));
        }
    }
}
