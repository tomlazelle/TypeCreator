using TypeCreator.AddStrategy;

namespace TypeCreator.Creation
{
    public interface ILifeSpan
    {
        object Instance(TypeContextFactory factory,IBaseTypeAction typeAction);
    }
}