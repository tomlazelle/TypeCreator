using NUnit.Framework;
using Should;
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
            });

            var repo = container.GetInstance<IRepository>();

            repo.ShouldNotBeNull();
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
}