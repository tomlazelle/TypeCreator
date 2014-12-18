using System;

namespace TypeCreator.Tests.TestObjects
{
    public class Repository:IRepository
    {
        private readonly IProvider _provider;

        public Repository(IProvider provider)
        {
            _provider = provider;
        }

        public string Get()
        {
            return _provider.Get();
        }

        private string generatedValue;

        public string GuidTest()
        {
            if (string.IsNullOrEmpty(generatedValue)) generatedValue = Guid.NewGuid().ToString();

            return generatedValue;
        }
    }
}