using System.Web;
using TypeCreator.AddStrategy;
using TypeCreator.Creation;

namespace TypeCreator.Web.LifeSpan
{
    public class SessionSingleton : DefaultLifeSpan
    {
        private object _lock = new object();
        public override object Instance(TypeContextFactory factory, IBaseTypeAction typeAction)
        {
            lock (_lock)
            {
                return HttpContext.Current.Session[typeAction.InterfaceType.Name] ??
                       (HttpContext.Current.Session[typeAction.InterfaceType.Name] = base.Instance(factory, typeAction));
            }
        }
    }
}