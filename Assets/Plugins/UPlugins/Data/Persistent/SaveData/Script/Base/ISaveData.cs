/////////////////////////////////////////////////////////////////////////////
//
//  Script   : ISaveData.cs
//  Info     : 存档管理接口
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2018
//
/////////////////////////////////////////////////////////////////////////////
using System;

namespace Aya.Data.Persistent
{
	public enum SaveType
	{
		PlayerPrefs = 0,
		Json = 1,
#if SAVEDATA_XML
        Xml = 2,
#endif
	    Serialization = 3,
    }

	public interface ISaveData
	{
	    void Set<T>(string module, string key, T value);
        void Set<T>(string key, T value);
	    T Get<T>(string module, string key, T defaultValue = default(T));
        T Get<T>(string key, T defaultValue = default(T));

	    void SetValue<T>(string module, string key, T value);
	    void SetValue<T>(string key, T value);

	    void SetObject<T>(string module, string key, T obj);
	    void SetObject<T>(string key, T obj);

	    T GetValue<T>(string module, string key, T defaultValue = default(T));
	    T GetValue<T>(string key, T defaultValue = default(T));

	    T GetObject<T>(string module, string key, T defaultObj = default(T));
	    T GetObject<T>(string key, T defaultObj = default(T));

        void Load(string module);
	    void Load();

	    void LoadAsync(string module, Action onDone);
	    void LoadAsync(Action onDone);

        void Delete(string module, string key);
	    void Delete(string key);
	    
	    void Save(string module);
	    void Save();

	    void SaveAsync(string module, Action onDone);
	    void SaveAsync(Action onDone);

        void Clear(string module);
	    void Clear();

	    void Release();
	    void Release(string module);
	}
}
