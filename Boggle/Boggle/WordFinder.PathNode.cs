namespace Boggle
{
    public partial class WordFinder
    {
        private class PathNode
        {
            public int Row { get; }
            public int Column { get; }

            public PathNode(int row, int column)
            {
                Row = row;
                Column = column;
            }
        }
    }
}