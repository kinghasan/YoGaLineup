using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SettingBase<TSetting> : ScriptableObject where TSetting : SettingBase<TSetting>
{
    #region Ins / Load

    public static TSetting Ins
    {
        get
        {
            if (_instance == null) _instance = Load();
            return _instance;
        }
    }

    private static TSetting _instance;

    public static TSetting Load()
    {
        var setting = Instantiate(Resources.Load<TSetting>("Setting/" + typeof(TSetting).Name));
        setting.Init();
        return setting;
    } 

    #endregion

    public virtual void Init()
    {

    }
}
