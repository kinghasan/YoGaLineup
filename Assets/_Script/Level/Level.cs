using System;
using System.Collections.Generic;
using UnityEngine;
using Aya.Extension;
using Sirenix.OdinInspector;

public class Level : GameEntity
{
    public int PlayerCount = 1;
    public Transform BackGround;
    public List<LevelBlock> BlockList;
    public ItemEndless Rainbow;
    public List<ItemBase> ItemList { get; set; }
    public Dictionary<Type, List<ItemBase>> ItemDic { get; set; }

    public List<LevelBlock> BlockInsList { get; set; } = new List<LevelBlock>();

    protected override void Awake()
    {
        base.Awake();
        if (BackGround != null)
            Instantiate(BackGround, new Vector3(0, -3, -100), Quaternion.identity);
    }

    public void Init()
    {
        InitBlocks();
        InitItem();
        InitPlayer();
        SDKUtil.ClikLevelStart();
    }

    #region Item

    public int RainbowIndex { get; set; }
    public void InitItem()
    {
        RainbowIndex = 0;
        ItemList = transform.GetComponentsInChildren<ItemBase>(true).ToList();
        foreach (var item in ItemList)
        {
            item.Init();
        }

        ItemList = transform.GetComponentsInChildren<ItemBase>(true).ToList();

        ItemDic = new Dictionary<Type, List<ItemBase>>();
        foreach (var item in ItemList)
        {
            var itemType = item.GetType();
            if (!ItemDic.TryGetValue(itemType, out var itemList))
            {
                itemList = new List<ItemBase>();
                ItemDic.Add(itemType, itemList);
            }

            itemList.Add(item);
        }
    }

    public void InitItemsRenderer()
    {
        ItemList.ForEach(item => item.InitRenderer());
    }

    public List<T> GetItems<T>() where T : ItemBase
    {
        if (!ItemDic.TryGetValue(typeof(T), out var list))
        {
            list = new List<ItemBase>();
            ItemDic.Add(typeof(T), list);
        }

        return list.ToList(i => i as T);
    }

    public T GetItem<T>() where T : ItemBase
    {
        var list = GetItems<T>();
        return list.First();
    }

    public void RemoveItem(ItemBase item)
    {
        var type = item.GetType();
        ItemList.Remove(item);
        if (ItemDic.TryGetValue(type, out var list))
        {
            list.Remove(item);
        }
    } 

    #endregion

    /// <summary>
    /// 跟随少女使用
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public Vector3 GetPositionY(float length)
    {
        var blockLength = 0f;
        foreach(var block in BlockInsList)
        {
            if (blockLength + block.Length >= length)
            {
                length -= blockLength;
                //var path = block.PathList[0] as LevelPathSpline;
                //var factor = (float)path.Path.Travel(0f, length);
                var bo = false;
                var pos = Vector3.zero;
                var fl = 0f;
                (bo, pos, fl) = block.Path.GetPositionByDistance(length);
                //var resultY = pos.y - Player.transform.localPosition.y;
                pos.y -= Player.transform.localPosition.y;
                return pos;
            }
            else
                blockLength += block.Length;
        }
        return Vector3.zero;
    }

    public void InitPlayer()
    {
        Game.PlayerList.Clear();
        for (var i = 0; i < PlayerCount; i++)
        {
            var playerIns = GamePool.Spawn(Game.PlayerPrefab, CurrentLevel.Trans);
            playerIns.State.Index = i;
            playerIns.State.IsPlayer = false;
            Game.PlayerList.Add(playerIns);
        }

        var player = Game.PlayerList.Random();
        player.State.IsPlayer = true;
        Game.Player = player;

        foreach (var playerTemp in Game.PlayerList)
        {
            playerTemp.Init();
        }
    }

    [ButtonGroup("Test"), Button("Test Create")]
    public void InitBlocks()
    {
        DeSpawnLevelBlocks();

        var currentPos = Vector3.zero;
        var currentForward = Vector3.forward;
        foreach (var levelBlockPrefab in BlockList)
        {
            var blockIns = Application.isPlaying ? GamePool.Spawn(levelBlockPrefab, transform) : Instantiate(levelBlockPrefab, transform);
            blockIns.transform.position = currentPos;
            blockIns.transform.forward = currentForward;
            blockIns.Init();

            currentPos = blockIns.EndPosition;
            currentForward = blockIns.EndForward;

            BlockInsList.Add(blockIns);
        }
    }

    [ButtonGroup("Test"), Button("Destroy"), GUIColor(1f, 0.5f, 0.5f)]
    public void DeSpawnLevelBlocks()
    {
        if (!Application.isPlaying)
        {
            BlockInsList = GetComponentsInChildren<LevelBlock>().ToList();
        }

        foreach (var levelBlock in BlockInsList)
        {
            if (Application.isPlaying)
            {
                GamePool.DeSpawn(levelBlock);
            }
            else
            {
                DestroyImmediate(levelBlock.gameObject);
            }
        }

        BlockInsList.Clear();
    }
}
