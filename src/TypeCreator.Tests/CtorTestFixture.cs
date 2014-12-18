using System.Linq;
using NUnit.Framework;
using Should;
using TypeCreator.AddStrategy;
using TypeCreator.Tests.TestObjects;

namespace TypeCreator.Tests
{
    [TestFixture]
    public class CtorTestFixture
    {
        [Test]
        public void can_get_type_with_ctor()
        {
            var container = TypeRegistry.Construct(x =>
            {
                x.Add(new TypeAction<IRepository, Repository>());
                x.Add(new TypeAction<IProvider, StringProvider>());
            });

            var repo = container.GetInstance<IRepository>();

            repo.Get().ShouldEqual("StringProvider");
        }

        [Test]
        public void can_get_type_with_generic_ctor()
        {
            var container = TypeRegistry.Construct(x =>
            {
                x.Add(new TypeAction<IGenericProvider<string>, GenericProvider<string>>());
                x.Add(new TypeAction<ICustomerService, CustomerService>());
                x.Add(new TypeAction<IRepository, Repository>());
                x.Add(new TypeAction<IProvider, StringProvider>());
            });

            var service = container.GetInstance<ICustomerService>();

            service.Get().Count().ShouldBeGreaterThan(2);
            service.Get2().ShouldNotBeNull();

        }
    }
}