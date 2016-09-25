namespace Boggle
{
    public interface ISolver
    {
        // this func may be called multiple times
        // board: 'q' represents the 'qu' Boggle cube
        IResults FindWords(char[,] board);
    }
}