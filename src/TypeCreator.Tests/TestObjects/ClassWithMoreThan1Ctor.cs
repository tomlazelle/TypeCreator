using System.Collections.Generic;

namespace TypeCreator.Tests.TestObjects
{
    public class ClassWithMoreThan1Ctor
    {
        
        public IDictionary<string, object> ReceivedValues = new Dictionary<string, object>();

        public ClassWithMoreThan1Ctor()
        {
        }

        public ClassWithMoreThan1Ctor(string value)
        {
            
            ReceivedValues.Add("value",value);
        }

        public ClassWithMoreThan1Ctor(string value,string value2)
        {
            ReceivedValues.Add("value", value);
            ReceivedValues.Add("value2", value2);
        }
    }
}