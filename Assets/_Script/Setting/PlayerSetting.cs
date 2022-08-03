using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int Point;
    public float Size;
    public GameObject ChangeFx;
    public Color Color;
}

[CreateAssetMenu(fileName = "PlayerSetting", menuName = "Setting/Player Setting")]
public class PlayerSetting : SettingBase<PlayerSetting>
{
    public int InitPoint;

    [TableList] public List<PlayerData> PlayerDatas;
}
