using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Aya.Extension;

public class ItemPoint : ItemBase<Player>
{
    [BoxGroup("Point")] public List<Transform> GirlList;
    //[BoxGroup("Point")] public int AddValue;
    [BoxGroup("Point")] public float MultiplyValue = 1f;
    [BoxGroup("Point")] public float StopTime = 0f;
    [BoxGroup("Point")] public TMP_Text Text;
    [BoxGroup("Point")] public bool ShowTip;
    [BoxGroup("Point")] public Color GoodTipColor;
    [BoxGroup("Point")] public Color BadTipColor;
    private List<Vector3> CachePosList;

    public string TextValue { get; set; }

    protected override void Awake()
    {
        base.Awake();
        CachePosList = new List<Vector3>();
        foreach(var girl in GirlList)
        {
            CachePosList.Add(girl.localPosition);
        }

        //if (AddValue != 0)
        //{
        //    if (AddValue > 0) TextValue = "＋" + AddValue;
        //    else TextValue = "－" + Mathf.Abs(AddValue);
        //}
        //else
        //{
        //    if (MultiplyValue > 1f)
        //    {
        //        TextValue = "×" + MultiplyValue;
        //    }
        //    else
        //    {
        //        var value = 1f / MultiplyValue;
        //        TextValue = "÷" + value;
        //    }
        //}

        if (Text != null)
        {
            Text.text = TextValue;
        }
    }

    public override void Init()
    {
        base.Init();
        for (var i = 0; i < GirlList.Count; i++)
        {
            var girl = GirlList[i];
            girl.transform.SetParent(transform);
            girl.localPosition = CachePosList[i];
            girl.GetComponentInChildren<Animator>().transform.eulerAngles = new Vector3(0f, 90f, 0f);
        }
    }

    public override void OnTargetEffect(Player target)
    {
        target.Render.AddRender(GirlList, Player.Data.Size, GirlList.Count);
        if (StopTime > 0)
            Player.Move.Stop(StopTime);
    }
}
