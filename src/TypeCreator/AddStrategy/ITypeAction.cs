using System;

namespace TypeCreator.AddStrategy
{
    public interface IBaseTypeAction
    {
        Type InterfaceType { get; set; }
        Type TypeToCreate { get; set; }
        string Key { get; set; }
        LifeSpans LifeSpan { get; set; }        
    }

    public interface ITypeAction<TInterface,TConcrete>:IBaseTypeAction
    {

        Func<TInterface, TInterface> AfterCreationDoThis { get; set; }        
    }
}