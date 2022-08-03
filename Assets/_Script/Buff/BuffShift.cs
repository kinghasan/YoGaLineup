
public class BuffShift : BuffBase
{
    public float SpeedChange => Args[0];

    public override void StartImpl()
    {
        Target.State.SpeedMultiply += SpeedChange;
    }

    public override void UpdateImpl()
    {

    }

    public override void EndImpl()
    {
        Target.State.SpeedMultiply -= SpeedChange;
    }
}
