using System.Collections.Generic;

namespace TypeCreator.Tests.TestObjects
{
    public class CustomerService : ICustomerService
    {
        private readonly IGenericProvider<string> _provider;
        private readonly IRepository _repository;

        public CustomerService(IGenericProvider<string> provider,IRepository repository)
        {
            _provider = provider;
            _repository = repository;
        }

        public IEnumerable<string> Get()
        {
            return _provider.Get<string>();
        }

        public string Get2()
        {
            return _repository.Get();
        }
    }
}