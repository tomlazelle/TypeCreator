using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TypeCreator.AddStrategy;

namespace TypeCreator.Creation
{
    public class TypeContextFactory
    {
        private ConcurrentBag<TypeActionFacade> _types;

        public TypeContextFactory()
        {
            _types = new ConcurrentBag<TypeActionFacade>();
        }

        public IEnumerable<T> GetAllInstances<T>()
        {
            if (_types.All(x => x.InterfaceType != typeof(T)))
            {
                return new List<T>();
            }

            var typesToCreate = _types.Where(x => x.InterfaceType == typeof(T));

            return typesToCreate.Select(typeToCreate => (T)CreateInstance(typeToCreate));
        }

        public IEnumerable<object> GetAllInstances(Type type)
        {
            if (_types.All(x => x.InterfaceType != type))
            {
                return new List<object>();
            }

            var typesToCreate = _types.Where(x => x.InterfaceType == type);

            return typesToCreate.Select(CreateInstance);
        }
        public T GetInstance<T>()
        {
            if (_types.All(x => x.InterfaceType != typeof(T)))
            {
                return default(T);
            }

            var typeToCreate = _types.FirstOrDefault(x => x.InterfaceType == typeof(T));

            return (T)CreateInstance(typeToCreate);
        }

        public void Add(IBaseTypeAction typeAction)
        {
            _types.Add(new TypeActionFacade(typeAction));
        }

        public object GetInstance(Type type)
        {
            if (_types.All(x => x.InterfaceType != type))
            {
                if (!type.FullName.StartsWith("System") && !type.FullName.StartsWith("Microsoft") && !type.FullName.ToLower().Contains("cshtml"))
                {
                    Add(new TypeAction(type, type));
                }
                else
                {
                    return null;
                }

            }


            var typeToCreate = _types.FirstOrDefault(x => x.InterfaceType == type);

            return CreateInstance(typeToCreate);
        }

        public T GetInstance<T>(string key)
        {
            if (_types.All(x => x.InterfaceType != typeof(T) && x.RegisteredTypeAction.Key == key))
            {
                return default(T);
            }

            var typeToCreate = _types.FirstOrDefault(x => x.InterfaceType == typeof(T) && x.RegisteredTypeAction.Key == key);

            return (T)CreateInstance(typeToCreate);
        }

        public object GetInstance(Type type, string key)
        {
            if (_types.All(x => x.InterfaceType != type && x.RegisteredTypeAction.Key == key))
            {
                return null;
            }

            var typeToCreate = _types.FirstOrDefault(x => x.InterfaceType == type && x.RegisteredTypeAction.Key == key);

            return CreateInstance(typeToCreate);
        }

        private object CreateInstance(TypeActionFacade container)
        {
            var concreteResult = container.Instance(this);

            return concreteResult;
        }

        public TypeActionFacade FindTypeFromParameter(Type parameterType)
        {
            Type fallbackType = null;
            Type[] genericArguments = null;

            if (parameterType.IsGenericType)
            {
                fallbackType = parameterType.GetGenericTypeDefinition();

                genericArguments = parameterType.GetGenericArguments();
            }
            var foundType = _types.FirstOrDefault(x => x.InterfaceType == parameterType);

            if (foundType == null && parameterType.IsGenericType && fallbackType != null)
            {
                foundType = _types.FirstOrDefault(x => x.InterfaceType == fallbackType);

                if (foundType != null && foundType.RegisteredTypeAction.TypeToCreate.IsGenericType && !foundType.RegisteredTypeAction.TypeToCreate.GenericTypeArguments.Any())
                {
                    var genericType = foundType.RegisteredTypeAction.TypeToCreate.MakeGenericType(genericArguments);

                    foundType.RegisteredTypeAction.TypeToCreate = genericType;
                }
            }

            return foundType;
        }
    }
}