using Aya.Extension;
using Aya.TweenPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRender : PlayerBase
{
    public float GirlSpawnInterval = 0.5f;
    public Transform RenderTrans;
    public Transform GirlListTrans;

    [SubPoolInstance] public GameObject RenderInstance { get; set; }

    public override void InitComponent()
    {
        foreach (var girl in Game.YogaGirlList)
        {
            girl.transform.SetParent(Level.Level.transform);
            girl.transform.localPosition = new Vector3(1000f, 0, 0);
            Destroy(girl.gameObject);
        }
        Game.YogaGirlList.Clear();
        RenderTrans.SetLocalPositionX(0f);
        RefreshRender(State.Point);
    }

    //public void RefreshRender(int point)
    //{
    //    var datas = PlayerSetting.Ins.PlayerDatas;
    //    var rank = 0;
    //    var data = datas[0];
    //    for (var i = 0; i < datas.Count; i++)
    //    {
    //        if (point >= datas[i].Point)
    //        {
    //            data = datas[i];
    //            rank = i;
    //        }
    //    }

    //    if (State.Rank != rank)
    //    {
    //        State.Rank = rank;
    //        Self.Data = data;

    //        var playerRendererPrefab = AvatarSetting.Ins.SelectedAvatarList[rank];
    //        RefreshRender(playerRendererPrefab);

    //        this.ExecuteNextFrame(() =>
    //        {
    //            if (data.ChangeFx != null && State.PointChanged)
    //            {
    //                SpawnFx(data.ChangeFx, RenderTrans);
    //            }
    //        });
    //    }
    //}

    public void RefreshRender(int point)
    {
        var datas = PlayerSetting.Ins.PlayerDatas;
        var rank = 0;
        var data = datas[0];
        for (var i = 0; i < datas.Count; i++)
        {
            if (point >= datas[i].Point)
            {
                data = datas[i];
                rank = i;
            }
        }

        if (State.Rank != rank)
        {
            State.Rank = rank;
            Self.Data = data;

            State.YoGaGirlPrefab = AvatarSetting.Ins.SelectedAvatarList[rank];
            RefreshRender(State.YoGaGirlPrefab);

            this.ExecuteNextFrame(() =>
            {
                if (data.ChangeFx != null && State.PointChanged)
                {
                    SpawnFx(data.ChangeFx, RenderTrans);
                }
            });
        }
    }

    public void AddRender(List<Transform> girlList, float Size, int value)
    {
        var IsAdd = value > 0 ? true : false;
        var direction = Game.PlayerFirst ? -1 : 1;
        if (IsAdd)
        {
            for(var i = 0; i < value; i++)
            {
                var TransZ = Game.YogaGirlList.Count * Size + Size;
                TransZ *= direction;
                var girl = girlList[i];
                girl.rotation = Quaternion.identity;
                girl.SetParent(GirlListTrans);
                GirlFollow target = null;
                if (Game.YogaGirlList.Count > 0)
                    target = Game.YogaGirlList.Last().transform.GetOrAddComponent<GirlFollow>();
                var spawnPos = Player.Render.RenderTrans.transform.localPosition;
                spawnPos.z += TransZ;
                var follow = girl.GetOrAddComponent<GirlFollow>();
                follow.IsStart = false;
                Game.YogaGirlList.Add(follow);
                //string yogaStr = Player.Control._yogaList[Player.Control._targetIndex];
                //follow.Animator.SetTrigger("Yoga15");
                //follow.CurrentClip = "Yoga15";
                follow.Animator.speed = 1;
                follow.Animator.Play("Run");
                //follow.Animator.transform.eulerAngles = new Vector3(0f, 180f, 0f);
                girl.localPosition = spawnPos;
                follow.Init(TransZ, target);
                follow.Animator.speed = 0;
                follow.Animator.transform.eulerAngles = new Vector3(0f, 90f, 0f);
                //UTween.Position(girl, girl.localPosition, spawnPos, 1f, SpaceMode.Local)
                //    .SetOnStop(() =>
                //    {
                //        follow.Init(TransZ, target);
                //        follow.Animator.speed = 0;
                //        follow.Animator.transform.eulerAngles = new Vector3(0f, 90f, 0f);
                //    });
            }
        }
        else
        {
            var nowPos = Vector3.zero;
            for (var i = 0; i > value; i--)
            {
                if (Game.YogaGirlList.Count <= 0)
                {
                    Player.Lose();
                    return;
                }
                var girl = Game.YogaGirlList.First();
                nowPos = girl.transform.position;
                Game.YogaGirlList.Remove(girl);
                girl.Animator.speed = 1;
                girl.Animator.Play("Fall");

                var Ins = girl.gameObject;
                this.ExecuteDelay(() =>
                {
                    Ins.transform.SetParent(Level.Level.transform);
                    Ins.transform.localPosition = new Vector3(1000f, 0, 0);
                    Destroy(Ins);
                }, 1f);
                //girl.IsDead = true;
                Game.YogaGirlList.Remove(girl);
                Player.Move.PathFollower.Distance -= Size;
                Player.Move.PathFollower.BlockDistance -= Size;
            }
            var index = 0;
            foreach(var girl in Game.YogaGirlList)
            {
                GirlFollow target = null;
                if (index > 0)
                    target = Game.YogaGirlList[index - 1];
                index++;
                girl.Init(index * Size * direction, target);
            }
            Player.transform.position = nowPos;
        }
    }

    public void RefreshRender(GameObject prefab)
    {
        DeSpawnRenderer();
        RenderInstance = GamePool.Spawn(prefab, RenderTrans);

        ComponentDic.ForEach(c => c.Value.CacheRendererComponent());
        Play(CurrentClip);
    }

    public void DeSpawnRenderer()
    {
        foreach(var girl in Game.YogaGirlList)
        {
            var follow = RenderInstance.GetComponent<PathFollowerGirl>();
            if (follow != null)
                Destroy(follow);
            GamePool.DeSpawn(girl.transform);
        }
        Game.YogaGirlList.Clear();
        GamePool.DeSpawn(Render.RendererTrans);
        RenderInstance = null;
    }
}
