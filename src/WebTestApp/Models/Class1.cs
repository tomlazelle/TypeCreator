using System;
using System.Threading;

namespace WebTestApp.Models
{
    public class Class1
    {
        public Class1()
        {
            generatedValue = Guid.NewGuid().ToString();
        }

        private string generatedValue;

        public void MethodName()
        {
            ThreadLocal<string> test = new ThreadLocal<string>(() => "test", false);
        }
    }
}