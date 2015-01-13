using System;
using TypeCreator.Creation;

namespace TypeCreator.AddStrategy
{
    public class TypeAction : IBaseTypeAction
    {
        public TypeAction(Type TInterface, Type TConcrete)
        {
            InterfaceType = TInterface;
            TypeToCreate = TConcrete;
            LifeSpan = new DefaultLifeSpan();
        }

        public Type InterfaceType { get; set; }

        public Type TypeToCreate { get; set; }

        public string Key { get; set; }

        public ILifeSpan LifeSpan { get; set; }

    }

    public class TypeAction<TInterface, TConcrete> : ITypeAction<TInterface, TConcrete>
    {
        public TypeAction()
        {
            InterfaceType = typeof(TInterface);
            TypeToCreate = typeof(TConcrete);
            LifeSpan = new DefaultLifeSpan();
        }

        public ILifeSpan LifeSpan { get; set; }

        public Type InterfaceType { get; set; }

        public Type TypeToCreate { get; set; }

        public string Key { get; set; }

        public Func<TInterface, TInterface> AfterCreationDoThis { get; set; }
    }
}