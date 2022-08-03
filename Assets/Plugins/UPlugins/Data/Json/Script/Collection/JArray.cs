using System;
using System.Collections;
using System.Collections.Generic;

namespace Aya.Data.Json
{
    public class JArray : JCollection
    {
        protected IList<JToken> List;

        public new static JArray Parse(string json)
        {
            return JToken.Parse(json) as JArray;
        }

        public static JArray OptParse(string json)
        {
            var ret = JToken.OptParse(json) as JArray;
            if (ret == null)
            {
                ret = new JArray();
            }

            return ret;
        }

        public static JArray OptParse(string json, JArray def)
        {
            var ret = JToken.OptParse(json) as JArray;
            if (ret == null)
            {
                ret = new JArray();
            }

            return def;
        }

        public static explicit operator JArray(string json)
        {
            return Parse(json);
        }

        public static JArray From<T>(IEnumerable<T> list, Func<T, JToken> func)
        {
            var array = new JArray();
            foreach (var item in list)
            {
                array.Add(func(item));
            }

            return array;
        }

        public JArray() : base(JType.Array)
        {
            List = new List<JToken>();
        }

        public JArray(IEnumerable list) : this()
        {
            AddRange(list);
        }

        public override int Count => List.Count;

        public override JToken this[string name]
        {
            get
            {
                int index;
                return int.TryParse(name, out index) ? this[index] : base[name];
            }
            set
            {
                int index;
                if (int.TryParse(name, out index))
                {
                    this[index] = value;
                }
                else
                {
                    base[name] = value;
                }
            }
        }

        public override JToken this[int index]
        {
            get
            {
                if (index < 0 || index >= List.Count)
                {
                    throw new JIndexOutOfRangeException(this, index);
                }
                else
                {
                    return List[index];
                }
            }
            set
            {
                if (index < 0 || index >= List.Count)
                {
                    throw new JIndexOutOfRangeException(this, index);
                }
                else
                {
                    if (value == null)
                    {
                        value = new JNull();
                    }

                    List[index] = value;
                }
            }
        }

        public JToken First => Count > 0 ? this[0] : null;
        public JToken Last => Count > 0 ? this[Count - 1] : null;

        public void Add(JToken item)
        {
            if (item == null)
            {
                item = new JNull();
            }

            List.Add(item);
        }

        public void AddRange(IEnumerable list)
        {
            if (list == null) return;
            foreach (var obj in list)
            {
                Add(Create(obj));
            }
        }

        public void Insert(int index, JToken item)
        {
            if (item == null)
            {
                item = new JNull();
            }

            List.Insert(index, item);
        }

        public bool Remove(JToken item)
        {
            return List.Remove(item);
        }

        public void RemoveAt(int index)
        {
            List.RemoveAt(index);
        }

        public void Clear()
        {
            List.Clear();
        }

        public int IndexOf(JToken item)
        {
            return List.IndexOf(item);
        }

        public bool Contains(JToken item)
        {
            return List.Contains(item);
        }

        public override IEnumerator<JToken> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        public T[] ToArray<T>(Func<JToken, T> func)
        {
            var array = new T[Count];
            if (func != null)
            {
                for (var i = 0; i < Count; i++)
                {
                    array[i] = func(this[i]);
                }
            }

            return array;
        }

        public List<T> ToList<T>(Func<JToken, T> func)
        {
            var list = new List<T>(Count);
            if (func != null)
            {
                for (var i = 0; i < Count; i++)
                {
                    list.Add(func(this[i]));
                }
            }

            return list;
        }

        public override JArray AsArray()
        {
            return this;
        }

        public override JArray GetArray()
        {
            return this;
        }

        public override JArray OptArray(JArray def)
        {
            return this;
        }

        public override void Write(JsonWriter writer)
        {
            writer.WriteArrayStart();
            foreach (var token in this)
            {
                token.Write(writer);
            }

            writer.WriteArrayEnd();
        }
    }
}