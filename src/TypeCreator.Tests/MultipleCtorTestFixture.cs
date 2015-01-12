using System;
using NUnit.Framework;
using Should;
using TypeCreator.Tests.TestObjects;

namespace TypeCreator.Tests
{
    [TestFixture]
    public class MultipleCtorTestFixture
    {
        [Test]
        public void can_use_default_ctor()
        {
            var type = typeof (ClassWithMoreThan1Ctor);

            var result = (ClassWithMoreThan1Ctor) Activator.CreateInstance(type, null);

            result.ReceivedValues.Count.ShouldBeLessThan(1);
        }


        [Test]
        public void can_use_ctor_with_1_parameter()
        {
            var type = typeof(ClassWithMoreThan1Ctor);

            var result = (ClassWithMoreThan1Ctor)Activator.CreateInstance(type, new object[]{"test"});

            result.ReceivedValues.Count.ShouldBeGreaterThanOrEqualTo(1);
        }


        [Test]
        public void can_use_ctor_with_2_parameter()
        {
            var type = typeof(ClassWithMoreThan1Ctor);

            var result = (ClassWithMoreThan1Ctor)Activator.CreateInstance(type, new object[] { "test","test2" });

            result.ReceivedValues.Count.ShouldBeGreaterThanOrEqualTo(2);
        }
    }
}