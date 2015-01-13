using NUnit.Framework;
using Should;
using TypeCreator.AddStrategy;
using TypeCreator.Creation;
using TypeCreator.Tests.TestObjects;

namespace TypeCreator.Tests
{
    [TestFixture]
    public class LifeSpanTestFixture
    {
        [Test]
        public void can_have_a_singleton_life_cycle()
        {
            var container = TypeRegistry.Construct(x =>
            {
                x.Add(new TypeAction<IRepository, Repository>
                {
                    LifeSpan = new SingletonLifeSpan()
                });

                x.Add(new TypeAction<IProvider,StringProvider>());
            });

            var expected = container.GetInstance<IRepository>().GuidTest();

            var actual = container.GetInstance<IRepository>().GuidTest();

            expected.ShouldEqual(actual);
        }  
    }
}