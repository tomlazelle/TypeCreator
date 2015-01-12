using Castle.DynamicProxy;
using NUnit.Framework;
using Should;
using TypeCreator.AddStrategy;
using TypeCreator.Tests.TestObjects;

namespace TypeCreator.Tests
{
    [TestFixture]
    public class AOPTestFixture
    {
        [Test]
        public void can_proxy_object()
        {

            var proxy = new ProxyGenerator();

            var container = TypeRegistry.Construct(x =>
            {
                x.Add(new TypeAction<ICustomer, Customer>
                {
                    AfterCreationDoThis = f =>
                    {
                        f.Name = "this is a test";
                        return proxy.CreateInterfaceProxyWithTargetInterface(f, new TestInterceptor());
                    }
                });

            });

            var customer = container.GetInstance<ICustomer>();

            customer.GetName().ShouldEqual("not your test name");
        }
    }

    public class TestInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            invocation.ReturnValue = "not your test name";
        }
    }
}