using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TypeCreator.AddStrategy;

namespace TypeCreator.Creation
{
    public class DefaultLifeSpan : ILifeSpan
    {
        public virtual object Instance(TypeContextFactory factory, IBaseTypeAction typeAction)
        {
            var concreteResult = GetInstanceType(factory, typeAction);

            if (typeAction.GetType().GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof (ITypeAction<,>)))
            {
                dynamic typeContainer = typeAction;
                if (typeContainer.AfterCreationDoThis != null)
                {
                    concreteResult = typeContainer.AfterCreationDoThis((dynamic) concreteResult);
                }
            }

            return concreteResult;
        }


        private object GetInstanceType(TypeContextFactory factory, IBaseTypeAction typeAction)
        {
            if (typeAction.Instance != null)
            {
                return typeAction.Instance;
            }

            var concreteResult = GetTypeWithCtorParams(typeAction, factory) ?? Activator.CreateInstance(typeAction.TypeToCreate);

            return concreteResult;
        }



        private bool CtorHasTheseParameters(IEnumerable<ParameterInfo> parameterInfos, Type[] ctorTypes)
        {
            var result = true;
            foreach (var parameterInfo in parameterInfos)
            {
                result = ctorTypes.Contains(parameterInfo.ParameterType);

                if (!result) break;
            }

            return result;
        }

        private object GetTypeWithCtorParams(IBaseTypeAction container, TypeContextFactory factory)
        {
            object concreteResult = null;
            IList<object> args = new List<object>();
            if (container.Ctor != null)
            {
                
                var foundCtor = container.TypeToCreate.GetConstructor(container.Ctor);

                CreateCtorParams(factory, foundCtor.GetParameters(), args);

                concreteResult = foundCtor.Invoke(args.ToArray());
            }
            else
            {
                var ctors = container.TypeToCreate.GetConstructors().Where(x => x.GetParameters().Any());

                if (ctors.Any())
                {
                    var ctorParams = ctors.First().GetParameters();
                   
                    CreateCtorParams(factory, ctorParams, args);

                    concreteResult = container.TypeToCreate.GetConstructor(args.Select(x => x.GetType()).ToArray()).Invoke(args.ToArray());
                }
            }

            return concreteResult;
        }

        private static void CreateCtorParams(TypeContextFactory factory, IEnumerable<ParameterInfo> parameterInfo, IList<object> args)
        {
            foreach (var param in parameterInfo)
            {
                var foundType = factory.FindTypeFromParameter(param.ParameterType);

                if (foundType != null)
                {
                    var instanceToAdd = factory.GetInstance(foundType.RegisteredTypeAction.InterfaceType);

                    args.Add(instanceToAdd);
                }
            }
        }
    }
}