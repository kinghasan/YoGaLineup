#region Header
/**
 * JsonMapper.cs
 *   JSON to .Net object and object to JSON conversions.
 *
 * The authors disclaim copyright to this source code. For more details, see
 * the COPYING file included with this distribution.
 **/
#endregion
#define JSON_UNITY_DATA_TYPE
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
#if JSON_UNITY_DATA_TYPE
using UnityEngine;
using Object = System.Object;
#endif

namespace Aya.Data.Json
{
    internal struct PropertyMetadata
    {
        public MemberInfo Info;
        public bool IsField;
        public Type Type;
    }

    internal struct ArrayMetadata
    {
        public Type ElementType { get; set; }
        public bool IsArray { get; set; }
        public bool IsList { get; set; }
    }

    internal struct ObjectMetadata
    {
        public Type ElementType { get; set; }
        public bool IsDictionary { get; set; }
        public IDictionary<string, PropertyMetadata> Properties { get; set; }
    }

    internal delegate void ExporterFunc(object obj, JsonWriter writer);

    public delegate void ExporterFunc<T>(T obj, JsonWriter writer);

    internal delegate object ImporterFunc(object input);

    public delegate TValue ImporterFunc<TJson, TValue>(TJson input);

    public class JsonMapper
    {
        #region Fields

        private static readonly int MaxNestingDepth;
        private static readonly IFormatProvider DatetimeFormat;
        private static readonly IDictionary<Type, ExporterFunc> BaseExportersTable;
        private static readonly IDictionary<Type, ExporterFunc> CustomExportersTable;
        private static readonly IDictionary<Type, IDictionary<Type, ImporterFunc>> BaseImportersTable;
        private static readonly IDictionary<Type, IDictionary<Type, ImporterFunc>> CustomImportersTable;
        private static readonly IDictionary<Type, ArrayMetadata> ArrayMetadata;
        private static readonly object ArrayMetadataLock = new Object();
        private static readonly IDictionary<Type, IDictionary<Type, MethodInfo>> ConvOps;
        private static readonly object ConvOpsLock = new Object();
        private static readonly IDictionary<Type, ObjectMetadata> ObjectMetadata;
        private static readonly object ObjectMetadataLock = new Object();
        private static readonly IDictionary<Type, IList<PropertyMetadata>> TypeProperties;
        private static readonly object TypePropertiesLock = new Object();
        private static readonly JsonWriter StaticWriter;
        private static readonly object StaticWriterLock = new Object();

        #endregion

        #region Constructors

        static JsonMapper()
        {
            MaxNestingDepth = 100;
            ArrayMetadata = new Dictionary<Type, ArrayMetadata>();
            ConvOps = new Dictionary<Type, IDictionary<Type, MethodInfo>>();
            ObjectMetadata = new Dictionary<Type, ObjectMetadata>();
            TypeProperties = new Dictionary<Type, IList<PropertyMetadata>>();
            StaticWriter = new JsonWriter();
            DatetimeFormat = DateTimeFormatInfo.InvariantInfo;
            BaseExportersTable = new Dictionary<Type, ExporterFunc>();
            CustomExportersTable = new Dictionary<Type, ExporterFunc>();
            BaseImportersTable = new Dictionary<Type, IDictionary<Type, ImporterFunc>>();
            CustomImportersTable = new Dictionary<Type, IDictionary<Type, ImporterFunc>>();
            RegisterBaseExporters();
            RegisterBaseImporters();
        }

        #endregion

        #region Private Methods

