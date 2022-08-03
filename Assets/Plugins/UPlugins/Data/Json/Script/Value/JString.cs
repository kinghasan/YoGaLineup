namespace Aya.Data.Json
{
    public class JString : JValue
    {
        private readonly string _value;

        public JString(string value) : base(JType.String)
        {
            this._value = value;
        }

        public override object Value => _value;

        public override void Write(JsonWriter writer)
        {
            writer.Write(_value);
        }

        public override bool Equals(object obj)
        {
            var other = obj as JString;
            if (other == null)
            {
                return false;
            }
            return _value == other._value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        #region As & Get

        public override string AsString()
        {
            return _value;
        }

        public override string GetString()
        {
            return _value;
        }

        #endregion

        #region Opt

        public override bool OptBool(bool defaultValue = default(bool))
        {
            bool value;
            if (_value != null && bool.TryParse(_value, out value))
            {
                return value;
            }
            return defaultValue;
        }

        public override int OptInt(int defaultValue = default(int))
        {
            int value;
            if (_value != null && int.TryParse(_value, out value))
            {
                return value;
            }
            return defaultValue;
        }

        public override long OptLong(long defaultValue = default(long))
        {
            long value;
            if (_value != null && long.TryParse(_value, out value))
            {
                return value;
            }
            return defaultValue;
        }

        public override float OptFloat(float defaultValue = default(float))
        {
            float value;
            if (_value != null && float.TryParse(_value, out value))
            {
                return value;
            }
            return defaultValue;
        }

        public override double OptDouble(double defaultValue = default(double))
        {
            double value;
            if (_value != null && double.TryParse(_value, out value))
            {
                return value;
            }
            return defaultValue;
        }

        public override string OptString(string defaultValue)
        {
            return _value;
        }

        #endregion
    }
}