using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NRules.RuleSharp
{
    internal interface ITypeLoader
    {
        Type FindType(string typeName);
        Type[] GetTypes();
    }

    internal class TypeLoader : ITypeLoader
    {
        private readonly List<Assembly> _references = new List<Assembly>();

        public Type[] GetTypes()
        {
            return _references.SelectMany(assembly => assembly.GetTypes()).ToArray();
        }

        public Type FindType(string typeName)
        {
            var typeNameTemp = typeName;
            bool isNullable = false;
            Type generatedType = null;

            if (typeNameTemp.EndsWith("?"))
            {
                int idx = typeName.LastIndexOf('?');

                typeNameTemp = typeName.Substring(0, idx);
                isNullable = true;
            }

            Type type = Type.GetType(typeNameTemp);
            if (type != null)
            {
                generatedType = type;
            }

            if (generatedType == null)
            {
                foreach (var assembly in _references)
                {
                    type = assembly.GetType(typeNameTemp);
                    if (type != null)
                    {
                        generatedType = type;
                        break;
                    }
                }
            }

            if (isNullable && t != null)
            {
                Type nullableType = generatedType;
                generatedType=  nullableType.GetNullabeType(); 
            }

            return generatedType;
        }
        
        public void AddReferences(IEnumerable<Assembly> assemblies)
        {
            _references.AddRange(assemblies);
        }

        public void AddReference(Assembly assembly)
        {
            _references.Add(assembly);
        }
    }
}
