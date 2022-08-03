using System.Collections;
using System.Collections.Generic;
using Aya.Extension;
using Aya.Util;
using Sirenix.OdinInspector;
using UnityEngine;

public class BreakableData
{
    public Rigidbody Rigidbody;
    public MeshCollider MeshCollider;
    public Vector3 Position;
    public Vector3 EulerAngle;
    public Transform Transform;
}

public class ItemBreakable : ItemBase<Player>
{
    [BoxGroup("Breakable")] public bool EnableBreak;
    [BoxGroup("Breakable"), ShowIf("EnableBreak")] public float ExplodeForce;
    [BoxGroup("Breakable"), ShowIf("EnableBreak")] public Vector3 ExplodeCenter;
    [BoxGroup("Breakable"), ShowIf("EnableBreak")] public float ExplodeRange;
    [BoxGroup("Breakable"), ShowIf("EnableBreak")] public Transform NormalTrans;
    [BoxGroup("Breakable"), ShowIf("EnableBreak")] public Transform BrokenTrans;
    [BoxGroup("Breakable"), ShowIf("EnableBreak")] public List<Transform> BrokenList;

    public bool IsBroken { get; set; }
    public List<BreakableData> BrokenDatas { get; set; } = new List<BreakableData>();

    protected override void Awake()
    {
        base.Awake();
        CacheBreakableDatas();
    }

    public virtual void CacheBreakableDatas()
    {
        BrokenDatas.Clear();
        foreach (var brokenItem in BrokenList)
        {
            var data = new BreakableData
            {
                Rigidbody = brokenItem.GetOrAddComponent<Rigidbody>(),
                MeshCollider = brokenItem.GetOrAddComponent<MeshCollider>(),
                Transform = brokenItem,
                Position = brokenItem.localPosition,
                EulerAngle = brokenItem.localEulerAngles
            };

            BrokenDatas.Add(data);
        }
    }

    public override void Init()
    {
        base.Init();

        if (!EnableBreak) return;

        IsBroken = false;
        NormalTrans.gameObject.SetActive(true);
        BrokenTrans.gameObject.SetActive(false);
        for (var i = 0; i < BrokenList.Count; i++)
        {
            var brokenItem = BrokenList[i];
            var data = BrokenDatas[i];
            brokenItem.gameObject.SetActive(false);
            brokenItem.localPosition = data.Position;
            brokenItem.localEulerAngles = data.EulerAngle;
        }
    }

    [BoxGroup("Breakable"), Button("Auto Cache"), ShowIf("EnableBreak")]
    public void AutoCache()
    {
        BrokenList = BrokenTrans.GetComponentsInChildren<MeshRenderer>(true).ToList(m => m.transform);
        foreach (var item in BrokenList)
        {
            item.GetOrAddComponent<Rigidbody>();
            var mesh = item.GetOrAddComponent<MeshCollider>();
            mesh.convex = true;
        }
    }

    public override void OnTargetEffect(Player target)
    {
        Explode();
    }

    public virtual void Explode()
    {
        if (!EnableBreak) return;
        if (IsBroken) return;

        IsBroken = true;
        NormalTrans.gameObject.SetActive(false);
        BrokenTrans.gameObject.SetActive(true);

        for (var i = 0; i < BrokenDatas.Count; i++)
        {
            var data = BrokenDatas[i];
            data.Transform.gameObject.SetActive(true);
            data.Rigidbody.AddExplosionForce(ExplodeForce, Position + ExplodeCenter, ExplodeRange);
            this.ExecuteDelay(() => { data.Transform.gameObject.SetActive(false); }, RandUtil.RandFloat(3, 6));
        }
    }
}
