using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReady : GamePhaseHandler
{
    public override GamePhaseType Type => GamePhaseType.Ready;

    public override void Enter(params object[] args)
    {
        Camera.Switch("Ready");
        UI.ShowWindow<UIReady>();
        Dispatch(GameEvent.Ready);
    }

    public override void UpdateImpl()
    {

    }

    public override void Exit()
    {

    }
}
