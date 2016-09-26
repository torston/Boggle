using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Boggle
{
    internal class BoggleBootstrapper
    {
        private static IWindsorContainer _container;

        public static IWindsorContainer GetContainer(string path)
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
                    .DependsOn(Dependency.OnValue("path", path))
                    .LifestyleSingleton()
                );

            _container.Register(
                Component.For<ITrieNode>().ImplementedBy<TrieNode>().LifestyleTransient(),
                Component.For<ITrieNodeFactory>().AsFactory().LifestyleTransient(),
                Component.For<ITrieHelper>().ImplementedBy<TrieHelper>().LifestyleSingleton()
                );

            _container.Register(Component.For<IResults>().ImplementedBy<Result>());

            return _container;
        }
    }
}