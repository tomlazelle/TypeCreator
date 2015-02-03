using System.Collections.Generic;
using System.Collections.ObjectModel;
using TypeCreator.AddStrategy;

namespace TypeCreator.Creation
{
    public class TypeActionContainer
    {
        private IList<IBaseTypeAction> _typeActions = new List<IBaseTypeAction>();

        public IEnumerable<IBaseTypeAction> Actions()
        {
            return new ReadOnlyCollection<IBaseTypeAction>(_typeActions);
        }

        protected void Add(IBaseTypeAction typeAction)
        {
            _typeActions.Add(typeAction);    
        }
    }
}