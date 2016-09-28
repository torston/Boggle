namespace Boggle
{
    public static class ExtensionMethods
    {
        public static bool IsInBoardRange(this char[,] array, int x, int y)
        {
            return
                x >= array.GetLowerBound(0) &&
                x <= array.GetUpperBound(0) &&
                y >= array.GetLowerBound(1) &&
                y <= array.GetUpperBound(1);
        }

        public static bool IsNonEnglishCharacter(this char character)
        {
            return character < 63 || character > 126;
        }

        public static char[,] CreateBoardFromString(this char[,] array, string s)
        {
            int rows = array.GetUpperBound(0) + 1;
            int columns = array.GetUpperBound(1) + 1;

            array = new char[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    array[i, j] = s[i * rows + j];
                }
            }

            return array;
        }

    }
}
