using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : GamePhaseHandler
{
    public override GamePhaseType Type => GamePhaseType.Pause;

    public override void Enter(params object[] args)
    {
        Dispatch(GameEvent.Pause);
    }

    public override void UpdateImpl()
    {

    }

    public override void Exit()
    {

    }
}
