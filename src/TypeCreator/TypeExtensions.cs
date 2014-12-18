using System;
using System.Linq;

namespace TypeCreator
{
    public static class TypeExtensions
    {
        public static bool CanBeCastTo(this Type type, Type destinationType)
        {
            if (type == (Type)null)
                return false;
            if (type == destinationType)
                return true;

            if (destinationType.IsGenericType && !destinationType.GenericTypeArguments.Any())
            {

                if (destinationType.IsInterface && !type.IsInterface)
                {
                    return type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == destinationType);
                }
            }

            if (type.Name.Contains("HomeController"))
            {
                var xo = string.Empty;
            }
            var t1 = destinationType.IsAssignableFrom(type);
            var t2 = type.GetInterfaces().Any(x=>x.IsAssignableFrom(destinationType));

            return t1 & t2;
        }
    }
}