using NUnit.Framework;
using Should;
using TypeCreator.AddStrategy;
using TypeCreator.Tests.TestObjects;

namespace TypeCreator.Tests
{
    [TestFixture]
    public class TypeRegistryTestFixture
    {
        [Test]
        public void can_add_type()
        {
            var container = TypeRegistry.Construct(x =>
            {
                x.Add(new TypeAction<ICustomer, Customer>());
            });

            var customer = container.GetInstance<ICustomer>();

            customer.Name.ShouldBeNull();
        }

        [Test]
        public void can_add_type_with_lifecycle()
        {
            var container = TypeRegistry.Construct(x =>
            {
                x.Add(new TypeAction<ICustomer, Customer>());
            });

            var customer = container.GetInstance<ICustomer>();

            customer.Name.ShouldBeNull();
        }

        [Test]
        public void can_register_the_same_type_interface_multiple_times()
        {
            var container = TypeRegistry.Construct(x =>
            {
                x.Add(new TypeAction<IProvider, StringProvider>());
                x.Add(new TypeAction<IProvider, MemoryProvider>());
            });

            var instances = container.GetAllInstances<IProvider>();

            foreach (IProvider instance in instances)
            {
                instance.Get().ShouldNotBeNull();
            }
        }

        [Test]
        public void can_add_type_with_name()
        {
            var container = TypeRegistry.Construct(x =>
            {
                x.Add(new TypeAction<IProvider, StringProvider> { Key = "StringProvider" });
                x.Add(new TypeAction<IProvider, MemoryProvider> { Key = "MemoryProvider" });
            });

            var instance = container.GetInstance<IProvider>("StringProvider");

            instance.ShouldNotBeNull();

            instance.Get().ShouldEqual("StringProvider");
        }

        [Test]
        public void can_add_type_with_name2()
        {
            var container = TypeRegistry.Construct(x =>
            {
                x.Add(new TypeAction<IProvider, StringProvider> { Key = "StringProvider" });
                x.Add(new TypeAction<IProvider, MemoryProvider> { Key = "MemoryProvider" });
            });

            var instance = (IProvider)container.GetInstance(typeof(IProvider), "StringProvider");

            instance.ShouldNotBeNull();

            instance.Get().ShouldEqual("StringProvider");
        }
    }
}