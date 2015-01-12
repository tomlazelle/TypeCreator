using System;

namespace TypeCreator.AddStrategy
{
    public class TypeAction : IBaseTypeAction
    {
        public TypeAction(Type TInterface, Type TConcrete)
        {
            InterfaceType = TInterface;
            TypeToCreate = TConcrete;
            LifeSpan = LifeSpans.Unique;
        }

        public Type InterfaceType { get; set; }

        public Type TypeToCreate { get; set; }

        public string Key { get; set; }

        public LifeSpans LifeSpan { get; set; }

    }

    public class TypeAction<TInterface, TConcrete> : ITypeAction<TInterface, TConcrete>
    {
        public TypeAction()
        {
            InterfaceType = typeof(TInterface);
            TypeToCreate = typeof(TConcrete);
            LifeSpan = LifeSpans.Unique;
        }

        public LifeSpans LifeSpan { get; set; }

        public Type InterfaceType { get; set; }

        public Type TypeToCreate { get; set; }

        public string Key { get; set; }

        public Func<TInterface, TInterface> AfterCreationDoThis { get; set; }
    }
}