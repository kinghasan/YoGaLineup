using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "GeneralSetting", menuName = "Setting/General Setting")]
public class GeneralSetting : SettingBase<GeneralSetting>
{
    [FoldoutGroup("Game")] public int DefaultCoin;
    [FoldoutGroup("Game")] public int DefaultKey;
    [FoldoutGroup("Game")] public float LoseWaitDuration;
}
