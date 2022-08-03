#region Header
/**
 * JsonReader.cs
 *   Stream-like access to JSON text.
 *
 * The authors disclaim copyright to this source code. For more details, see
 * the COPYING file included with this distribution.
 **/
#endregion
using System;
using System.Collections.Generic;
using System.IO;

namespace Aya.Data.Json
{
    public enum JsonToken
    {
        None,

        ObjectStart,
        PropertyName,
        ObjectEnd,

        ArrayStart,
        ArrayEnd,

        Int,
        Long,
        Float,
        Double,

        String,

        Boolean,
        Null
    }

    public class JsonReader
    {
        #region Fields

        private static IDictionary<int, IDictionary<int, int[]>> _parseTable;
        private readonly Stack<int> _automatonStack;
        private int _currentInput;
        private int _currentSymbol;
        private bool _endOfJson;
        private bool _endOfInput;
        private readonly Lexer _lexer;
        private bool _parserInString;
        private bool _parserReturn;
        private bool _readStarted;
        private TextReader _reader;
        private readonly bool _readerIsOwned;
        private bool _skipNonMembers;
        private object _tokenValue;
        private JsonToken _token;

        #endregion

        #region Public Properties

        public bool AllowComments
        {
            get { return _lexer.AllowComments; }
            set { _lexer.AllowComments = value; }
        }

        public bool AllowSingleQuotedStrings
        {
            get { return _lexer.AllowSingleQuotedStrings; }
            set { _lexer.AllowSingleQuotedStrings = value; }
        }

        public bool SkipNonMembers
        {
            get { return _skipNonMembers; }
            set { _skipNonMembers = value; }
        }

        public bool EndOfInput => _endOfInput;
        public bool EndOfJson => _endOfJson;
        public JsonToken Token => _token;
        public object Value => _tokenValue;

        #endregion

        #region Constructors

        static JsonReader()
        {
            PopulateParseTable();
        }

        public JsonReader(string jsonText) : this(new StringReader(jsonText), true)
        {
        }

        public JsonReader(TextReader reader) : this(reader, false)
        {
        }

        private JsonReader(TextReader reader, bool owned)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            _parserInString = false;
            _parserReturn = false;
            _readStarted = false;
            _automatonStack = new Stack<int>();
            _automatonStack.Push((int) ParserToken.End);
            _automatonStack.Push((int) ParserToken.Text);
            _lexer = new Lexer(reader);
            _endOfInput = false;
            _endOfJson = false;
            _skipNonMembers = true;
            this._reader = reader;
            _readerIsOwned = owned;
        }

        #endregion

        #region Static Methods

