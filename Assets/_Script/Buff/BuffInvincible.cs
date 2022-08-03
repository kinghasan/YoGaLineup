
public class BuffInvincible : BuffBase
{
    public override void StartImpl()
    {
        Target.State.IsInvincible = true;
    }

    public override void UpdateImpl()
    {

    }

    public override void EndImpl()
    {
        Target.State.IsInvincible = false;
    }
}
