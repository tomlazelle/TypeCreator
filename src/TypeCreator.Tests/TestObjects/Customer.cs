namespace TypeCreator.Tests.TestObjects
{
    public class Customer : ICustomer
    {
        public IProvider Provider { get; set; }

        public string Name { get; set; }
        public virtual string GetName()
        {
            return Name;
        }
    }
}