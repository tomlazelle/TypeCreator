using System.Web.Http;
using System.Web.Mvc;
using TypeCreator;
using TypeCreator.AddStrategy;
using TypeCreator.Creation;
using TypeCreator.ServiceProvider;
using TypeCreator.Web.LifeSpan;
using WebActivatorEx;
using WebTestApp;
using WebTestApp.Models;

[assembly: PreApplicationStartMethod(typeof (TypeCreatorMvc), "Start")]

namespace WebTestApp
{
    public static class TypeCreatorMvc
    {
        public static void Start()
        {
            var container = TypeRegistry.Construct(x =>
            {
                x.Add(new TypeAction<ICustomer, Customer>
                {
                    AfterCreationDoThis = p =>
                    {
                        p.Name = "tom test";
                        return p;
                    }
                });
                x.Add(new TypeAction<IRepository, Repository>
                {
                    LifeSpan = new SessionSingleton()
                });
            });


            DependencyResolver.SetResolver(new TypeCreatorDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new TypeCreatorDependencyResolver(container);
        }
    }
}