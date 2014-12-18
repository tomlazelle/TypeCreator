using System;
namespace TypeCreator
{
    public static class TypeRegistry
    {


        public static ITypeContext Construct(Action<ITypeContext> context)
        {
            var typeContext = new TypeContext();

            context.Invoke(typeContext);

            return typeContext;
        }
    }
}