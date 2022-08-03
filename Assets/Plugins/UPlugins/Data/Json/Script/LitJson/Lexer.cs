#region Header
/**
 * Lexer.cs
 *   JSON lexer implementation based on a finite state machine.
 *
 * The authors disclaim copyright to this source code. For more details, see
 * the COPYING file included with this distribution.
 **/
#endregion


using System;
using System.IO;
using System.Text;


namespace Aya.Data.Json
{
    internal class FsmContext
    {
        public bool Return;
        public int NextState;
        public Lexer L;
        public int StateStack;
    }


    internal class Lexer
    {
        #region Fields

        private delegate bool StateHandler(FsmContext ctx);
        private static int[] _fsmReturnTable;
        private static StateHandler[] _fsmHandlerTable;
        private bool _allowComments;
        private bool _allowSingleQuotedStrings;
        private bool _endOfInput;
        private readonly FsmContext _fsmContext;
        private int _inputBuffer;
        private int _inputChar;
        private readonly TextReader _reader;
        private int _state;
        private readonly StringBuilder _stringBuffer;
        private string _stringValue;
        private int _token;
        private int _unichar;

        #endregion

        #region Properties

        public bool AllowComments
        {
            get { return _allowComments; }
            set { _allowComments = value; }
        }

        public bool AllowSingleQuotedStrings
        {
            get { return _allowSingleQuotedStrings; }
            set { _allowSingleQuotedStrings = value; }
        }

        public bool EndOfInput => _endOfInput;
        public int Token => _token;
        public string StringValue => _stringValue;

        #endregion

        #region Constructors

        static Lexer()
        {
            PopulateFsmTables();
        }

        public Lexer(TextReader reader)
        {
            _allowComments = true;
            _allowSingleQuotedStrings = true;
            _inputBuffer = 0;
            _stringBuffer = new StringBuilder(128);
            _state = 1;
            _endOfInput = false;
            this._reader = reader;
            _fsmContext = new FsmContext {L = this};
        }

        #endregion

        #region Static Methods

        private static int HexValue(int digit)
        {
            switch (digit)
            {
                case 'a':
                case 'A':
                    return 10;
                case 'b':
                case 'B':
                    return 11;
                case 'c':
                case 'C':
                    return 12;
                case 'd':
                case 'D':
                    return 13;
                case 'e':
                case 'E':
                    return 14;
                case 'f':
                case 'F':
                    return 15;
                default:
                    return digit - '0';
            }
        }

        private static void PopulateFsmTables()
        {
            // See section A.1. of the manual for details of the finite
            // state machine.
            _fsmHandlerTable = new StateHandler[28]
            {
                State1,
                State2,
                State3,
                State4,
                State5,
                State6,
                State7,
                State8,
                State9,
                State10,
                State11,
                State12,
                State13,
                State14,
                State15,
                State16,
                State17,
                State18,
                State19,
                State20,
                State21,
                State22,
                State23,
                State24,
                State25,
                State26,
                State27,
                State28
            };
            _fsmReturnTable = new int[28]
            {
                (int) ParserToken.Char,
                0,
                (int) ParserToken.Number,
                (int) ParserToken.Number,
                0,
                (int) ParserToken.Number,
                0,
                (int) ParserToken.Number,
                0,
                0,
                (int) ParserToken.True,
                0,
                0,
                0,
                (int) ParserToken.False,
                0,
                0,
                (int) ParserToken.Null,
                (int) ParserToken.CharSeq,
                (int) ParserToken.Char,
                0,
                0,
                (int) ParserToken.CharSeq,
                (int) ParserToken.Char,
                0,
                0,
                0,
                0
            };
        }

        private static char ProcessEscChar(int esc_char)
        {
            switch (esc_char)
            {
                case '"':
                case '\'':
                case '\\':
                case '/':
                    return Convert.ToChar(esc_char);
                case 'n':
                    return '\n';
                case 't':
                    return '\t';
                case 'r':
                    return '\r';
                case 'b':
                    return '\b';
                case 'f':
                    return '\f';
                default:
                    // Unreachable
                    return '?';
            }
        }

