// ReSharper disable once CheckNamespace
namespace Boggle
{
    public interface ISolver
    {
        // this func may be called multiple times
        // inputBoard: 'q' represents the 'qu' Boggle cube
        IResults FindWords(char[,] inputBoard);
    }
}