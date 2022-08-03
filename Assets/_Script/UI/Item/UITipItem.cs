using Aya.Extension;
using UnityEngine;
using UnityEngine.UI;

public class UITipItem : GameEntity
{
    public Text Text;
    public float Duration;

    public UITip UiTip => UITip.Ins;

    protected override void Awake()
    {
        base.Awake();
    }

    public UITipItem Show()
    {
        UiTip.ExecuteDelay(() =>
        {
            UiTip.Pool.DeSpawn(this);
        }, Duration);

        return this;
    }

    public UITipItem Set(string text)
    {
        if (Text != null)
        {
            Text.text = text;
        }

        return this;
    }

    public UITipItem Set(string text, Color color)
    {
        if (Text != null)
        {
            Text.text = text;
            Text.color = color;
        }

        return this;
    }
}
