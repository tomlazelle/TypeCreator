namespace TypeCreator
{
    public static class SetterExtensions
    {
        public static void SetProperties<T>(this ITypeContext context, T typeToSetProperties) where T : class
        {
            foreach (var property in typeToSetProperties.GetType().GetProperties())
            {
                var instance = context.GetInstance(property.PropertyType);

                if (instance != null)
                {
                    typeToSetProperties.GetType().GetProperty(property.Name).SetValue(typeToSetProperties, instance);
                }
            }
        }
    }
}