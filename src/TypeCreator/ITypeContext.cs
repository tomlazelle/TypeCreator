using System;
using System.Collections.Generic;
using System.Reflection;
using TypeCreator.AddStrategy;

namespace TypeCreator
{
    public interface ITypeContext:IDisposable
    {

        void Add(IBaseTypeAction typeAction);

//        void Add<TInterface, TConcrete>(string key, Func<TInterface, TInterface> func = null, LifeSpans lifeSpan = LifeSpans.Unique);
//        void Add(Type TInterface, Type TConcrete,string key, LifeSpans lifeSpan = LifeSpans.Unique);

        T GetInstance<T>();
        object GetInstance(Type type);
        T GetInstance<T>(string key);
        object GetInstance(Type type, string key);

        IEnumerable<T> GetAllInstances<T>();
        IEnumerable<object> GetAllInstances(Type type);

        void TypeScanner(Assembly[] assemblies);
        void TypeScanner(Assembly[] assemblies, Type[] types);

    }
}