        private static void AddArrayMetadata(Type type)
        {
            if (ArrayMetadata.ContainsKey(type))
            {
                return;
            }

            var data = new ArrayMetadata { IsArray = type.IsArray };
            if (type.GetInterface("System.Collections.IList") != null)
            {
                data.IsList = true;
            }

            foreach (var pInfo in type.GetProperties())
            {
                var ignore = false;
                foreach (var attr in pInfo.GetCustomAttributes(false))
                {
                    if (CheckIgnore(attr.GetType()))
                    {
                        ignore = true;
                        break;
                    }
                }

                if (ignore)
                {
                    continue;
                }

                if (pInfo.Name != "Item")
                {
                    continue;
                }

                var parameters = pInfo.GetIndexParameters();
                if (parameters.Length != 1)
                {
                    continue;
                }

                if (parameters[0].ParameterType == typeof(int))
                {
                    data.ElementType = pInfo.PropertyType;
                }
            }

            lock (ArrayMetadataLock)
            {
                try
                {
                    ArrayMetadata.Add(type, data);
                }
                catch (ArgumentException)
                {
                    return;
                }
            }
        }

        private static void AddObjectMetadata(Type type)
        {
            if (ObjectMetadata.ContainsKey(type))
            {
                return;
            }

            var data = new ObjectMetadata();
            if (type.GetInterface("System.Collections.IDictionary") != null)
            {
                data.IsDictionary = true;
            }

            data.Properties = new Dictionary<string, PropertyMetadata>();
            foreach (var pInfo in type.GetProperties())
            {
                var ignore = false;
                foreach (var attr in pInfo.GetCustomAttributes(false))
                {
                    if (CheckIgnore(attr.GetType()))
                    {
                        ignore = true;
                        break;
                    }
                }

                if (ignore)
                {
                    continue;
                }

                if (pInfo.Name == "Item")
                {
                    var parameters = pInfo.GetIndexParameters();
                    if (parameters.Length != 1)
                    {
                        continue;
                    }

                    //if (parameters[0].ParameterType == typeof(string))
                    data.ElementType = pInfo.PropertyType;
                    continue;
                }

                var pData = new PropertyMetadata
                {
                    Info = pInfo,
                    Type = pInfo.PropertyType
                };
                data.Properties.Add(pInfo.Name, pData);
            }

            foreach (var fInfo in type.GetFields())
            {
                var ignore = false;
                foreach (var attr in fInfo.GetCustomAttributes(false))
                {
                    if (CheckIgnore(attr.GetType()))
                    {
                        ignore = true;
                        break;
                    }
                }

                if (ignore)
                {
                    continue;
                }

                var pData = new PropertyMetadata
                {
                    Info = fInfo,
                    IsField = true,
                    Type = fInfo.FieldType
                };
                data.Properties.Add(fInfo.Name, pData);
            }

            lock (ObjectMetadataLock)
            {
                try
                {
                    ObjectMetadata.Add(type, data);
                }
                catch (ArgumentException)
                {
                    return;
                }
            }
        }

        private static void AddTypeProperties(Type type)
        {
            if (TypeProperties.ContainsKey(type))
            {
                return;
            }

            IList<PropertyMetadata> props = new List<PropertyMetadata>();
            foreach (var pInfo in type.GetProperties())
            {
                var ignore = false;
                foreach (var attr in pInfo.GetCustomAttributes(false))
                {
                    if (CheckIgnore(attr.GetType()))
                    {
                        ignore = true;
                        break;
                    }
                }

                if (ignore)
                {
                    continue;
                }

                if (pInfo.Name == "Item")
                {
                    continue;
                }

                var pData = new PropertyMetadata
                {
                    Info = pInfo,
                    IsField = false
                };
                props.Add(pData);
            }

            foreach (var fInfo in type.GetFields())
            {
                var ignore = false;
                foreach (var attr in fInfo.GetCustomAttributes(false))
                {
                    if (CheckIgnore(attr.GetType()))
                    {
                        ignore = true;
                        break;
                    }
                }

                if (ignore)
                {
                    continue;
                }

                var pData = new PropertyMetadata
                {
                    Info = fInfo,
                    IsField = true
                };
                props.Add(pData);
            }

            lock (TypePropertiesLock)
            {
                try
                {
                    TypeProperties.Add(type, props);
                }
                catch (ArgumentException)
                {
                    return;
                }
            }
        }

