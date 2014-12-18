using System;
using System.Collections.Generic;
using System.Linq;
using TypeCreator.AddStrategy;

namespace TypeCreator.Creation
{

    public class DefaultLifeSpan:ILifeSpan
    {
        public virtual object Instance(TypeContextFactory factory,IBaseTypeAction typeAction)
        {

            var concreteResult = GetTypeWithCtorParams(typeAction, factory) ?? Activator.CreateInstance(typeAction.TypeToCreate);
            if (typeAction.GetType().GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ITypeAction<,>)))
            {
                dynamic typeContainer = typeAction;
                if (typeContainer.FuncToExecute != null)
                {
                    concreteResult = typeContainer.FuncToExecute((dynamic)concreteResult);
                }
            }

            return concreteResult;
        }

        private object GetTypeWithCtorParams(IBaseTypeAction container, TypeContextFactory factory)
        {
            var ctors = container.TypeToCreate.GetConstructors().Where(x => x.GetParameters().Any());

            object concreteResult = null;

            if (ctors.Any())
            {
                var ctorParams = ctors.First().GetParameters();

                IList<object> args = new List<object>();

                foreach (var param in ctorParams)
                {
                    var foundType = factory.FindTypeFromParameter(param.ParameterType);

                    if (foundType != null)
                    {
                        var instanceToAdd = factory.GetInstance(foundType.RegisteredTypeAction.InterfaceType);

                        args.Add(instanceToAdd);
                    }
                }


                concreteResult = container.TypeToCreate.GetConstructor(args.Select(x => x.GetType()).ToArray()).Invoke(args.ToArray());
            }

            return concreteResult;
        }
    }
}