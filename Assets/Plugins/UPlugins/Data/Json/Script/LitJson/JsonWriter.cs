#region Header
/**
 * JsonWriter.cs
 *   Stream-like facility to output JSON text.
 *
 * The authors disclaim copyright to this source code. For more details, see
 * the COPYING file included with this distribution.
 **/
#endregion
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Aya.Data.Json
{
    internal enum Condition
    {
        InArray,
        InObject,
        NotAProperty,
        Property,
        Value
    }

    internal class WriterContext
    {
        public int  Count;
        public bool InArray;
        public bool InObject;
        public bool ExpectingValue;
        public int  Padding;
    }

    public class JsonWriter
    {
        #region Fields
        private static readonly NumberFormatInfo NumberFormat;
        private WriterContext _context;
        private Stack<WriterContext> _ctxStack;
        private bool _hasReachedEnd;
        private char[] _hexSeq;
        private int _indentation;
        private int _indentValue;
        private readonly StringBuilder _instStringBuilder;
        private bool _prettyPrint;
        private bool _validate;
        private readonly TextWriter _writer;
        #endregion

        #region Properties

        public int IndentValue
        {
            get { return _indentValue; }
            set
            {
                _indentation = (_indentation / _indentValue) * value;
                _indentValue = value;
            }
        }

        public bool PrettyPrint
        {
            get { return _prettyPrint; }
            set { _prettyPrint = value; }
        }

        public TextWriter TextWriter => _writer;

        public bool Validate
        {
            get { return _validate; }
            set { _validate = value; }
        }

        #endregion

        #region Constructors

        static JsonWriter ()
        {
            NumberFormat = NumberFormatInfo.InvariantInfo;
        }

        public JsonWriter ()
        {
            _instStringBuilder = new StringBuilder ();
            _writer = new StringWriter (_instStringBuilder);
            Init ();
        }

        public JsonWriter(StringBuilder sb) : this(new StringWriter(sb))
        {
        }

        public JsonWriter (TextWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException (nameof(writer));
            }
            this._writer = writer;
            Init ();
        }
        #endregion

        #region Private Methods

        private void DoValidation (Condition cond)
        {
            if (!_context.ExpectingValue)
            {
                _context.Count++;
            }
            if (!_validate)
            {
                return;
            }
            if (_hasReachedEnd)
            {
                throw new JsonException ("A complete JSON symbol has already been written");
            }
            if (cond == Condition.InArray)
            {
                if (!_context.InArray)
                {
                    throw new JsonException("Can't close an array here");
                }
            }
            else if (cond == Condition.InObject)
            {
                if (!_context.InObject || _context.ExpectingValue)
                {
                    throw new JsonException("Can't close an object here");

                }
            }
            else if (cond == Condition.NotAProperty)
            {
                if (_context.InObject && !_context.ExpectingValue)
                {
                    throw new JsonException("Expected a property");

                }
            }
            else if (cond == Condition.Property)
            {
                if (!_context.InObject || _context.ExpectingValue)
                {
                    throw new JsonException("Can't add a property here");

                }
            }
            else if (cond == Condition.Value)
            {
                if (!_context.InArray && (!_context.InObject || !_context.ExpectingValue))
                {
                    throw new JsonException("Can't add a value here");

                }
            }
        }

        private void Init ()
        {
            _hasReachedEnd = false;
            _hexSeq = new char[4];
            _indentation = 0;
            _indentValue = 1;
            _prettyPrint = false;
            _validate = true;
            _ctxStack = new Stack<WriterContext> ();
            _context = new WriterContext ();
            _ctxStack.Push (_context);
        }

        private static void IntToHex (int n, char[] hex)
        {
            for (var i = 0; i < 4; i++)
            {
                var num = n % 16;
                if (num < 10)
                {
                    hex[3 - i] = (char) ('0' + num);
                }
                else
                {
                    hex[3 - i] = (char) ('A' + (num - 10));
                }
                n >>= 4;
            }
        }

        private void Indent ()
        {
            if (_prettyPrint)
            {
                _indentation += _indentValue;
            }
        }

        private void Put (string str)
        {
            if (_prettyPrint && ! _context.ExpectingValue)
            {
                for (var i = 0; i < _indentation; i++)
                {
                    _writer.Write ('\t');
                }

            }
            _writer.Write (str);
        }

        private void PutNewline (bool addComma = true)
        {
            if (addComma && !_context.ExpectingValue && _context.Count > 1)
            {
                _writer.Write (',');
            }
            if (_prettyPrint && _instStringBuilder.Length > 0 && !_context.ExpectingValue)
            {
                _writer.Write ('\n');
            }
        }

        private void PutString(string str)
        {
            Put(String.Empty);
            _writer.Write('"');
            var n = str.Length;
            for (var i = 0; i < n; i++)
            {
                switch (str[i])
                {
                    case '\n':
                        _writer.Write("\\n");
                        continue;

                    case '\r':
                        _writer.Write("\\r");
                        continue;

                    case '\t':
                        _writer.Write("\\t");
                        continue;

                    case '"':
                    case '\\':
                        _writer.Write('\\');
                        _writer.Write(str[i]);
                        continue;

                    case '\f':
                        _writer.Write("\\f");
                        continue;

                    case '\b':
                        _writer.Write("\\b");
                        continue;
                }
                if ((int) str[i] >= 32 && (int) str[i] <= 126)
                {
                    _writer.Write(str[i]);
                    continue;
                }
                // Default, turn into a \uXXXX sequence
                IntToHex((int) str[i], _hexSeq);
                _writer.Write("\\u");
                _writer.Write(_hexSeq);
            }
            _writer.Write('"');
        }

        private void Unindent ()
        {
            if (_prettyPrint)
            {
                _indentation -= _indentValue;
            }
        }
        #endregion

        public override string ToString ()
        {
            if (_instStringBuilder == null)
            {
                return String.Empty;
            }
            return _instStringBuilder.ToString ();
        }

        public void Reset ()
        {
            _hasReachedEnd = false;
            _ctxStack.Clear ();
            _context = new WriterContext ();
            _ctxStack.Push (_context);
            _instStringBuilder?.Remove (0, _instStringBuilder.Length);
        }

        public void Write (bool boolean)
        {
            DoValidation (Condition.Value);
            PutNewline ();
            Put (boolean ? "true" : "false");
            _context.ExpectingValue = false;
        }

        public void Write (decimal number)
        {
            DoValidation (Condition.Value);
            PutNewline ();
            Put (Convert.ToString (number, NumberFormat));
            _context.ExpectingValue = false;
        }

		public void Write(float number)
		{
			DoValidation(Condition.Value);
			PutNewline();
			var str = Convert.ToString(number, NumberFormat);
			Put(str);
		    if (str.IndexOf('.') == -1 && str.IndexOf('E') == -1)
		    {
		        _writer.Write(".0");
		    }
			_context.ExpectingValue = false;
		}

        public void Write (double number)
        {
            DoValidation (Condition.Value);
            PutNewline ();
            var str = Convert.ToString (number, NumberFormat);
            Put (str);
            if (str.IndexOf('.') == -1 && str.IndexOf('E') == -1)
            {
                _writer.Write (".0");
            }
            _context.ExpectingValue = false;
        }

        public void Write (int number)
        {
            DoValidation (Condition.Value);
            PutNewline ();
            Put (Convert.ToString (number, NumberFormat));
            _context.ExpectingValue = false;
        }

        public void Write (long number)
        {
            DoValidation (Condition.Value);
            PutNewline ();
            Put (Convert.ToString (number, NumberFormat));
            _context.ExpectingValue = false;
        }

        public void Write (string str)
        {
            DoValidation (Condition.Value);
            PutNewline ();
            if (str == null)
            {
                Put ("null");
            }
            else
            {
                PutString (str);
            }
            _context.ExpectingValue = false;
        }

        public void Write (ulong number)
        {
            DoValidation (Condition.Value);
            PutNewline ();
            Put (Convert.ToString (number, NumberFormat));
            _context.ExpectingValue = false;
        }

        public void WriteArrayEnd ()
        {
            DoValidation (Condition.InArray);
            PutNewline (false);
            _ctxStack.Pop ();
            if (_ctxStack.Count == 1)
            {
                _hasReachedEnd = true;
            }
            else {
                _context = _ctxStack.Peek ();
                _context.ExpectingValue = false;
            }
            Unindent ();
            Put ("]");
        }

        public void WriteArrayStart ()
        {
            DoValidation (Condition.NotAProperty);
            PutNewline ();
            Put ("[");
            _context = new WriterContext {InArray = true};
            _ctxStack.Push (_context);
            Indent ();
        }

        public void WriteObjectEnd ()
        {
            DoValidation (Condition.InObject);
            PutNewline (false);
            _ctxStack.Pop ();
            if (_ctxStack.Count == 1)
            {
                _hasReachedEnd = true;
            }
            else {
                _context = _ctxStack.Peek ();
                _context.ExpectingValue = false;
            }
            Unindent ();
            Put ("}");
        }

        public void WriteObjectStart ()
        {
            DoValidation (Condition.NotAProperty);
            PutNewline ();
            Put ("{");
            _context = new WriterContext {InObject = true};
            _ctxStack.Push (_context);
            Indent ();
        }

        public void WritePropertyName (string propertyName)
        {
            DoValidation (Condition.Property);
            PutNewline ();
            PutString (propertyName);
            if (_prettyPrint)
            {
                if (propertyName.Length > _context.Padding)
                {
                    _context.Padding = propertyName.Length;
                }
                for (var i = _context.Padding - propertyName.Length; i >= 0; i--)
                {
                    _writer.Write (' ');
                }
                _writer.Write (": ");
            }
            else
            {
                _writer.Write (':');
            }
            _context.ExpectingValue = true;
        }
    }
}
