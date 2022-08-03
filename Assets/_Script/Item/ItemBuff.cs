using Aya.Types;
using Sirenix.OdinInspector;
using UnityEngine;

public class ItemBuff : ItemBase<Player>
{
    [BoxGroup("Buff"), TypeReference(typeof(BuffBase))] public TypeReference Type;
    [BoxGroup("Buff")] public float Duration = 1f;
    [BoxGroup("Buff")] public float[] Args;
    [BoxGroup("Buff"), AssetsOnly] public GameObject[] Assets;
    [BoxGroup("Buff")] public AnimationCurve[] Curves;

    [BoxGroup("Tip"), Multiline(10), HideLabel] public string Tip;

    public override void OnTargetEffect(Player target)
    {
        target.Buff.AddBuff(Type.Type, Duration, Args, Assets, Curves);
    }
}