using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TypeCreator.Creation;

namespace TypeCreator
{
    public interface IConventionScanner
    {
        void Execute(TypeContextFactory factory);
    }

    internal class ConventionScanner : IConventionScanner
    {
        private Assembly[] _assemblies;
        private Action<Type, TypeContextFactory> _action;

        private static Lazy<ConventionScanner> _instance = new Lazy<ConventionScanner>(() => new ConventionScanner());
        private Type[] _types;

        public static ConventionScanner Factory
        {
            get { return _instance.Value; }
        }


        public ConventionScanner Assemblies(Assembly[] assemblies)
        {
            _assemblies = assemblies;

            return this;
        }

        public ConventionScanner Matches(Type[] types)
        {
            _types = types;
            return this;
        }

        public ConventionScanner Do(Action<Type, TypeContextFactory> action)
        {
            _action = action;

            return this;
        }


        public void Execute(TypeContextFactory factory)
        {
            foreach (var assembly in _assemblies)
            {


                List<Type> foundTypes = new List<Type>();

                if (_types != null)
                {
                    foreach (var type in _types)
                    {
                        foundTypes.AddRange(assembly.GetTypes().Where(x => !x.IsAbstract && x.CanBeCastTo(type)));
                    }

                }
                else
                {
                    var badNames = new[] { "System", "Microsoft" };

                    foundTypes.AddRange(assembly.GetTypes().Where(x => !x.IsAbstract && !x.IsInterface && !badNames.Any(b => x.Name.StartsWith(b))));
                }

                foreach (var foundType in foundTypes)
                {
                    _action.Invoke(foundType,factory);
                }
            }
        }

        ~ConventionScanner()
        {
            _assemblies = null;
            _action = null;
            _types = null;
        }
    }
}