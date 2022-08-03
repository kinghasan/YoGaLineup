using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : GamePhaseHandler
{
    public override GamePhaseType Type => GamePhaseType.Start;

    public override void Enter(params object[] args)
    {
        Camera.Switch("Finish", Player.Trans, Player.Trans);
        Camera.Switch("Game", Player.Trans, Player.Trans);
        UI.ShowWindow<UIGame>();
        Game.Enter<GamePlay>();
        Dispatch(GameEvent.Start);
    }

    public override void UpdateImpl()
    {

    }

    public override void Exit()
    {

    }
}
