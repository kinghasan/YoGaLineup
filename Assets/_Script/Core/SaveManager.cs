using System;
using Aya.Data.Persistent;

[Serializable]
public class SaveSlotData : sObject
{
    // Save slot data

    public SaveSlotData(string key) : base(key)
    {
    }
}

public class SaveManager : GameEntity<SaveManager>
{
    public sInt LevelIndex = new sInt(nameof(LevelIndex), 1);
    public sInt RandLevelIndex = new sInt(nameof(RandLevelIndex), 0);
    public SaveSlotData Data { get; set; } = new SaveSlotData(nameof(SaveSlotData));

    public sInt Coin;
    public sInt Key;


    protected override void Awake()
    {
        base.Awake();
        Coin = new sInt(nameof(Coin), GetSetting<GeneralSetting>().DefaultCoin);
        Key = new sInt(nameof(Key), GetSetting<GeneralSetting>().DefaultKey);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        SaveSync();
    }

    public void Load()
    {
    }

    public void SaveSync()
    {
        sObject<SaveSlotData>.Save(Data);
    }
}