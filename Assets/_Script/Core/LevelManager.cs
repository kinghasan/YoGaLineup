using System.Collections.Generic;
using Aya.Extension;
using Aya.Util;
using UnityEngine;

public class LevelManager : GameEntity<LevelManager>
{
    public int TestLevelIndex = 1;
    public int StartRandIndex = -1;
    public List<Level> RandList;
    public string RoadTag = "Road";

    public new Level Level { get; set; }
    public GameObject Background { get; set; }
    public virtual void NextLevel()
    {
        Save.LevelIndex++;
        if (Save.LevelIndex >= StartRandIndex && StartRandIndex > 0)
        {
            Save.RandLevelIndex.Value = RandUtil.RandInt(0, RandList.Count);
        }

        LevelStart();
    }

    public virtual void LevelStart()
    {
        UI.HideAllWindow();
        GamePool.DeSpawnAll();
        EffectPool.DeSpawnAll();

        this.ExecuteEndOfFrame(() => {
            Level levelPrefab = null;
            var index = Save.LevelIndex.Value;

            if (Level != null)
            {
                GamePool.DeSpawn(Level);
                Level = null;
            }

            if (index >= StartRandIndex && StartRandIndex > 0)
            {
                index = Save.RandLevelIndex.Value;
                levelPrefab = RandList[index];
            }

            if (TestLevelIndex > 0)
            {
                index = TestLevelIndex;
                Level = null;
            }

            if (levelPrefab == null)
            {
                levelPrefab = Resources.Load<Level>("Level/Level_" + index.ToString("D2"));
            }

            Level = GamePool.Spawn(levelPrefab);
            Level.Trans.SetParent(null);
            Level.Init();
            InitEnvironment();

            Game.Init();
            Game.Enter<GameReady>(); 
        });
    }

    public void InitEnvironment()
    {
        if (Background != null)
        {
            GamePool.DeSpawn(Background.gameObject);
            Background = null;
        }

        var environmentData = GetSetting<EnvironmentSetting>().CurrentEnvironment;
        if (environmentData == null) return;
        RenderSettings.fogColor = environmentData.FogColor;
        RenderSettings.skybox = environmentData.Skybox;
        var roads = GameObject.FindGameObjectsWithTag(RoadTag);
        foreach (var road in roads)
        {
            road.GetComponent<Renderer>().material = environmentData.RoadMat;
        }

        if (environmentData.Background != null)
        {
            Background = Instantiate(environmentData.Background);
        }
    }
}
