using System.Collections.Generic;
using System.Linq;

namespace TypeCreator.Tests.TestObjects
{
    public class GenericProvider<T>:IGenericProvider<T>
    {
        public IEnumerable<T> Get<T>()
        {
            var data = new[]
            {
                "this", "is", "a", "test"
            };

            return data.Cast<T>();
        }
    }
}