using System;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace Boggle
{
    internal class Program
    {
        private static IWindsorContainer _container;

        private const string PathToDictionary = "dictionary.txt";
        private const int Rows = 3;
        private const int Columns = 3;

        private static char[,] _board;

        private static void Main(string[] args)
        {
            CreateRandomBoard();

            PrintBoard();

            BootstrapContainer();

            var solver = _container.Resolve<ISolver>(); ;
            var result = solver.FindWords(_board);

            Console.WriteLine($"The score is: {result.Score} points!");
            Console.WriteLine($"Words are: \n {string.Join(", ", result.Words)}");
        }

        private static void BootstrapContainer()
        {
            _container = new WindsorContainer();
            _container.AddFacility<TypedFactoryFacility>();

            _container.Register(
                Component.For<ISolver>()
                    .ImplementedBy<BoggleSolver>()
                    .LifestyleSingleton()
            );

            _container.Register(
                Component.For<IWordsRepository>()
                    .ImplementedBy<WordsRepository>()
                    .DependsOn(Dependency.OnValue("path", PathToDictionary))
                    .LifestyleSingleton()
            );

            _container.Register(
                Component.For<ITrieNode>().ImplementedBy<TrieNode>().LifestyleTransient(),
                Component.For<ITrieNodeFactory>().AsFactory().LifestyleTransient(),
                Component.For<ITrieHelper>().ImplementedBy<TrieHelper>().LifestyleSingleton()
            );

            _container.Register(Component.For<IResults>().ImplementedBy<Result>());
        }

        private static void CreateRandomBoard()
        {
            _board = new char[Rows, Columns];

            for (var i = 0; i < Columns; i++)
            {
                for (var j = 0; j < Rows; j++)
                {
                    _board[i, j] = "dzxeaiqut"[j + Columns * i];
                }
            }
        }

        private static void PrintBoard()
        {
            for (var i = 0; i < Columns; i++)
            {
                for (var j = 0; j < Rows; j++)
                {
                    Console.Write(_board[i, j] + " ");
                }

                Console.WriteLine();
            }
        }
    }
}
