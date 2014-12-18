using System;
using System.Collections.Generic;
using System.Linq;
using TypeCreator.Creation;

namespace TypeCreator.AddStrategy
{
    public class TypeActionFacade
    {
        public readonly IBaseTypeAction RegisteredTypeAction;
        private ILifeSpan _lifeSpan;

        public TypeActionFacade(IBaseTypeAction typeAction)
        {
            RegisteredTypeAction = typeAction;

            if (typeAction.LifeSpan == LifeSpans.Singleton)
            {
                _lifeSpan = new SingletonLifeSpan();
            }
            else
            {
                _lifeSpan = new DefaultLifeSpan();
            }
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
            return _lifeSpan.Instance(factory, RegisteredTypeAction);
        }


    }
}