using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStoreSetting<out TStoreData>
    where TStoreData : StoreData
{
    string SaveKey { get; }
    TStoreData CurrentSelectData { get; }
    TStoreData CurrentUnlockData { get; }
    bool ExistLockData { get; }
    bool CanUnlock { get; }
    bool ExistNeedShow { get; }

    int SelectIndex { get; set; }
    int UnlockIndex { get; set; }
    int BuyCount { get; set; }
    int UnlockProgress { get; set; }

    void PreNextUnlock();
}