namespace Boggle
{
    public class Letter
    {
        public int Row { get; }
        public int Column { get; }
        public char Character { get; }

        public Letter(int row, int column, char character)
        {
            Row = row;
            Column = column;
            Character = character;
        }
    }
}