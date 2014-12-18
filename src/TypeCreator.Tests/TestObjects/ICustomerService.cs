using System.Collections.Generic;

namespace TypeCreator.Tests.TestObjects
{
    public interface ICustomerService
    {
        IEnumerable<string> Get();
        string Get2();
    }
}