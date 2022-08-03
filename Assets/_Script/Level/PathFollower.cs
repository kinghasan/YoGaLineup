using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : GameEntity
{
    public Player Owner { get; set; }
    public float Length { get; set; }
    public int BlockIndex { get; set; }

    public LevelBlock CurrentBlock => CurrentLevel.BlockInsList[BlockIndex];
    public LevelPath CurrentPath => CurrentBlock.PathList[BlockPathIndex];

    public bool Finish { get; set; }

    public float Distance { get; set; }
    public float Factor { get; set; }

    public List<int> BlockPathIndexes { get; set; }
    public int BlockPathIndex => BlockPathIndexes[BlockIndex];
    public float BlockDistance { get; set; }
    public float BlockFactor { get; set; }

    public void Init(Player player)
    {
        Owner = player;
        Finish = false;
        Distance = 0f;
        Factor = 0f;
        Length = 0f;
        BlockPathIndexes = new List<int>();
        foreach (var levelBlock in CurrentLevel.BlockList)
        {
            Length += levelBlock.Length;
            BlockPathIndexes.Add(0);
        }

        RefreshPathInfo();
        EnterBlock(0);
    }

    public void SwitchPath(int blockIndex, int pathIndex)
    {
        if (BlockPathIndexes[blockIndex] == pathIndex) return;
        BlockPathIndexes[blockIndex] = pathIndex;
        if (blockIndex == BlockIndex)
        {
            Owner.State.TurnRange = CurrentPath.TurnRange;
        }
        
        RefreshPathInfo();
    }

    public void RefreshPathInfo()
    {
        Length = 0f;
        for (var i = 0; i < Level.CurrentLevel.BlockInsList.Count; i++)
        {
            var block = Level.CurrentLevel.BlockInsList[i];
            var pathIndex = BlockPathIndexes[i];
            var path = block.PathList[pathIndex];
            Length += path.Length;
        }
    }

    public void RefreshFactor()
    {
        Factor = Distance / Length;
        BlockFactor = BlockDistance / CurrentPath.Length;
    }

    public Vector3 Move(float distance)
    {
        if (Finish) return CurrentPath.GetPositionByFactor(1f);
        Vector3 result;

        while (true)
        {
            var finish = false;
            var overDistance = 0f;
            (finish, result, overDistance) = CurrentPath.GetPositionByDistance(BlockDistance + distance);
            if (finish)
            {
                Distance += distance - overDistance;
                BlockDistance += distance - overDistance;
            }
            else
            {
                Distance += distance;
                BlockDistance += distance;
            }

            if (finish)
            {
                var enterResult = EnterBlock(BlockIndex + 1, overDistance);
                if (!enterResult)
                {
                    Finish = true;
                    break;
                }

                distance = 0f;
            }
            else
            {
                break;
            }
        }

        RefreshFactor();
        return result;
    }

    public bool EnterBlock(int index, float initDistance = 0f)
    {
        if (index >= CurrentLevel.BlockInsList.Count)
        {
            Finish = true;
            return false;
        }

        BlockIndex = index;
        EnterPath(initDistance);
        if (!Owner.State.LimitTurnRange)
        {
            Owner.State.TurnRange = CurrentPath.TurnRange;
        }
        
        return true;
    }

    public virtual void EnterPath(float initMoveDistance = 0f)
    {
        BlockDistance = 0f;
        BlockFactor = 0f;
        Move(initMoveDistance);
    }
}