        private static MethodInfo GetConvOp(Type t1, Type t2)
        {
            lock (ConvOpsLock)
            {
                if (!ConvOps.ContainsKey(t1))
                {
                    ConvOps.Add(t1, new Dictionary<Type, MethodInfo>());
                }
            }

            lock (ConvOpsLock)
            {
                if (ConvOps[t1].ContainsKey(t2))
                {
                    return ConvOps[t1][t2];
                }
            }

            var op = t1.GetMethod("op_Implicit", new Type[] { t2 });
            lock (ConvOpsLock)
            {
                try
                {
                    ConvOps[t1].Add(t2, op);
                }
                catch (ArgumentException)
                {
                    return ConvOps[t1][t2];
                }
            }

            return op;
        }

        private static object ReadValue(Type instType, JsonReader reader)
        {
            reader.Read();
            if (reader.Token == JsonToken.ArrayEnd)
            {
                return null;
            }

            var underlyingType = Nullable.GetUnderlyingType(instType);
            var valueType = underlyingType ?? instType;
            if (reader.Token == JsonToken.Null)
            {
                if (instType.IsClass || underlyingType != null)
                {
                    return null;
                }

                throw new JsonException($"Can't assign null to an Instance of type {instType}");
            }

            if (reader.Token == JsonToken.Double ||
                reader.Token == JsonToken.Float ||
                reader.Token == JsonToken.Int ||
                reader.Token == JsonToken.Long ||
                reader.Token == JsonToken.String ||
                reader.Token == JsonToken.Boolean)
            {
                var jsonType = reader.Value.GetType();
                if (valueType.IsAssignableFrom(jsonType))
                {
                    return reader.Value;
                }

                // If there's a custom importer that fits, use it
                if (CustomImportersTable.ContainsKey(jsonType) && CustomImportersTable[jsonType].ContainsKey(valueType))
                {
                    var importer = CustomImportersTable[jsonType][valueType];
                    return importer(reader.Value);
                }

                // Maybe there's a base importer that works
                if (BaseImportersTable.ContainsKey(jsonType) && BaseImportersTable[jsonType].ContainsKey(valueType))
                {
                    var importer = BaseImportersTable[jsonType][valueType];
                    return importer(reader.Value);
                }

                // Maybe it's an enum
                if (valueType.IsEnum)
                {
                    return Enum.ToObject(valueType, reader.Value);
                }

                // Try using an implicit conversion operator
                var convOp = GetConvOp(valueType, jsonType);
                if (convOp != null)
                {
                    return convOp.Invoke(null, new object[] { reader.Value });
                }

                // No luck
                throw new JsonException($"Can't assign value '{reader.Value}' (type {jsonType}) to type {instType}");
            }

