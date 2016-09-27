using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Boggle
{
    public class BoggleBootstrapper
    {
        private static IWindsorContainer _container;

        public static IWindsorContainer GetContainer(string path)
        {
            _container = new WindsorContainer();

            _container.Register(
                Component.For<IWordFinder>()
                    .ImplementedBy<WordFinder>()
                );
            _container.Register(
                Component.For<IValidator>()
                    .ImplementedBy<Validator>()
                );

            _container.Register(
                Component.For<IWordsRepository>()
                    .ImplementedBy<WordsRepository>()
                    .DependsOn(Dependency.OnValue("path", path))
                    .LifestyleSingleton()
                );

            _container.Register(
                Component.For<ITrieHelper>().ImplementedBy<TrieHelper>()
                );

            _container.Register(
                Component.For<ISolver>()
                    .ImplementedBy<BoggleSolver>()
                    .LifestyleSingleton()
                );

            _container.Register(Component.For<IResults>().ImplementedBy<Result>());

            return _container;
        }
    }
}