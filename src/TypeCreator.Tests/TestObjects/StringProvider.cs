namespace TypeCreator.Tests.TestObjects
{
    public class StringProvider : IProvider
    {
        public string Get()
        {
            return "StringProvider";
        }
    }

    public class MemoryProvider:IProvider
    {
        public string Get()
        {
            return "MemoryProvider";
        }
    }
}