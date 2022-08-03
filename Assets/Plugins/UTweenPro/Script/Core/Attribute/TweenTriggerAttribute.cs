using System;

namespace Aya.TweenPro
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TweenTriggerAttribute : Attribute
    {
        public string DisplayName { get; set; }
        public string IconName { get; }

        public TweenTriggerAttribute(string displayName, string iconName = null)
        {
            DisplayName = displayName;
            IconName = iconName;
        }
    }
}
