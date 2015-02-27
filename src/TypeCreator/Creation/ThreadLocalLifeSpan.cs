using System.Threading;
using TypeCreator.AddStrategy;

namespace TypeCreator.Creation
{
    public class ThreadLocalLifeSpan : DefaultLifeSpan
    {
        private ThreadLocal<object> _data;

        private object _locked = new object();

        public override object Instance(TypeContextFactory factory, IBaseTypeAction typeAction)
        {

            if (_data != null) return _data.Value;

            lock (_locked)
            {
                _data = new ThreadLocal<object>(() => base.Instance(factory, typeAction));
            }

            return _data.Value;
        }
    }
}