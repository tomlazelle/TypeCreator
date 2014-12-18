using System.Linq;
using NUnit.Framework;
using Should;
using TypeCreator.AddStrategy;
using TypeCreator.Tests.TestObjects;

namespace TypeCreator.Tests
{
    [TestFixture]
    public class GenericTestFixture
    {
        [Test]
        public void can_add_and_get_closed_type()
        {
            var container = TypeRegistry.Construct(x =>
            {
                x.Add(new TypeAction(typeof(IGenericProvider<>), typeof(GenericProvider<>)));
                x.Add(new TypeAction<ICustomerService, CustomerService>());
                x.Add(new TypeAction<IRepository, Repository>());
                x.Add(new TypeAction<IProvider, StringProvider>());
            });

            var service = container.GetInstance<ICustomerService>();

            service.Get().Count().ShouldBeGreaterThan(2);
            service.Get2().ShouldNotBeNull();
        }

        [Test]
        public void can_get_instace_from_non_generic_type()
        {
            var container = TypeRegistry.Construct(x =>
            {
                x.Add(new TypeAction<Customer, Customer>());
            });

            var customer = (Customer) container.GetInstance(typeof (Customer));

            customer.Name.ShouldBeNull();
        }
    }
}