        private static bool State1(FsmContext ctx)
        {
            while (ctx.L.GetChar())
            {
                if (ctx.L._inputChar == ' ' ||
                    ctx.L._inputChar >= '\t' &&
                    ctx.L._inputChar <= '\r')
                {
                    continue;
                }
                if (ctx.L._inputChar >= '1' && ctx.L._inputChar <= '9')
                {
                    ctx.L._stringBuffer.Append((char) ctx.L._inputChar);
                    ctx.NextState = 3;
                    return true;
                }
                switch (ctx.L._inputChar)
                {
                    case '"':
                        ctx.NextState = 19;
                        ctx.Return = true;
                        return true;
                    case ',':
                    case ':':
                    case '[':
                    case ']':
                    case '{':
                    case '}':
                        ctx.NextState = 1;
                        ctx.Return = true;
                        return true;
                    case '-':
                        ctx.L._stringBuffer.Append((char) ctx.L._inputChar);
                        ctx.NextState = 2;
                        return true;
                    case '0':
                        ctx.L._stringBuffer.Append((char) ctx.L._inputChar);
                        ctx.NextState = 4;
                        return true;
                    case 'f':
                        ctx.NextState = 12;
                        return true;
                    case 'n':
                        ctx.NextState = 16;
                        return true;
                    case 't':
                        ctx.NextState = 9;
                        return true;
                    case '\'':
                        if (!ctx.L._allowSingleQuotedStrings)
                        {
                            return false;
                        }
                        ctx.L._inputChar = '"';
                        ctx.NextState = 23;
                        ctx.Return = true;
                        return true;
                    case '/':
                        if (!ctx.L._allowComments)
                            return false;
                        ctx.NextState = 25;
                        return true;
                    default:
                        return false;
                }
            }

            return true;
        }

        private static bool State2(FsmContext ctx)
        {
            ctx.L.GetChar();
            if (ctx.L._inputChar >= '1' && ctx.L._inputChar <= '9')
            {
                ctx.L._stringBuffer.Append((char) ctx.L._inputChar);
                ctx.NextState = 3;
                return true;
            }
            switch (ctx.L._inputChar)
            {
                case '0':
                    ctx.L._stringBuffer.Append((char) ctx.L._inputChar);
                    ctx.NextState = 4;
                    return true;
                default:
                    return false;
            }
        }

        private static bool State3(FsmContext ctx)
        {
            while (ctx.L.GetChar())
            {
                if (ctx.L._inputChar >= '0' && ctx.L._inputChar <= '9')
                {
                    ctx.L._stringBuffer.Append((char) ctx.L._inputChar);
                    continue;
                }
                if (ctx.L._inputChar == ' ' ||
                    ctx.L._inputChar >= '\t' &&
                    ctx.L._inputChar <= '\r')
                {
                    ctx.Return = true;
                    ctx.NextState = 1;
                    return true;
                }
                switch (ctx.L._inputChar)
                {
                    case ',':
                    case ']':
                    case '}':
                        ctx.L.UngetChar();
                        ctx.Return = true;
                        ctx.NextState = 1;
                        return true;
                    case '.':
                        ctx.L._stringBuffer.Append((char) ctx.L._inputChar);
                        ctx.NextState = 5;
                        return true;
                    case 'e':
                    case 'E':
                        ctx.L._stringBuffer.Append((char) ctx.L._inputChar);
                        ctx.NextState = 7;
                        return true;
                    default:
                        return false;
                }
            }
            return true;
        }

        private static bool State4(FsmContext ctx)
        {
            ctx.L.GetChar();
            if (ctx.L._inputChar == ' ' ||
                ctx.L._inputChar >= '\t' &&
                ctx.L._inputChar <= '\r')
            {
                ctx.Return = true;
                ctx.NextState = 1;
                return true;
            }
            switch (ctx.L._inputChar)
            {
                case ',':
                case ']':
                case '}':
                    ctx.L.UngetChar();
                    ctx.Return = true;
                    ctx.NextState = 1;
                    return true;
                case '.':
                    ctx.L._stringBuffer.Append((char) ctx.L._inputChar);
                    ctx.NextState = 5;
                    return true;
                case 'e':
                case 'E':
                    ctx.L._stringBuffer.Append((char) ctx.L._inputChar);
                    ctx.NextState = 7;
                    return true;
                default:
                    return false;
            }
        }

        private static bool State5(FsmContext ctx)
        {
            ctx.L.GetChar();
            if (ctx.L._inputChar >= '0' && ctx.L._inputChar <= '9')
            {
                ctx.L._stringBuffer.Append((char) ctx.L._inputChar);
                ctx.NextState = 6;
                return true;
            }
            return false;
        }

        private static bool State6(FsmContext ctx)
        {
            while (ctx.L.GetChar())
            {
                if (ctx.L._inputChar >= '0' && ctx.L._inputChar <= '9')
                {
                    ctx.L._stringBuffer.Append((char) ctx.L._inputChar);
                    continue;
                }
                if (ctx.L._inputChar == ' ' ||
                    ctx.L._inputChar >= '\t' && ctx.L._inputChar <= '\r')
                {
                    ctx.Return = true;
                    ctx.NextState = 1;
                    return true;
                }
                switch (ctx.L._inputChar)
                {
                    case ',':
                    case ']':
                    case '}':
                        ctx.L.UngetChar();
                        ctx.Return = true;
                        ctx.NextState = 1;
                        return true;
                    case 'e':
                    case 'E':
                        ctx.L._stringBuffer.Append((char) ctx.L._inputChar);
                        ctx.NextState = 7;
                        return true;
                    default:
                        return false;
                }
            }
            return true;
        }

