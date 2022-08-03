using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWin : GamePhaseHandler
{
    public override GamePhaseType Type => GamePhaseType.Win;

    public override void Enter(params object[] args)
    {
        Camera.Switch("Finish");
        SDKUtil.ClikLevelComplete();
        UI.ShowWindow<UIWin>();
        Dispatch(GameEvent.Win);
    }

    public override void UpdateImpl()
    {

    }

    public override void Exit()
    {

    }
}
