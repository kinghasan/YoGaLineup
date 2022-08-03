using System;
using System.Collections.Generic;
using Aya.Data.Persistent;
using Aya.Extension;

public abstract class UpgradeInfo
{
    public virtual void Init()
    {

    }
}

public class UpgradeInfo<T> : UpgradeInfo where T : UpgradeData
{
    public List<T> DataList;
    public Dictionary<int, T> DataDic;
    public int Count => DataList.Count;

    public override void Init()
    {
        DataDic = DataList.ToDictionary(d => d.ID);
    }

    public T this[int id] => DataDic[id];

    public T GetData(int index)
    {
        if (index < 0) return DataList.First();
        if (index >= Count) return DataList.Last();
        return DataList[index];
    }

    public T GetData(Predicate<T> predicate)
    {
        foreach (var data in DataList)
        {
            if (predicate(data)) return data;
        }

        return default;
    }

    public List<T> GetDatas(Predicate<T> predicate = null)
    {
        if (predicate == null) return DataList;
        var result = new List<T>();
        foreach (var data in DataList)
        {
            if (predicate(data)) result.Add(data);
        }

        return result;
    }


    public sInt Level = new sInt(typeof(T).Name, 1);
    public bool IsMaxLevel => Level >= Count;
    public bool CanUpgrade => Level < Count && Current.CanUpgrade();

    public T Current
    {
        get
        {
            var index = Level.Value - 1;
            var data = DataList[index];
            return data;
        }
    }

    public bool Upgrade()
    {
        if (IsMaxLevel) return false;
        if (!Current.CanUpgrade()) return false;
        Current.Upgrade();
        Level.Value += 1;
        return true;
    }
}