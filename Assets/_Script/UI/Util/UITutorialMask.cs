using Aya.Data.Persistent;
using UnityEngine;

public class UITutorialMask : GameEntity
{
    public string Key;
    public GameObject Mask;

    protected sBool TutorialShown;

    protected override void Awake()
    {
        base.Awake();
        TutorialShown = new sBool(Key, false);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Refresh();
    }

    public void Refresh()
    {
        Mask.gameObject.SetActive(!TutorialShown);
        if (!TutorialShown)
        {
            TutorialShown.Value = true;
        }
    }
}
