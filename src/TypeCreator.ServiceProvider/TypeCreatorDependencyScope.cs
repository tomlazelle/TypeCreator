using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Microsoft.Practices.ServiceLocation;

namespace TypeCreator.ServiceProvider
{
    public class TypeCreatorDependencyScope : ServiceLocatorImplBase, IDependencyScope
    {
        protected readonly ITypeContext Container;


        protected TypeCreatorDependencyScope(ITypeContext container)
        {
            Container = container;
        }

        public void Dispose()
        {
            Container.Dispose();
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {

            if (string.IsNullOrEmpty(key))
            {
                return Container.GetInstance(serviceType);
            }
            
            return Container.GetInstance(serviceType, key);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return Container.GetAllInstances(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Container.GetAllInstances(serviceType);
        }
    }
}