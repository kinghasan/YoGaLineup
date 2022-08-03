using System;
using System.Collections.Generic;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    public static class TypeCaches
    {
        public static BindingFlags DefaultBindingFlags => BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

#if UNITY_EDITOR

        public static Dictionary<Type, TweenerAttribute> TweenerAttributeDic
        {
            get
            {
                if (_tweenerAttributeDic == null)
                {
                    _tweenerAttributeDic = new Dictionary<Type, TweenerAttribute>();
                    var tweenerTypes = TypeCache.GetTypesWithAttribute<TweenerAttribute>();
                    foreach (var type in tweenerTypes)
                    {
                        var attribute = type.GetCustomAttribute<TweenerAttribute>();
                        _tweenerAttributeDic.Add(type, attribute);
                    }
                }

                return _tweenerAttributeDic;
            }
        }

        private static Dictionary<Type, TweenerAttribute> _tweenerAttributeDic;

#endif
    }
}
