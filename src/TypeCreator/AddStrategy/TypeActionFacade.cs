using System;
using TypeCreator.Creation;

namespace TypeCreator.AddStrategy
{
    public class TypeActionFacade
    {
        public readonly IBaseTypeAction RegisteredTypeAction;

        public TypeActionFacade(IBaseTypeAction typeAction)
        {
            RegisteredTypeAction = typeAction;
        }

        public Type InterfaceType
        {
            get
            {
                return RegisteredTypeAction.InterfaceType;
            }
        }

        public object Instance(TypeContextFactory factory)
        {
            return RegisteredTypeAction.LifeSpan.Instance(factory, RegisteredTypeAction);
        }
    }
}