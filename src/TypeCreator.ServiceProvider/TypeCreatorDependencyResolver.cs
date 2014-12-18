using System.Web.Http.Dependencies;

namespace TypeCreator.ServiceProvider
{
    public class TypeCreatorDependencyResolver : TypeCreatorDependencyScope, IDependencyResolver
    {
        public TypeCreatorDependencyResolver(ITypeContext container)
            : base(container)
        {     
        }

        public IDependencyScope BeginScope()
        {
            return new TypeCreatorDependencyResolver(Container);
        }
    }
}