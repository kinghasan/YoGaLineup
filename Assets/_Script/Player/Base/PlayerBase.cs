using System;
using System.Collections.Generic;
using Aya.Extension;
using UnityEngine;

public class PlayerBase : ComponentBase<PlayerBase>
{
    public int Index => State.Index;
    public bool IsPlayer => State.IsPlayer;
    public bool IsAi => !State.IsPlayer;
    public bool IsAlive => State.Hp > 0;
    public bool IsDie => State.Hp <= 0;

    [SubComponent] public Player Self { get; set; }
    [SubComponent] public PlayerControl Control { get; set; }
    [SubComponent] public PlayerMove Move { get; set; }
    [SubComponent] public PlayerBuff Buff { get; set; }
    [SubComponent] public PlayerState State { get; set; }
    [SubComponent] public PlayerAi Ai { get; set; }

    [SubComponent] public PlayerHealth Health { get; set; }
    [SubComponent] public PlayerRender Render { get; set; }
}