        private static bool State7(FsmContext ctx)
        {
            ctx.L.GetChar();
            if (ctx.L._inputChar >= '0' && ctx.L._inputChar <= '9')
            {
                ctx.L._stringBuffer.Append((char) ctx.L._inputChar);
                ctx.NextState = 8;
                return true;
            }
            switch (ctx.L._inputChar)
            {
                case '+':
                case '-':
                    ctx.L._stringBuffer.Append((char) ctx.L._inputChar);
                    ctx.NextState = 8;
                    return true;
                default:
                    return false;
            }
        }

        private static bool State8(FsmContext ctx)
        {
            while (ctx.L.GetChar())
            {
                if (ctx.L._inputChar >= '0' && ctx.L._inputChar <= '9')
                {
                    ctx.L._stringBuffer.Append((char) ctx.L._inputChar);
                    continue;
                }
                if (ctx.L._inputChar == ' ' ||
                    ctx.L._inputChar >= '\t' && ctx.L._inputChar <= '\r')
                {
                    ctx.Return = true;
                    ctx.NextState = 1;
                    return true;
                }
                switch (ctx.L._inputChar)
                {
                    case ',':
                    case ']':
                    case '}':
                        ctx.L.UngetChar();
                        ctx.Return = true;
                        ctx.NextState = 1;
                        return true;
                    default:
                        return false;
                }
            }
            return true;
        }

        private static bool State9(FsmContext ctx)
        {
            ctx.L.GetChar();
            switch (ctx.L._inputChar)
            {
                case 'r':
                    ctx.NextState = 10;
                    return true;
                default:
                    return false;
            }
        }

        private static bool State10(FsmContext ctx)
        {
            ctx.L.GetChar();
            switch (ctx.L._inputChar)
            {
                case 'u':
                    ctx.NextState = 11;
                    return true;
                default:
                    return false;
            }
        }

        private static bool State11(FsmContext ctx)
        {
            ctx.L.GetChar();
            switch (ctx.L._inputChar)
            {
                case 'e':
                    ctx.Return = true;
                    ctx.NextState = 1;
                    return true;
                default:
                    return false;
            }
        }

        private static bool State12(FsmContext ctx)
        {
            ctx.L.GetChar();
            switch (ctx.L._inputChar)
            {
                case 'a':
                    ctx.NextState = 13;
                    return true;
                default:
                    return false;
            }
        }

        private static bool State13(FsmContext ctx)
        {
            ctx.L.GetChar();
            switch (ctx.L._inputChar)
            {
                case 'l':
                    ctx.NextState = 14;
                    return true;
                default:
                    return false;
            }
        }

        private static bool State14(FsmContext ctx)
        {
            ctx.L.GetChar();
            switch (ctx.L._inputChar)
            {
                case 's':
                    ctx.NextState = 15;
                    return true;
                default:
                    return false;
            }
        }

        private static bool State15(FsmContext ctx)
        {
            ctx.L.GetChar();
            switch (ctx.L._inputChar)
            {
                case 'e':
                    ctx.Return = true;
                    ctx.NextState = 1;
                    return true;
                default:
                    return false;
            }
        }

        private static bool State16(FsmContext ctx)
        {
            ctx.L.GetChar();
            switch (ctx.L._inputChar)
            {
                case 'u':
                    ctx.NextState = 17;
                    return true;
                default:
                    return false;
            }
        }

        private static bool State17(FsmContext ctx)
        {
            ctx.L.GetChar();

            switch (ctx.L._inputChar)
            {
                case 'l':
                    ctx.NextState = 18;
                    return true;
                default:
                    return false;
            }
        }

        private static bool State18(FsmContext ctx)
        {
            ctx.L.GetChar();
            switch (ctx.L._inputChar)
            {
                case 'l':
                    ctx.Return = true;
                    ctx.NextState = 1;
                    return true;
                default:
                    return false;
            }
        }

        private static bool State19(FsmContext ctx)
        {
            while (ctx.L.GetChar())
            {
                switch (ctx.L._inputChar)
                {
                    case '"':
                        ctx.L.UngetChar();
                        ctx.Return = true;
                        ctx.NextState = 20;
                        return true;
                    case '\\':
                        ctx.StateStack = 19;
                        ctx.NextState = 21;
                        return true;
                    default:
                        ctx.L._stringBuffer.Append((char) ctx.L._inputChar);
                        continue;
                }
            }
            return true;
        }

        private static bool State20(FsmContext ctx)
        {
            ctx.L.GetChar();
            switch (ctx.L._inputChar)
            {
                case '"':
                    ctx.Return = true;
                    ctx.NextState = 1;
                    return true;
                default:
                    return false;
            }
        }

