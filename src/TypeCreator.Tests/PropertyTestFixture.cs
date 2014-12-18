using NUnit.Framework;
using Should;
using TypeCreator.AddStrategy;
using TypeCreator.Tests.TestObjects;

namespace TypeCreator.Tests
{
    [TestFixture]
    public class PropertyTestFixture
    {

        [Test]
        public void can_inject_property()
        {
            var container = TypeRegistry.Construct(x =>
            {
                x.Add(new TypeAction<ICustomer, Customer>
                {
                    FuncToExecute = a =>
                    {
                        a.Name = "this is a test";
                        return a;
                    }
                });
            });

            var customer = container.GetInstance<ICustomer>();

            customer.Name.ShouldEqual("this is a test");
        }

        [Test]
        public void can_set_properties_on_already_created_type()
        {
            var container = TypeRegistry.Construct(x =>
            {
                x.Add(new TypeAction<IProvider, StringProvider>());
            });

            var customer = new Customer();

            container.SetProperties(customer);

            customer.Provider.ShouldNotBeNull();
            customer.Provider.Get().ShouldNotBeNull();
        }
    }
}