using System;
using System.Reflection;

namespace Aya.Types
{
    public static class TypeCaches
    {
        public static Assembly[] Assemblies
        {
            get
            {
                if (_assemblies == null)
                {
                    _assemblies = AppDomain.CurrentDomain.GetAssemblies();
                }

                return _assemblies;
            }
        }

        private static Assembly[] _assemblies;

        public static Assembly GetAssemblyByName(string assemblyName)
        {
            foreach (var assembly in Assemblies)
            {
                if (assembly.FullName != assemblyName) continue;
                return assembly;
            }

            return default;
        }
    }
}