        private static void PopulateParseTable()
        {
            // See section A.2. of the manual for details
            _parseTable = new Dictionary<int, IDictionary<int, int[]>>();
            TableAddRow(ParserToken.Array);
            TableAddCol(ParserToken.Array, '[', '[', (int) ParserToken.ArrayPrime);

            TableAddRow(ParserToken.ArrayPrime);
            TableAddCol(ParserToken.ArrayPrime, '"', (int) ParserToken.Value, (int) ParserToken.ValueRest, ']');
            TableAddCol(ParserToken.ArrayPrime, '[', (int) ParserToken.Value, (int) ParserToken.ValueRest, ']');
            TableAddCol(ParserToken.ArrayPrime, ']', ']');
            TableAddCol(ParserToken.ArrayPrime, '{', (int) ParserToken.Value, (int) ParserToken.ValueRest, ']');
            TableAddCol(ParserToken.ArrayPrime, (int) ParserToken.Number, (int) ParserToken.Value, (int) ParserToken.ValueRest, ']');
            TableAddCol(ParserToken.ArrayPrime, (int) ParserToken.True, (int) ParserToken.Value, (int) ParserToken.ValueRest, ']');
            TableAddCol(ParserToken.ArrayPrime, (int) ParserToken.False, (int) ParserToken.Value, (int) ParserToken.ValueRest, ']');
            TableAddCol(ParserToken.ArrayPrime, (int) ParserToken.Null, (int) ParserToken.Value, (int) ParserToken.ValueRest, ']');

            TableAddRow(ParserToken.Object);
            TableAddCol(ParserToken.Object, '{', '{', (int) ParserToken.ObjectPrime);

            TableAddRow(ParserToken.ObjectPrime);
            TableAddCol(ParserToken.ObjectPrime, '"', (int) ParserToken.Pair, (int) ParserToken.PairRest, '}');
            TableAddCol(ParserToken.ObjectPrime, '}', '}');

            TableAddRow(ParserToken.Pair);
            TableAddCol(ParserToken.Pair, '"', (int) ParserToken.String, ':', (int) ParserToken.Value);

            TableAddRow(ParserToken.PairRest);
            TableAddCol(ParserToken.PairRest, ',', ',', (int) ParserToken.Pair, (int) ParserToken.PairRest);
            TableAddCol(ParserToken.PairRest, '}', (int) ParserToken.Epsilon);

            TableAddRow(ParserToken.String);
            TableAddCol(ParserToken.String, '"', '"', (int) ParserToken.CharSeq, '"');

            TableAddRow(ParserToken.Text);
            TableAddCol(ParserToken.Text, '[', (int) ParserToken.Array);
            TableAddCol(ParserToken.Text, '{', (int) ParserToken.Object);

            TableAddRow(ParserToken.Value);
            TableAddCol(ParserToken.Value, '"', (int) ParserToken.String);
            TableAddCol(ParserToken.Value, '[', (int) ParserToken.Array);
            TableAddCol(ParserToken.Value, '{', (int) ParserToken.Object);
            TableAddCol(ParserToken.Value, (int) ParserToken.Number, (int) ParserToken.Number);
            TableAddCol(ParserToken.Value, (int) ParserToken.True, (int) ParserToken.True);
            TableAddCol(ParserToken.Value, (int) ParserToken.False, (int) ParserToken.False);
            TableAddCol(ParserToken.Value, (int) ParserToken.Null, (int) ParserToken.Null);

            TableAddRow(ParserToken.ValueRest);
            TableAddCol(ParserToken.ValueRest, ',', ',', (int) ParserToken.Value, (int) ParserToken.ValueRest);
            TableAddCol(ParserToken.ValueRest, ']', (int) ParserToken.Epsilon);
        }

        private static void TableAddCol(ParserToken row, int col, params int[] symbols)
        {
            _parseTable[(int) row].Add(col, symbols);
        }

        private static void TableAddRow(ParserToken rule)
        {
            _parseTable.Add((int) rule, new Dictionary<int, int[]>());
        }

        #endregion

        #region Private Methods

        private void ProcessNumber(string number)
        {
            if (number.IndexOf('.') != -1 ||
                number.IndexOf('e') != -1 ||
                number.IndexOf('E') != -1)
            {
                float nFloat;
                if (Single.TryParse(number, out nFloat))
                {
                    _token = JsonToken.Float;
                    _tokenValue = nFloat;
                    return;
                }

                double nDouble;
                if (Double.TryParse(number, out nDouble))
                {
                    _token = JsonToken.Double;
                    _tokenValue = nDouble;
                    return;
                }
            }

            int nInt32;
            if (Int32.TryParse(number, out nInt32))
            {
                _token = JsonToken.Int;
                _tokenValue = nInt32;
                return;
            }

            long nInt64;
            if (Int64.TryParse(number, out nInt64))
            {
                _token = JsonToken.Long;
                _tokenValue = nInt64;
                return;
            }

            ulong nUint64;
            if (UInt64.TryParse(number, out nUint64))
            {
                _token = JsonToken.Long;
                _tokenValue = nUint64;
                return;
            }

            // Shouldn't happen, but just in case, return something
            _token = JsonToken.Int;
            _tokenValue = 0;
        }

