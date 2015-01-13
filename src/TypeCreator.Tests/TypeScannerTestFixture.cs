using System;
using System.Reflection;
using NUnit.Framework;
using Should;
using TypeCreator.AddStrategy;
using TypeCreator.Creation;
using TypeCreator.Tests.TestObjects;

namespace TypeCreator.Tests
{
    [TestFixture]
    public class TypeScannerTestFixture
    {
        [Test]
        public void can_scan_assembly_and_register_types()
        {
            var container = TypeRegistry.Construct(x =>
            {
                x.TypeScanner(new[] { typeof(ICustomer).Assembly });
                x.AddTypeScanner(new CustomTypeScanner());
            });

            var repo = container.GetInstance<IRepository>();

            repo.ShouldNotBeNull();

            var test = container.GetInstance<UsedForScannerOnly>();

            test.Name.ShouldEqual(typeof(UsedForScannerOnly).Name);
        }

        [Test]
        public void can_scan_assembly_and_register_selected_types()
        {
            var container = TypeRegistry.Construct(x =>
            {
                x.TypeScanner(new[] { typeof(ICustomer).Assembly }, new[] { typeof(ICustomer) });
            });

            var customer = container.GetInstance<ICustomer>();

            customer.ShouldNotBeNull();
        }
    }

    public class CustomTypeScanner : IConventionScanner
    {
        public void Execute(TypeContextFactory factory)
        {
            factory.Add(new TypeAction<UsedForScannerOnly,UsedForScannerOnly>());
        }
    }

    public class UsedForScannerOnly
    {
        public UsedForScannerOnly()
        {
            Name = GetType().Name;
        }

        public string Name { get; set; }
    }
}