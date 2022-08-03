using Aya.Extension;
using Sirenix.OdinInspector;
using UnityEngine;

public class Player : PlayerBase
{
    public PlayerData Data { get; set; }
    public new string CurrentClip => Control.CurrentClip;

    protected override void Awake()
    {
        base.Awake();
    }

    public void Init()
    {
        InitAllComponent();
        Control.InitYoga();
        Animator.speed = 0;
        Animator.transform.eulerAngles = new Vector3(0f, 90f, 0f);

        Trans.position = Vector3.zero;
        Trans.forward = Vector3.forward;
    }

    public void Start()
    {
        
    }

    public void FixedUpdate()
    {


    }

    public void LateUpdate()
    {

    }

    public void Win()
    {
        if (Game.GamePhase != GamePhaseType.Gaming) return;
        Stop(true);
        Play("Win");
        if (IsPlayer) Game.Enter<GameWin>();
    }

    public void Lose()
    {
        if (Game.GamePhase != GamePhaseType.Gaming) return;
        Stop(false);
        Play("Lose");
        if (IsPlayer) Game.Enter<GameLose>();
    }

    public void Die()
    {
        if (Game.GamePhase != GamePhaseType.Gaming) return;
        State.Hp = 0;
        Stop(false);
        Play("Lose");
        if (IsPlayer) Game.Enter<GameLose>();
    }

    public void Stop(bool win)
    {
        Move.DisableMove();
        if (!win)
        {
            State.RestoreSave();
        }
    }
}
