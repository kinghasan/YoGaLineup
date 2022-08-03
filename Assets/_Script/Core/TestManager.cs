using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class TestManager : GameEntity<TestManager>
{
    public void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyUp(KeyCode.W))
        {
            GameWin();
        }

        if (Input.GetKeyUp(KeyCode.L))
        {
            GameLose();
        }

        if (Input.GetKeyUp(KeyCode.N))
        {
            NextLevel();
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            AddCoin1000();
        }

        if (Input.GetKeyUp(KeyCode.K))
        {
            AddKey();
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            GoToFinish();
        }
#endif
    }

    [BoxGroup("Game"), Button("Game Win")]
    public void GameWin()
    {
        Game.Enter<GameWin>();
    }

    [BoxGroup("Game"), Button("Game Lose")]
    public void GameLose()
    {
        Game.Enter<GameLose>();
    }

    [BoxGroup("Game"), Button("Next Level")]
    public void NextLevel()
    {
        Level.NextLevel();
    }

    [BoxGroup("Game"), Button("Go To Finish")]
    public void GoToFinish()
    {
        var block = CurrentLevel.GetItems<ItemChangeGamePhase>().Find(i => i.GamePhase == GamePhaseType.Win).GetComponentInParent<LevelBlock>();
        Player.Move.EnterBlock(CurrentLevel.BlockInsList.IndexOf(block));
        Player.Move.MovePath(0f);
    }

    [BoxGroup("Data"), Button("Add Coin 100")]
    public void AddCoin100()
    {
        Save.Coin.Value += 100;
    }
    
    [BoxGroup("Data"), Button("Add Coin 1000")]
    public void AddCoin1000()
    {
        Save.Coin.Value += 1000;
    }

    [BoxGroup("Data"), Button("Add Key 1")]
    public void AddKey()
    {
        Save.Key.Value += 1;
    }

    [BoxGroup("Save"), Button("Clear Save Data"), GUIColor(1f, 0.5f, 0.5f)]
    public void ClearSaveData()
    {
        PlayerPrefs.DeleteAll();
    }
}
