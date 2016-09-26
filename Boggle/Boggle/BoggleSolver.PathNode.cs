namespace Boggle
{
    public partial class BoggleSolver
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