            object instance = null;
            if (reader.Token == JsonToken.ArrayStart)
            {
                AddArrayMetadata(instType);
                var tData = ArrayMetadata[instType];
                if (!tData.IsArray && !tData.IsList)
                {
                    throw new JsonException($"Type {instType} can't act as an array");
                }

                IList list;
                Type elemType;
                if (!tData.IsArray)
                {
                    list = (IList)Activator.CreateInstance(instType);
                    elemType = tData.ElementType;
                }
                else
                {
                    list = new ArrayList();
                    elemType = instType.GetElementType();
                }

                while (true)
                {
                    var item = ReadValue(elemType, reader);
                    if (item == null && reader.Token == JsonToken.ArrayEnd)
                    {
                        break;
                    }

                    list.Add(item);
                }

                if (tData.IsArray)
                {
                    var n = list.Count;
                    if (elemType != null)
                    {
                        instance = Array.CreateInstance(elemType, n);
                        for (var i = 0; i < n; i++)
                        {
                            ((Array)instance).SetValue(list[i], i);
                        }
                    }
                }
                else
                {
                    instance = list;
                }
            }
            else if (reader.Token == JsonToken.ObjectStart)
            {
                AddObjectMetadata(valueType);
                var tData = ObjectMetadata[valueType];
                instance = Activator.CreateInstance(valueType);
                while (true)
                {
                    reader.Read();
                    if (reader.Token == JsonToken.ObjectEnd)
                    {
                        break;
                    }

                    var property = Convert.ToString(reader.Value);
                    if (!string.IsNullOrEmpty(property) && tData.Properties.ContainsKey(property))
                    {
                        var propData = tData.Properties[property];
                        if (propData.IsField)
                        {
                            ((FieldInfo)propData.Info).SetValue(instance, ReadValue(propData.Type, reader));
                        }
                        else
                        {
                            var pInfo = (PropertyInfo)propData.Info;
                            if (pInfo.CanWrite)
                            {
                                pInfo.SetValue(instance, ReadValue(propData.Type, reader), null);
                            }
                            else
                            {
                                ReadValue(propData.Type, reader);
                            }
                        }
                    }
                    else
                    {
                        if (!tData.IsDictionary)
                        {
                            if (!reader.SkipNonMembers)
                            {
                                throw new JsonException($"The type {instType} doesn't have the " + $"property '{property}'");
                            }
                            else
                            {
                                //ReadSkip (reader);
                                continue;
                            }
                        }

                        object key;
                        var keyType = valueType.GetGenericArguments()[0];
                        if (keyType.IsSubclassOf(typeof(Enum)))
                        {
                            key = Enum.Parse(keyType, property);
                        }
                        else
                        {
                            key = Convert.ChangeType(property, keyType);
                        }

                        ((IDictionary)instance).Add(key, ReadValue(tData.ElementType, reader));
                    }
                }
            }

            return instance;
        }

        private static void RegisterBaseExporters()
        {
            BaseExportersTable[typeof(byte)] = delegate (object obj, JsonWriter writer) { writer.Write(Convert.ToInt32((byte)obj)); };
            BaseExportersTable[typeof(char)] = delegate (object obj, JsonWriter writer) { writer.Write(Convert.ToString((char)obj)); };
            BaseExportersTable[typeof(DateTime)] = delegate (object obj, JsonWriter writer) { writer.Write(Convert.ToString((DateTime)obj, DatetimeFormat)); };
            BaseExportersTable[typeof(decimal)] = delegate (object obj, JsonWriter writer) { writer.Write((decimal)obj); };
            BaseExportersTable[typeof(sbyte)] = delegate (object obj, JsonWriter writer) { writer.Write(Convert.ToInt32((sbyte)obj)); };
            BaseExportersTable[typeof(short)] = delegate (object obj, JsonWriter writer) { writer.Write(Convert.ToInt32((short)obj)); };
            BaseExportersTable[typeof(ushort)] = delegate (object obj, JsonWriter writer) { writer.Write(Convert.ToInt32((ushort)obj)); };
            BaseExportersTable[typeof(uint)] = delegate (object obj, JsonWriter writer) { writer.Write(Convert.ToUInt64((uint)obj)); };
            BaseExportersTable[typeof(ulong)] = delegate (object obj, JsonWriter writer) { writer.Write((ulong)obj); };
            BaseExportersTable[typeof(Guid)] = delegate (object obj, JsonWriter writer) { writer.Write(((Guid)obj).ToString()); };
#if JSON_UNITY_DATA_TYPE
            // Texture2D
            BaseExportersTable[typeof(Texture2D)] = (obj, writer) =>
            {
                var bytes = ((Texture2D)obj).EncodeToPNG();
                var str = Convert.ToBase64String(bytes);
                writer.Write(str);
            };
            // Vector2
            BaseExportersTable[typeof(Vector2)] = (obj, writer) =>
            {
                var value = ((Vector2)obj);
                var str = value.x + "," + value.y;
                writer.Write(str);
            };
            // Vector3
            BaseExportersTable[typeof(Vector3)] = (obj, writer) =>
            {
                var value = ((Vector3)obj);
                var str = value.x + "," + value.y + "," + value.z;
                writer.Write(str);
            };
            // Vector4
            BaseExportersTable[typeof(Vector4)] = (obj, writer) =>
            {
                var value = ((Vector4)obj);
                var str = value.x + "," + value.y + "," + value.z + "," + value.w;
                writer.Write(str);
            };
            // Quaternion
            BaseExportersTable[typeof(Quaternion)] = (obj, writer) =>
            {
                var value = ((Quaternion)obj);
                var str = value.x + "," + value.y + "," + value.z + "," + value.w;
                writer.Write(str);
            };
            // Color
            BaseExportersTable[typeof(Color)] = (obj, writer) =>
            {
                var value = ((Color)obj);
                var str = value.r + "," + value.g + "," + value.b + "," + value.a;
                writer.Write(str);
            };
#endif
        }