        private static bool State21(FsmContext ctx)
        {
            ctx.L.GetChar();
            switch (ctx.L._inputChar)
            {
                case 'u':
                    ctx.NextState = 22;
                    return true;
                case '"':
                case '\'':
                case '/':
                case '\\':
                case 'b':
                case 'f':
                case 'n':
                case 'r':
                case 't':
                    ctx.L._stringBuffer.Append(ProcessEscChar(ctx.L._inputChar));
                    ctx.NextState = ctx.StateStack;
                    return true;
                default:
                    return false;
            }
        }

        private static bool State22(FsmContext ctx)
        {
            var counter = 0;
            var mult = 4096;
            ctx.L._unichar = 0;
            while (ctx.L.GetChar())
            {
                if (ctx.L._inputChar >= '0' && ctx.L._inputChar <= '9' ||
                    ctx.L._inputChar >= 'A' && ctx.L._inputChar <= 'F' ||
                    ctx.L._inputChar >= 'a' && ctx.L._inputChar <= 'f')
                {
                    ctx.L._unichar += HexValue(ctx.L._inputChar) * mult;
                    counter++;
                    mult /= 16;
                    if (counter == 4)
                    {
                        ctx.L._stringBuffer.Append(Convert.ToChar(ctx.L._unichar));
                        ctx.NextState = ctx.StateStack;
                        return true;
                    }
                    continue;
                }
                return false;
            }
            return true;
        }

        private static bool State23(FsmContext ctx)
        {
            while (ctx.L.GetChar())
            {
                switch (ctx.L._inputChar)
                {
                    case '\'':
                        ctx.L.UngetChar();
                        ctx.Return = true;
                        ctx.NextState = 24;
                        return true;
                    case '\\':
                        ctx.StateStack = 23;
                        ctx.NextState = 21;
                        return true;
                    default:
                        ctx.L._stringBuffer.Append((char) ctx.L._inputChar);
                        continue;
                }
            }
            return true;
        }

        private static bool State24(FsmContext ctx)
        {
            ctx.L.GetChar();
            switch (ctx.L._inputChar)
            {
                case '\'':
                    ctx.L._inputChar = '"';
                    ctx.Return = true;
                    ctx.NextState = 1;
                    return true;
                default:
                    return false;
            }
        }

        private static bool State25(FsmContext ctx)
        {
            ctx.L.GetChar();
            switch (ctx.L._inputChar)
            {
                case '*':
                    ctx.NextState = 27;
                    return true;
                case '/':
                    ctx.NextState = 26;
                    return true;
                default:
                    return false;
            }
        }

        private static bool State26(FsmContext ctx)
        {
            while (ctx.L.GetChar())
            {
                if (ctx.L._inputChar == '\n')
                {
                    ctx.NextState = 1;
                    return true;
                }
            }
            return true;
        }

        private static bool State27(FsmContext ctx)
        {
            while (ctx.L.GetChar())
            {
                if (ctx.L._inputChar == '*')
                {
                    ctx.NextState = 28;
                    return true;
                }
            }
            return true;
        }

        private static bool State28(FsmContext ctx)
        {
            while (ctx.L.GetChar())
            {
                if (ctx.L._inputChar == '*')
                {
                    continue;
                }
                if (ctx.L._inputChar == '/')
                {
                    ctx.NextState = 1;
                    return true;
                }
                ctx.NextState = 27;
                return true;
            }
            return true;
        }

        #endregion

        private bool GetChar()
        {
            if ((_inputChar = NextChar()) != -1)
            {
                return true;
            }
            _endOfInput = true;
            return false;
        }

        private int NextChar()
        {
            if (_inputBuffer != 0)
            {
                var tmp = _inputBuffer;
                _inputBuffer = 0;
                return tmp;
            }
            return _reader.Read();
        }

        public bool NextToken()
        {
            StateHandler handler;
            _fsmContext.Return = false;
            while (true)
            {
                handler = _fsmHandlerTable[_state - 1];
                if (!handler(_fsmContext))
                {
                    throw new JsonException(_inputChar);
                }
                if (_endOfInput)
                {
                    return false;
                }
                if (_fsmContext.Return)
                {
                    _stringValue = _stringBuffer.ToString();
                    _stringBuffer.Remove(0, _stringBuffer.Length);
                    _token = _fsmReturnTable[_state - 1];
                    if (_token == (int) ParserToken.Char)
                    {
                        _token = _inputChar;
                    }
                    _state = _fsmContext.NextState;
                    return true;
                }
                _state = _fsmContext.NextState;
            }
        }

        private void UngetChar()
        {
            _inputBuffer = _inputChar;
        }
    }
}
