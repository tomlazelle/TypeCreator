using System;
using System.Collections.Generic;
using System.Reflection;
using TypeCreator.AddStrategy;
using TypeCreator.Creation;

namespace TypeCreator
{
    internal class TypeContext : ITypeContext
    {

        private TypeContextFactory _factory;

        public TypeContext()
        {
            _factory = new TypeContextFactory();
        }

        public IEnumerable<T> GetAllInstances<T>()
        {
            return _factory.GetAllInstances<T>();
        }

        public IEnumerable<object> GetAllInstances(Type type)
        {
            return _factory.GetAllInstances(type);
        }

        public void Add(IBaseTypeAction typeAction)
        {
            _factory.Add(typeAction);
        }

        public T GetInstance<T>()
        {
            return _factory.GetInstance<T>();
        }

        public object GetInstance(Type type)
        {
            return _factory.GetInstance(type);
        }

        public T GetInstance<T>(string key)
        {
            return _factory.GetInstance<T>(key);
        }

        public object GetInstance(Type type, string key)
        {
            return _factory.GetInstance(type, key);
        }

        public void TypeScanner(Assembly[] assemblies)
        {
            new ConventionScanner().Assemblies(assemblies)
                .Do(x =>
                {
                    var interfaces = x.GetInterfaces();
                    foreach (var type in interfaces)
                    {
                        _factory.Add(new TypeAction(type, x));
                    }
                }).Scan();
        }

        public void TypeScanner(Assembly[] assemblies, Type[] types)
        {
            new ConventionScanner().Assemblies(assemblies)
                .Matches(types)
                .Do(x =>
                {
                    var interfaces = x.GetInterfaces();
                    if (interfaces != null)
                    {
                        foreach (var type in interfaces)
                        {
                            _factory.Add(new TypeAction(type, x));
                        }
                    }

                    _factory.Add(new TypeAction(x, x));
                }).Scan();
        }

        public void Dispose()
        {
            //TODO kill the factory
        }


    }
}