        private void ProcessSymbol()
        {
            if (_currentSymbol == '[')
            {
                _token = JsonToken.ArrayStart;
                _parserReturn = true;
            }
            else if (_currentSymbol == ']')
            {
                _token = JsonToken.ArrayEnd;
                _parserReturn = true;

            }
            else if (_currentSymbol == '{')
            {
                _token = JsonToken.ObjectStart;
                _parserReturn = true;
            }
            else if (_currentSymbol == '}')
            {
                _token = JsonToken.ObjectEnd;
                _parserReturn = true;
            }
            else if (_currentSymbol == '"')
            {
                if (_parserInString)
                {
                    _parserInString = false;
                    _parserReturn = true;
                }
                else
                {
                    if (_token == JsonToken.None)
                    {
                        _token = JsonToken.String;
                    }

                    _parserInString = true;
                }
            }
            else if (_currentSymbol == (int) ParserToken.CharSeq)
            {
                _tokenValue = _lexer.StringValue;
            }
            else if (_currentSymbol == (int) ParserToken.False)
            {
                _token = JsonToken.Boolean;
                _tokenValue = false;
                _parserReturn = true;
            }
            else if (_currentSymbol == (int) ParserToken.Null)
            {
                _token = JsonToken.Null;
                _parserReturn = true;
            }
            else if (_currentSymbol == (int) ParserToken.Number)
            {
                ProcessNumber(_lexer.StringValue);
                _parserReturn = true;
            }
            else if (_currentSymbol == (int) ParserToken.Pair)
            {
                _token = JsonToken.PropertyName;
            }
            else if (_currentSymbol == (int) ParserToken.True)
            {
                _token = JsonToken.Boolean;
                _tokenValue = true;
                _parserReturn = true;
            }
        }

        private bool ReadToken()
        {
            if (_endOfInput)
            {
                return false;
            }

            _lexer.NextToken();
            if (_lexer.EndOfInput)
            {
                Close();
                return false;
            }

            _currentInput = _lexer.Token;
            return true;
        }

        #endregion

        public void Close()
        {
            if (_endOfInput)
            {
                return;
            }

            _endOfInput = true;
            _endOfJson = true;
            if (_readerIsOwned)
            {
                _reader.Close();
            }

            _reader = null;
        }

        public bool Read()
        {
            if (_endOfInput)
            {
                return false;
            }

            if (_endOfJson)
            {
                _endOfJson = false;
                _automatonStack.Clear();
                _automatonStack.Push((int) ParserToken.End);
                _automatonStack.Push((int) ParserToken.Text);
            }

            _parserInString = false;
            _parserReturn = false;
            _token = JsonToken.None;
            _tokenValue = null;
            if (!_readStarted)
            {
                _readStarted = true;
                if (!ReadToken())
                {
                    return false;
                }
            }

            while (true)
            {
                if (_parserReturn)
                {
                    if (_automatonStack.Peek() == (int) ParserToken.End)
                    {
                        _endOfJson = true;
                    }

                    return true;
                }

                _currentSymbol = _automatonStack.Pop();
                ProcessSymbol();
                if (_currentSymbol == _currentInput)
                {
                    if (!ReadToken())
                    {
                        if (_automatonStack.Peek() != (int) ParserToken.End)
                        {
                            throw new JsonException("Input doesn't evaluate to proper JSON text");

                        }

                        if (_parserReturn)
                        {
                            return true;
                        }

                        return false;
                    }

                    continue;
                }

                int[] entrySymbols;
                try
                {
                    entrySymbols = _parseTable[_currentSymbol][_currentInput];
                }
                catch (KeyNotFoundException e)
                {
                    throw new JsonException((ParserToken) _currentInput, e);
                }

                if (entrySymbols[0] == (int) ParserToken.Epsilon)
                {
                    continue;
                }

                for (var i = entrySymbols.Length - 1; i >= 0; i--)
                {
                    _automatonStack.Push(entrySymbols[i]);
                }
            }
        }
    }
}