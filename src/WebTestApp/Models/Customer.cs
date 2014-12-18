using System;

namespace WebTestApp.Models
{
    public class Customer:ICustomer
    {
        public string Name { get; set; }
    }

    public interface ICustomer
    {
        string Name { get; set; }
    }

    public interface IRepository
    {
        string Get();
    }

    public class Repository:IRepository
    {
        private string generatedValue;

        public string Get()
        {
            if (string.IsNullOrEmpty(generatedValue)) generatedValue = Guid.NewGuid().ToString();

            return generatedValue;
        }
    }
}