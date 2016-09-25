using System.Collections.Generic;
using System.Linq;

namespace Boggle
{
    public static class Extensions
    {
        public static bool In2DArrayBounds(this char[,] array, int x, int y)
        {
            return 
                x >= array.GetLowerBound(0) && 
                x <= array.GetUpperBound(0) && 
                y >= array.GetLowerBound(1) && 
                y <= array.GetUpperBound(1);
        }

        public static bool InRange<T>(this List<List<T>> list, int x, int y)
        {
            if (x >= 0 && x < list.Count)
            {
                return list.All(innerList => y >= 0 && y < innerList.Count);
            }
            return false;
        }
    }
}
