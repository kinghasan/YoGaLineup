using UnityEngine;

public abstract class BuffBase
{
    public Player Target;
    public float Duration;
    public float[] Args;
    public GameObject[] Assets;
    public AnimationCurve[] Curves;

    public bool Active;
    public float Timer;
    public float Progress => Timer / Duration;
    public float RemainTime => Duration - Timer;

    public virtual void Start(float duration, float[] args, GameObject[] assets = null, AnimationCurve[] curves = null)
    {
        Duration = duration;
        Timer = 0f;
        Args = args;
        Assets = assets;
        Curves = curves;
        Active = true;
        StartImpl();
    }

    public abstract void StartImpl();

    public virtual void Update(float deltaTime)
    {
        Timer += Time.deltaTime;
        if (Timer >= Duration)
        {
            Timer = Duration;
            End();
        }

        else UpdateImpl();
    }

    public abstract void UpdateImpl();

    public virtual void End()
    {
        Active = false;
        EndImpl();
    }

    public abstract void EndImpl();
}