using System;
using System.Collections.Generic;
using Aya.Extension;
using Sirenix.OdinInspector;
using UnityEngine;
using Tabtale.TTPlugins;

public class GameManager : GameEntity<GameManager>
{
    [FoldoutGroup("PlayerType")]
    public bool PlayerFirst;
    [FoldoutGroup("Player")] public Player PlayerPrefab;
    public new Player Player { get; set; }
    public new List<Player> PlayerList { get; set; } = new List<Player>();
    public List<GirlFollow> YogaGirlList => Player.State.YogaGirlList;
    [HideInInspector]
    public Transform GirlListTran;

    [FoldoutGroup("Misc")]
    public Transform PhaseHandler;

    public GamePhaseType GamePhase { get; set; }
    public GamePhaseHandler CurrentPhase { get; set; }
    public Dictionary<GamePhaseType, GamePhaseHandler> PhaseDic { get; protected set; }
    public Dictionary<Type, GamePhaseHandler> PhaseTypeDic { get; protected set; }
    public List<GamePhaseHandler> PhaseList { get; protected set; }

    public GameState State { get; set; }

    protected override void Awake()
    {
        base.Awake();

        SDKUtil.ClikInit(); 

        Time.timeScale = 1f;
        GamePhase = GamePhaseType.None;
        CurrentPhase = null;
        GirlListTran = transform.Find("GirlList");

        State = GetComponent<GameState>();

        PhaseList = PhaseHandler.GetComponents<GamePhaseHandler>().ToList();
        PhaseDic = PhaseList.ToDictionary(p => p.Type);
        PhaseTypeDic = PhaseList.ToDictionary(p => p.GetType());
    }

    public virtual void Start()
    {
        Level.LevelStart();
    }

    public void Init()
    {
        State.Init();
    }

    public T Get<T>() where T : GamePhaseHandler
    {
        return PhaseTypeDic[typeof(T)] as T;
    }

    public void Enter<T>(params object[] args) where T : GamePhaseHandler
    {
        Enter(typeof(T), args);
    }

    public void Enter(Type phaseType, params object[] args)
    {
        var nextPhase = PhaseTypeDic[phaseType];
        Enter(nextPhase, args);
    }

    public void Enter(GamePhaseType gamePhaseType, params object[] args)
    {
        var nextPhase = PhaseDic[gamePhaseType];
        Enter(nextPhase, args);
    }

    public void Enter(GamePhaseHandler nextPhase, params object[] args)
    {
        if (CurrentPhase != null) CurrentPhase.Exit();
        GamePhase = nextPhase.Type;
        CurrentPhase = nextPhase;
        nextPhase.Enter(args);
    }

    public void Update()
    {
        if (CurrentPhase == null) return;
        CurrentPhase.UpdateImpl();
    }
}