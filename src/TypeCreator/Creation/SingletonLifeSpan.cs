using TypeCreator.AddStrategy;

namespace TypeCreator.Creation
{
    public class SingletonLifeSpan : DefaultLifeSpan
    {
        public SingletonLifeSpan()
        {
        }

        private object _instance;
        private object _lock = new object();

        public override object Instance(TypeContextFactory factory, IBaseTypeAction typeAction)
        {
            if (_instance != null) return _instance;

            lock (_lock)
            {
                _instance = base.Instance(factory, typeAction);
            }
            return _instance;
        }
    }
}