        private static void RegisterBaseImporters()
        {
            ImporterFunc importer = input => Convert.ToByte((int)input);
            RegisterImporter(BaseImportersTable, typeof(int), typeof(byte), importer);
            importer = input => Convert.ToUInt64((int)input);
            RegisterImporter(BaseImportersTable, typeof(int), typeof(ulong), importer);
            importer = input => Convert.ToInt64((int)input);
            RegisterImporter(BaseImportersTable, typeof(int), typeof(long), importer);
            importer = input => Convert.ToSByte((int)input);
            RegisterImporter(BaseImportersTable, typeof(int), typeof(sbyte), importer);
            importer = input => Convert.ToInt16((int)input);
            RegisterImporter(BaseImportersTable, typeof(int), typeof(short), importer);
            importer = input => Convert.ToUInt16((int)input);
            RegisterImporter(BaseImportersTable, typeof(int), typeof(ushort), importer);
            importer = input => Convert.ToUInt32((int)input);
            RegisterImporter(BaseImportersTable, typeof(int), typeof(uint), importer);
            importer = input => Convert.ToSingle((int)input);
            RegisterImporter(BaseImportersTable, typeof(int), typeof(float), importer);
            importer = input => Convert.ToDouble((int)input);
            RegisterImporter(BaseImportersTable, typeof(int), typeof(double), importer);
            importer = input => Convert.ToDecimal((double)input);
            RegisterImporter(BaseImportersTable, typeof(double), typeof(decimal), importer);
            importer = input => Convert.ToUInt32((long)input);
            RegisterImporter(BaseImportersTable, typeof(long), typeof(uint), importer);
            importer = input => Convert.ToChar((string)input);
            RegisterImporter(BaseImportersTable, typeof(string), typeof(char), importer);
            importer = input => Convert.ToDateTime((string)input, DatetimeFormat);
            RegisterImporter(BaseImportersTable, typeof(string), typeof(DateTime), importer);
            importer = input => new Guid((string)input);
            RegisterImporter(BaseImportersTable, typeof(string), typeof(Guid), importer);
#if JSON_UNITY_DATA_TYPE
            // Texture2D
            importer = input =>
            {
                var texture = new Texture2D(1, 1);
                var bytes = Convert.FromBase64String(((string)input));
                texture.LoadImage(bytes);
                return texture;
            };
            RegisterImporter(BaseImportersTable, typeof(string), typeof(Texture2D), importer);
            // Vector2 
            importer = input =>
            {
                var strs = ((string)input).Split(',');
                var x = Convert.ToSingle(strs[0]);
                var y = Convert.ToSingle(strs[1]);
                var value = new Vector2(x, y);
                return value;
            };
            RegisterImporter(BaseImportersTable, typeof(string), typeof(Vector2), importer);
            // Vector3 
            importer = input =>
            {
                var strs = ((string)input).Split(',');
                var x = Convert.ToSingle(strs[0]);
                var y = Convert.ToSingle(strs[1]);
                var z = Convert.ToSingle(strs[2]);
                var value = new Vector3(x, y, z);
                return value;
            };
            RegisterImporter(BaseImportersTable, typeof(string), typeof(Vector3), importer);
            // Vector4 
            importer = input =>
            {
                var strs = ((string)input).Split(',');
                var x = Convert.ToSingle(strs[0]);
                var y = Convert.ToSingle(strs[1]);
                var z = Convert.ToSingle(strs[2]);
                var w = Convert.ToSingle(strs[3]);
                var value = new Vector4(x, y, z, w);
                return value;
            };
            RegisterImporter(BaseImportersTable, typeof(string), typeof(Vector4), importer);
            // Quaternion 
            importer = input =>
            {
                var strs = ((string)input).Split(',');
                var x = Convert.ToSingle(strs[0]);
                var y = Convert.ToSingle(strs[1]);
                var z = Convert.ToSingle(strs[2]);
                var w = Convert.ToSingle(strs[3]);
                var value = new Quaternion(x, y, z, w);
                return value;
            };
            RegisterImporter(BaseImportersTable, typeof(string), typeof(Quaternion), importer);
            // Color 
            importer = input =>
            {
                var strs = ((string)input).Split(',');
                var r = Convert.ToSingle(strs[0]);
                var g = Convert.ToSingle(strs[1]);
                var b = Convert.ToSingle(strs[2]);
                var a = Convert.ToSingle(strs[3]);
                var value = new Color(r, g, b, a);
                return value;
            };
            RegisterImporter(BaseImportersTable, typeof(string), typeof(Color), importer);
#endif
        }

