namespace Aya.Data.Json
{
    public class JBool : JValue
    {
        private readonly bool _value;

        public JBool(bool value) : base(JType.Bool)
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
            var other = obj as JBool;
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

        public override bool AsBool()
        {
            return _value;
        }

        public override bool? GetBool()
        {
            return _value;
        }

        public override bool OptBool(bool defaultValue = default(bool))
        {
            return _value;
        }

        public override string OptString(string defaultValue)
        {
            return _value.ToString();
        }
    }
}