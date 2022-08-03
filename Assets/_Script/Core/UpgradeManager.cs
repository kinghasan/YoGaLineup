using System;
using System.Collections.Generic;
using System.Reflection;
using Aya.Data.Persistent;
using Aya.Extension;
using UnityEngine;

public class UpgradeManager : GameEntity<UpgradeManager>
{
    public static Dictionary<Type, UpgradeInfo> ConfigDic = new Dictionary<Type, UpgradeInfo>();

    public T GetData<T>(int index) where T : UpgradeData
    {
        return GetInfo<T>().GetData(index);
    }

    public T GetData<T>(Predicate<T> predicate) where T : UpgradeData
    {
        return GetInfo<T>().GetData(predicate);
    }

    public List<T> GetDatas<T>(Predicate<T> predicate = null) where T : UpgradeData
    {
        return GetInfo<T>().GetDatas(predicate);
    }

    public UpgradeInfo<T> GetInfo<T>() where T : UpgradeData
    {
        if (!ConfigDic.TryGetValue(typeof(T), out var config))
        {
            config = Load<T>();
            ConfigDic.Add(typeof(T), config);
        }

        return config as UpgradeInfo<T>;
    }

    public UpgradeInfo<T> Load<T>() where T : UpgradeData
    {
        var path = "Config/" + typeof(T).Name;
        var text = Resources.Load<TextAsset>(path).text;
        var lines = text.Split('\n');
        var dataList = new List<T>();
        var fieldDic = new Dictionary<string, FieldInfo>();
        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            var str = line.Trim();
            if (string.IsNullOrEmpty(str)) continue;

            var strArray = str.Split(',');
            if (i == 0)
            {
                foreach (var fieldName in strArray)
                {
                    var fieldInfo = typeof(T).GetField(fieldName);
                    fieldDic.Add(fieldName, fieldInfo);
                }
            }
            else
            {
                var data = Activator.CreateInstance<T>();
                var index = 0;
                foreach (var filedInfo in fieldDic.Values)
                {
                    var value = strArray[index];
                    if (filedInfo.FieldType == typeof(int))
                    {
                        filedInfo.SetValue(data, value.AsInt());
                    }
                    else if (filedInfo.FieldType == typeof(float))
                    {
                        filedInfo.SetValue(data, value.AsFloat());
                    }
                    else if (filedInfo.FieldType == typeof(string))
                    {
                        filedInfo.SetValue(data, value.AsString());
                    }

                    index++;
                }

                dataList.Add(data);
            }
        }

        var config = new UpgradeInfo<T> {DataList = dataList};
        config.Init();
        return config;
    }
}
