using System;

namespace Aya.Data.Json
{
    public class JInvalidTypeException : JsonException
    {
        public JInvalidTypeException(JToken token, string method)
            : this($"Try to do {method}() on {token.Type} token! (name=\"{token.Name}\")")
        {
        }

        public JInvalidTypeException(object value, Type targetType)
            : this($"Can not cast from {value?.GetType().ToString() ?? "null"} to {targetType}!")
        {
        }

        public JInvalidTypeException(string message)
            : base(message)
        {
        }
    }

    public class JIndexOutOfRangeException : JsonException
    {
        public JIndexOutOfRangeException(JArray array, int index)
            : this($"Index {index} out of array range! (count={array.Count},name=\"{array.Name}\")")
        {
        }

        public JIndexOutOfRangeException(string message)
            : base(message)
        {
        }
    }
}