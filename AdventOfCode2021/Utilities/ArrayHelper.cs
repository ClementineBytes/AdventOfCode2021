using System.Collections.Generic;

namespace AdventOfCode2021.Utilities
{
    public class ArrayHelper
    {
        public ArrayHelper() { }

        public IEnumerable<T> GetRow<T>(T[,] array, int row)
        {
            for (int i = 0; i <= array.GetUpperBound(1); i++)
                yield return array[row, i];
        }

        public IEnumerable<T> GetRow<T>(T[][] array, int row)
        {
            for (int i = 0; i <= array.GetUpperBound(0); i++)
                yield return array[i][row];
        }
        public IEnumerable<T> GetColumn<T>(T[,] array, int column)
        {
            for (int i = 0; i <= array.GetUpperBound(0); i++)
                yield return array[i, column];
        }
        public IEnumerable<T> GetColumn<T>(T[][] array, int column)
        {
            for (int i = 0; i <= array.GetUpperBound(0); i++)
                yield return array[i][column];
        }
    }
}
