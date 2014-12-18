using System.Collections.Generic;

namespace TypeCreator.Tests.TestObjects
{
    public interface IGenericProvider<T>
    {
        IEnumerable<T> Get<T>();
    }
}