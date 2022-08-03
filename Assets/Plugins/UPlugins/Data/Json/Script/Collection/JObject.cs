using System.Collections.Generic;

namespace Aya.Data.Json
{
    public class JObject : JCollection
    {
        protected IDictionary<string, JToken> Dict;

        public new static JObject Parse(string json)
        {
            return JToken.Parse(json) as JObject;
        }

        public static JObject OptParse(string json)
        {
            var ret = JToken.OptParse(json) as JObject;
            if (ret == null)
            {
                ret = new JObject();
            }

            return ret;
        }

        public static JObject OptParse(string json, JObject defaultValue)
        {
            var ret = JToken.OptParse(json) as JObject;
            if (ret == null)
            {
                ret = new JObject();
            }
            return defaultValue;
        }

        public static explicit operator JObject(string json)
        {
            return Parse(json);
        }

        public JObject() : base(JType.Object)
        {
            Dict = new Dictionary<string, JToken>();
        }

        public override JToken this[int index]
        {
            get { return this[index.ToString()]; }
            set { this[index.ToString()] = value; }
        }

        public override JToken this[string name]
        {
            get
            {
                JToken token;
                Dict.TryGetValue(name, out token);
                if (token == null)
                {
                    token = new JNone(name);
                }
                return token;
            }
            set
            {
                if (value == null)
                {
                    value = new JNull();
                }
                if (Dict.ContainsKey(name))
                {
                    Dict[name] = value;
                }
                else
                {
                    Dict.Add(name, value);
                }
                value.Name = name;
            }
        }

        public override int Count => Dict.Count;

        public void Add(string name, JToken value)
        {
            this[name] = value;
        }

        public void Remove(string name)
        {
            Dict.Remove(name);
        }

        public override IEnumerator<JToken> GetEnumerator()
        {
            return Dict.Values.GetEnumerator();
        }

        public IEnumerable<string> Keys => Dict.Keys;

        public override JObject AsObject()
        {
            return this;
        }

        public override JObject GetObject()
        {
            return this;
        }

        public override JObject OptObject(JObject def)
        {
            return this;
        }

        public override void Write(JsonWriter writer)
        {
            writer.WriteObjectStart();
            foreach (var token in this)
            {
                writer.WritePropertyName(token.Name);
                token.Write(writer);
            }
            writer.WriteObjectEnd();
        }
    }
}