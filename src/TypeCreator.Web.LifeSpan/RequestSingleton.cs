using System.Web;
using TypeCreator.AddStrategy;
using TypeCreator.Creation;

namespace TypeCreator.Web.LifeSpan
{
    public class RequestSingleton : DefaultLifeSpan
    {
        private object _lock = new object();

        public override object Instance(TypeContextFactory factory, IBaseTypeAction typeAction)
        {
            lock (_lock)
            {
                return HttpContext.Current.Items[typeAction.InterfaceType.Name] ??
                       (HttpContext.Current.Items[typeAction.InterfaceType.Name] = base.Instance(factory, typeAction));
            }
        }
    }
}