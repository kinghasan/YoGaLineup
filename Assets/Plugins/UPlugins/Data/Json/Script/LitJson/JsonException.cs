#region Header
/**
 * JsonException.cs
 *   Base class throwed by LitJSON when a parsing error occurs.
 *
 * The authors disclaim copyright to this source code. For more details, see
 * the COPYING file included with this distribution.
 **/
#endregion
using System;

namespace Aya.Data.Json
{
    public class JsonException : ApplicationException
    {
        public JsonException() : base()
        {
        }

        internal JsonException(ParserToken token) : base($"Invalid token '{token}' in input string")
        {
        }

        internal JsonException(ParserToken token, Exception innerException) : base($"Invalid token '{token}' in input string", innerException)
        {
        }

        internal JsonException(int c) : base($"Invalid character '{(char) c}' in input string")
        {
        }

        internal JsonException(int c, Exception innerException) : base($"Invalid character '{(char) c}' in input string", innerException)
        {
        }

        public JsonException(string message) : base(message)
        {
        }

        public JsonException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}