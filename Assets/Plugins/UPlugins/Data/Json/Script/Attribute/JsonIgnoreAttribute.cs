using System;

namespace Aya.Data.Json
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class JsonIgnoreAttribute : Attribute
    {
    }
}