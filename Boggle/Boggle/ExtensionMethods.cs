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
    }
}
