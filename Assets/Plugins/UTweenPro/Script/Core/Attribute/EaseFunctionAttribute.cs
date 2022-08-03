using System;

namespace Aya.TweenPro
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EaseFunctionAttribute : Attribute
    {
        public Type Type { get; set; }
        public string DisplayName { get; set; }
        public string MenuPath { get; set; }

        public EaseFunctionAttribute(Type type, string displayName, string menuPath)
        {
            Type = type;
            DisplayName = displayName;
            MenuPath = menuPath;
        }
    }
}
