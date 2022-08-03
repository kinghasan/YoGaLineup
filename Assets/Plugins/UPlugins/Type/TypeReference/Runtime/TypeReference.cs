using System;
using System.Reflection;

namespace Aya.Types
{
    [Serializable]
    public class TypeReference
    {
        public string AssemblyName;
        public string TypeName;

        #region Cache

        public Assembly Assembly
        {
            get
            {
                CacheAssembly();
                return _cacheAssembly;
            }
        }

        private Assembly _cacheAssembly;
        private string _cacheAssemblyName;

        public Type Type
        {
            get
            {
                CacheType();
                return _cacheType;
            }
        }

        private Type _cacheType;
        private string _cacheTypeName;

        public Assembly CacheAssembly()
        {
            if (_cacheAssembly != null && _cacheAssemblyName == AssemblyName) return _cacheAssembly;
            _cacheAssemblyName = AssemblyName;
            _cacheAssembly = TypeCaches.GetAssemblyByName(_cacheAssemblyName);
            return _cacheAssembly;
        }

        public Type CacheType()
        {
            if (_cacheType != null && _cacheAssemblyName == AssemblyName && _cacheTypeName == TypeName) return _cacheType;
            _cacheTypeName = TypeName;
            _cacheType = Assembly.GetType(_cacheTypeName);
            return _cacheType;
        }

        #endregion

        #region Construct

        public TypeReference()
        {

        }

        public TypeReference(string assemblyName, string typeName)
        {
            AssemblyName = assemblyName;
            TypeName = typeName;
        }

        #endregion

        #region Override Operator

        public static implicit operator Type(TypeReference typeReference) => typeReference.Type;
        public static implicit operator TypeReference(Type type) => new TypeReference(type.Assembly.FullName, type.Name);

        #endregion
    }
}