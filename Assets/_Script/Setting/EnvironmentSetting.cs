using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class EnvironmentData
{
    public Material Skybox;
    public Color FogColor;
    public GameObject Background;
    public Material RoadMat;
}

[Serializable]
public class LevelEnvironmentData
{
    public Sprite Icon;
    public int Index;
}

[CreateAssetMenu(fileName = "EnvironmentSetting", menuName = "Setting/Environment Setting")]
public class EnvironmentSetting : SettingBase<EnvironmentSetting>
{
    [TableList]
    public List<EnvironmentData> EnvironmentDatas;

    [TableList]
    public List<LevelEnvironmentData> LevelEnvironmentDatas;

    public EnvironmentData CurrentEnvironment
    {
        get
        {
            var levelEnvironmentData = CurrentLevelEnvironment;
            if (levelEnvironmentData != null && levelEnvironmentData.Index < EnvironmentDatas.Count)
            {
                var environmentData = EnvironmentDatas[levelEnvironmentData.Index];
                return environmentData;
            }

            return default;
        }
    }

    public LevelEnvironmentData CurrentLevelEnvironment
    {
        get
        {
            if (EnvironmentIndex < LevelEnvironmentDatas.Count)
            {
                var levelEnvironmentData = LevelEnvironmentDatas[EnvironmentIndex];
                return levelEnvironmentData;
            }

            return default;
        }
    }

    public LevelEnvironmentData NextLevelEnvironment
    {
        get
        {
            if (EnvironmentIndex + 1 < LevelEnvironmentDatas.Count)
            {
                var levelEnvironmentData = LevelEnvironmentDatas[EnvironmentIndex + 1];
                return levelEnvironmentData;
            }

            return default;
        }
    }

    public int EnvironmentIndex
    {
        get
        {
            var level = SaveManager.Ins.LevelIndex.Value;
            var index = level / 5;
            var start = index * 5 + 1;
            if (level % 5 == 0)
            {
                start -= 5;
                index--;
            }

            return index;
        }
    }
}
