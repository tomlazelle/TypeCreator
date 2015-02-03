using System;
using TypeCreator.Creation;

namespace TypeCreator.AddStrategy
{
    public interface IBaseTypeAction
    {
        Type InterfaceType { get; set; }
        Type TypeToCreate { get; set; }
        string Key { get; set; }
        ILifeSpan LifeSpan { get; set; }
        Type[] Ctor { get; set; }
    }

    public interface ITypeAction<TInterface,TConcrete>:IBaseTypeAction
    {

        Func<TInterface, TInterface> AfterCreationDoThis { get; set; }        
    }
}