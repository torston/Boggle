﻿// ReSharper disable once CheckNamespace
namespace Boggle
{
    public class MyBoggleSolution
    {
        // input dictionary is a file with one word per line
        public static ISolver CreateSolver(string dictionaryPath)
        {
            var container = BoggleBootstrapper.GetContainer(dictionaryPath);

            return container.Resolve<ISolver>();
        }
    }
}