using System;
using UnityEngine;

namespace Aya.Types
{
    public class TypeReferenceAttribute : PropertyAttribute
    {
        public Type Type;

        public TypeReferenceAttribute(Type type)
        {
            Type = type;
        }
    }
}