        private static void RegisterImporter(IDictionary<Type, IDictionary<Type, ImporterFunc>> table, Type jsonType, Type valueType, ImporterFunc importer)
        {
            if (!table.ContainsKey(jsonType))
            {
                table.Add(jsonType, new Dictionary<Type, ImporterFunc>());
            }

            table[jsonType][valueType] = importer;
        }

        private static void WriteValue(object obj, JsonWriter writer, bool writer_is_private, int depth)
        {
            if (depth > MaxNestingDepth)
            {
                throw new JsonException("Max allowed object depth reached while " + $"trying to export from type {obj.GetType()}");
            }

            if (obj == null)
            {
                writer.Write(null);
                return;
            }

            if (obj is string s)
            {
                writer.Write(s);
                return;
            }

            if (obj is float f)
            {
                writer.Write(f);
                return;
            }

            if (obj is double d)
            {
                writer.Write(d);
                return;
            }

            if (obj is int i)
            {
                writer.Write(i);
                return;
            }

            if (obj is bool b)
            {
                writer.Write(b);
                return;
            }

            if (obj is long l)
            {
                writer.Write(l);
                return;
            }

            if (obj is Array array)
            {
                writer.WriteArrayStart();
                foreach (var elem in array)
                {
                    WriteValue(elem, writer, writer_is_private, depth + 1);
                }

                writer.WriteArrayEnd();
                return;
            }

            if (obj is IList list)
            {
                writer.WriteArrayStart();
                foreach (var elem in list)
                {
                    WriteValue(elem, writer, writer_is_private, depth + 1);
                }

                writer.WriteArrayEnd();
                return;
            }

            if (obj is IDictionary dictionary)
            {
                writer.WriteObjectStart();
                foreach (DictionaryEntry entry in dictionary)
                {
                    writer.WritePropertyName(entry.Key.ToString());
                    WriteValue(entry.Value, writer, writer_is_private, depth + 1);
                }

                writer.WriteObjectEnd();
                return;
            }

            var objType = obj.GetType();
            // See if there's a custom exporter for the object
            if (CustomExportersTable.ContainsKey(objType))
            {
                var exporter = CustomExportersTable[objType];
                exporter(obj, writer);
                return;
            }

            // If not, maybe there's a base exporter
            if (BaseExportersTable.ContainsKey(objType))
            {
                var exporter = BaseExportersTable[objType];
                exporter(obj, writer);
                return;
            }

            // Last option, let's see if it's an enum
            if (obj is Enum)
            {
                var eType = Enum.GetUnderlyingType(objType);
                if (eType == typeof(long) || eType == typeof(uint) || eType == typeof(ulong))
                {
                    writer.Write((ulong)obj);
                }
                else
                {
                    writer.Write((int)obj);
                }

                return;
            }

            // Okay, so it looks like the input should be exported as an
            // object
            AddTypeProperties(objType);
            var props = TypeProperties[objType];
            writer.WriteObjectStart();
            foreach (var pData in props)
            {
                if (pData.IsField)
                {
                    writer.WritePropertyName(pData.Info.Name);
                    WriteValue(((FieldInfo)pData.Info).GetValue(obj), writer, writer_is_private, depth + 1);
                }
                else
                {
                    var pInfo = (PropertyInfo)pData.Info;
                    if (pInfo.CanRead)
                    {
                        writer.WritePropertyName(pData.Info.Name);
                        WriteValue(pInfo.GetValue(obj, null), writer, writer_is_private, depth + 1);
                    }
                }
            }

            writer.WriteObjectEnd();
        }

