using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aya.Extension;

public class GameEndless : GamePhaseHandler
{
    public ItemEndless Rainbow;
    public override GamePhaseType Type => GamePhaseType.Endless;

    public override void Enter(params object[] args)
    {
        Player.Animator.transform.rotation = Quaternion.identity;
        Player.Animator.Play("Yoga2", 0, 1f);
        foreach (var girl in Game.YogaGirlList)
        {
            girl.Animator.transform.rotation = Quaternion.identity;
            girl.Animator.Play("Yoga2", 0, 1f);
        }

        foreach(var girl in Level.Level.ItemDic[typeof(ItemEndless)])
        {
            var endless = girl as ItemEndless;
            endless.InitGirl();
        }
    }

    public override void UpdateImpl()
    {
        var pos = Player.transform.position;
        pos.z += Time.deltaTime * 1f * Player.Move.MoveSpeed;
        Player.Position = pos;
    }

    public override void Exit()
    {

    }
}
