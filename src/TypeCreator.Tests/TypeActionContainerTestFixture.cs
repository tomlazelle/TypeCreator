using NUnit.Framework;
using Should;
using TypeCreator.AddStrategy;
using TypeCreator.Creation;
using TypeCreator.Tests.TestObjects;

namespace TypeCreator.Tests
{
    [TestFixture]
    public class TypeActionContainerTestFixture
    {
        [Test]
        public void can_use_container_for_type_actions()
        {
            var container = TypeRegistry.Construct(x =>
                x.AddContainer(new TestContainer()));

            var customer = container.GetInstance<ICustomer>();

            customer.ShouldNotBeNull();

        }
    }

    public class TestContainer : TypeActionContainer
    {
        public TestContainer()
        {
            Add(new TypeAction<ICustomer,Customer>());
        }
    }
}