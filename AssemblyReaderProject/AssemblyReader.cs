using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AssemblyReaderProject
{
    public static class AssemblyReader
    {
        public static List<Type> GetPublicTypes(string path)
        {
            return Assembly.LoadFrom(path).GetTypes()
                .SelectMany(GetTypes)
                .Where(type => type.IsPublic || type.IsNestedPublic)
                .ToList();
        }

        private static List<Type> GetTypes(Type type)
        {
            var result = new List<Type> {type};
            result.AddRange(type.GetNestedTypes().SelectMany(GetTypes).ToList());
            return result;
        }
    }
}