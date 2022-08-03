
public enum GamePhaseType
{
    None = -1,
    Ready,
    Start,
    Gaming,
    Pause,
    Win,
    Lose,
    Endless,
    Reward,
}

public abstract class GamePhaseHandler : GameEntity<GamePhaseHandler>
{
    public abstract GamePhaseType Type { get; }

    public virtual void Enter(params object[] args)
    {

    }

    public virtual void UpdateImpl()
    {

    }

    public virtual void Exit()
    {

    }
}