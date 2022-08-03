using System;

namespace Aya.TweenPro
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TweenerAttribute : Attribute
    {
        public string DisplayName { get; set; }
        public string Group { get; set; }
        public string IconName { get; }

        public TweenerAttribute(string displayName, string group)
        {
            DisplayName = displayName;
            Group = group;
            IconName = null;
        }

        public TweenerAttribute(string displayName, string group, string iconName)
        {
            DisplayName = displayName;
            Group = group;
            IconName = iconName;
        }
    }
}