        #endregion

        //        public static string ToJson(KeyValueSet set)
        //        {
        //            return set.ToJson();
        //        }
        //
        //        public static string ToJson<TKey, TValue>(KeyValueSet<TKey, TValue> set)
        //        {
        //            return set.ToJson();
        //        }

        public static string ToJson(object obj, bool pretty = false)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            lock (StaticWriterLock)
            {
                StaticWriter.Reset();
                StaticWriter.PrettyPrint = pretty;
                WriteValue(obj, StaticWriter, true, 0);
                return StaticWriter.ToString();
            }
        }

        public static void ToJson(object obj, JsonWriter writer)
        {
            WriteValue(obj, writer, false, 0);
        }

        public static T ToObject<T>(JsonReader reader)
        {
            return (T)ReadValue(typeof(T), reader);
        }

        public static object ToObject<T>(Type type, JsonReader reader)
        {
            return ReadValue(type, reader);
        }

        public static T ToObject<T>(TextReader reader)
        {
            var jsonReader = new JsonReader(reader);
            return (T)ReadValue(typeof(T), jsonReader);
        }

        public static object ToObject(Type type, TextReader reader)
        {
            var jsonReader = new JsonReader(reader);
            return ReadValue(type, jsonReader);
        }

        public static T ToObject<T>(string json)
        {
            var reader = new JsonReader(json);
            return (T)ReadValue(typeof(T), reader);
        }

        public static object ToObject(Type type, string json)
        {
            var reader = new JsonReader(json);
            return ReadValue(type, reader);
        }

        public static void RegisterExporter<T>(ExporterFunc<T> exporter)
        {
            ExporterFunc exporterWrapper = delegate (object obj, JsonWriter writer) { exporter((T)obj, writer); };
            CustomExportersTable[typeof(T)] = exporterWrapper;
        }

        public static void RegisterImporter<TJson, TValue>(ImporterFunc<TJson, TValue> importer)
        {
            ImporterFunc importerWrapper = input => importer((TJson)input);
            RegisterImporter(CustomImportersTable, typeof(TJson), typeof(TValue), importerWrapper);
        }

        public static void UnRegisterExporters()
        {
            CustomExportersTable.Clear();
        }

        public static void UnRegisterImporters()
        {
            CustomImportersTable.Clear();
        }

        public static bool CheckIgnore(Type type)
        {
            if (type == typeof(JsonIgnoreAttribute) || type == typeof(NonSerializedAttribute))
            {
                return true;
            }

            return false;
        }
    }
}