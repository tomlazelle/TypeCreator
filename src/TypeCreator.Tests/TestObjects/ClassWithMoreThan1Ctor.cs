using System.Collections.Generic;

namespace TypeCreator.Tests.TestObjects
{
    public interface IClassWithMoreThan1Ctor
    {
        IDictionary<string, object> ReceivedValues { get; set; }
    }

    public class ClassWithMoreThan1Ctor : IClassWithMoreThan1Ctor
    {


        public IDictionary<string, object> ReceivedValues { get; set; }
    

        public ClassWithMoreThan1Ctor()
        {
            ReceivedValues = new Dictionary<string, object>();
        }

        public ClassWithMoreThan1Ctor(ICustomer value)
        {            
            ReceivedValues = new Dictionary<string, object>();

            ReceivedValues.Add("value",value);
        }

        public ClassWithMoreThan1Ctor(ICustomer value,IGenericProvider<ICustomer> value2)
        {
            ReceivedValues = new Dictionary<string, object>();

            ReceivedValues.Add("value", value);
            ReceivedValues.Add("value2", value2);
        }
    }
}