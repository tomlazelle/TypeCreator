using System;
using System.Linq;
using NUnit.Framework;
using Should;
using TypeCreator.AddStrategy;
using TypeCreator.Tests.TestObjects;

namespace TypeCreator.Tests
{
    [TestFixture]
    public class MultipleCtorTestFixture
    {
        [Test]
        public void can_use_default_ctor()
        {
            var type = typeof(ClassWithMoreThan1Ctor);

            var result = (ClassWithMoreThan1Ctor)Activator.CreateInstance(type, null);

            result.ReceivedValues.Count.ShouldBeLessThan(1);
        }


        

        [Test]
        public void can_pick_ctor_for_construction()
        {
            var container = TypeRegistry.Construct(x =>
            {
                x.Add(new TypeAction<ICustomer, Customer>());
                x.Add(new TypeAction(typeof(IClassWithMoreThan1Ctor), typeof(ClassWithMoreThan1Ctor))
                {
                    Ctor = new[] { typeof(ICustomer) }
                });
            });

            var item = container.GetInstance<IClassWithMoreThan1Ctor>();

            item.ReceivedValues.Count.ShouldEqual(1);
            item.ReceivedValues.First().Value.ShouldBeType<Customer>();
        }

        [Test]
        public void can_pick_ctor_for_construction_with_2_params()
        {
            var container = TypeRegistry.Construct(x =>
            {
                x.Add(new TypeAction<ICustomer, Customer>());
                x.Add(new TypeAction<IGenericProvider<ICustomer>, GenericProvider<ICustomer>>());
                x.Add(new TypeAction(typeof(IClassWithMoreThan1Ctor), typeof(ClassWithMoreThan1Ctor))
                {
                    Ctor = new[] { typeof(ICustomer), typeof(IGenericProvider<ICustomer>) }
                });
            });

            var item = container.GetInstance<IClassWithMoreThan1Ctor>();

            item.ReceivedValues.Count.ShouldEqual(2);
            item.ReceivedValues.First().Value.ShouldBeType<Customer>();
        }
    }
}