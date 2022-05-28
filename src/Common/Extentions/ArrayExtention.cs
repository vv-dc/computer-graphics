namespace Common
{
    // https://rosettacode.org/wiki/Quickselect_algorithm#C.23
    public static class ArrayExtension
    {
        public static T NthSmallestElement<T>(this T[] array, int n) where T : IComparable<T>
        {
            if (n < 0 || n > array.Length - 1)
                throw new ArgumentOutOfRangeException("n", n, string.Format("n should be between 0 and {0} it was {1}.", array.Length - 1, n));
            if (array.Length == 0)
                throw new ArgumentException("Array is empty.", "array");
            if (array.Length == 1)
                return array[0];

            return QuickSelectSmallest(array, n)[n];
        }

        private static T[] QuickSelectSmallest<T>(T[] input, int n) where T : IComparable<T>
        {
            var partiallySortedArray = (T[])input.Clone();

            var startIndex = 0;
            var endIndex = input.Length - 1;
            var pivotIndex = n;

            var r = new Random();
            while (endIndex > startIndex)
            {
                pivotIndex = QuickSelectPartition(partiallySortedArray, startIndex, endIndex, pivotIndex);
                if (pivotIndex == n)
                    break;
                if (pivotIndex > n)
                    endIndex = pivotIndex - 1;
                else
                    startIndex = pivotIndex + 1;
                pivotIndex = r.Next(startIndex, endIndex);
            }
            return partiallySortedArray;
        }

        private static int QuickSelectPartition<T>(this T[] array, int startIndex, int endIndex, int pivotIndex) where T : IComparable<T>
        {
            var pivotValue = array[pivotIndex];

            array.Swap(pivotIndex, endIndex);
            for (var i = startIndex; i < endIndex; i++)
            {
                if (array[i].CompareTo(pivotValue) > 0)
                    continue;

                array.Swap(i, startIndex);
                startIndex++;
            }
            array.Swap(endIndex, startIndex);
            return startIndex;
        }

        private static void Swap<T>(this T[] array, int index1, int index2)
        {
            if (index1 == index2)
                return;

            var temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;
        }
    }
}