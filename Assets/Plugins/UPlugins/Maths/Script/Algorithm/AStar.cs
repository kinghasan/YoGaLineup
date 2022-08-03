/////////////////////////////////////////////////////////////////////////////
//
//  Script   : AStar.cs
//  Info     : A* 寻路算法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2018
//
/////////////////////////////////////////////////////////////////////////////
using System.Collections.Generic;

namespace Aya.Maths
{
    public enum AStarItemType
    {
        Start,      // 起点
        End,        // 终点
        Normal,     // 正常
        Obstacle,   // 障碍物  
    }

    public interface IAStartItem
    {
        // start -> end 的消耗
        // F = G + H
        int F { get; set; }
        // start -> n 的消耗
        int G { get; set; }
        // n -> end 的消耗
        int H { get; set; }

        // 通过计数
        int PassCount { get; set; }
        // 通过最大次数
        int PassLimit { get; set; }

        // 根据起点计算 G 值
        int CalcG(IAStartItem start);
        // 根据终点计算 H 值
        int CalcH(IAStartItem end);
        // 计算 F = G + H
        int CalcF();

        // 获取对应 AStar 算法的节点类型
        AStarItemType GetAStarItemType();
        // 父节点
        IAStartItem Parent { get; set; }

        // 可到达的邻居节点，可以依据上一个经过节点做出特殊处理
        IList<IAStartItem> GetNeighbors(IAStartItem preview);

        /*
        
        关于 G / H / F 的一种参考实现

        public int CalcG(IAStartItem start)
        {
            var point = this;
            var startPoint = start as CellItem;
            var g = (Mathf.Abs(point.X - startPoint.X) + Mathf.Abs(point.Y - startPoint.Y));
            var parentG = point.Parent != null ? point.Parent.G : 0;
            return g + parentG;
        }

        public int CalcH(IAStartItem end)
        {
            var point = this;
            var endPoint = end as CellItem;
            var step = Mathf.Abs(point.X - endPoint.X) + Mathf.Abs(point.Y - endPoint.Y);
            return step;
        }

        public int CalcF()
        {
            return G + H;
        }
        */
    }

    public static class AStar
    {
        // 地图节点数组
        public static List<IAStartItem> Map;
        // 开启列表
        public static List<IAStartItem> OpenList;
        // 结束列表
        public static List<IAStartItem> CloseList;

        // 起点
        public static IAStartItem Start;
        // 终点
        public static IAStartItem End;

        public static IList<IAStartItem> Search(List<IAStartItem> map)
        {
            var start = map.Find(item => item.GetAStarItemType() == AStarItemType.Start);
            var end = map.Find(item => item.GetAStarItemType() == AStarItemType.End);
            return Search(map, start, end);
        }

        public static IList<IAStartItem> Search(List<IAStartItem> map, IAStartItem start, IAStartItem end)
        {
            Map = map;
            Start = start;
            End = end;
            OpenList = new List<IAStartItem>();
            CloseList = new List<IAStartItem>();

            // 初始化寻路基础数据
            for (var i = 0; i < map.Count; i++)
            {
                var item = map[i];
                item.Parent = null;
                item.G = 0;
                item.H = 0;
                item.F = 0;
                item.PassCount = 0;
            }

            var endItem = SearchPath();
            if (endItem == null)
            {
                // 没找到
                return null;
            }
            else
            {
                var retList = new List<IAStartItem>();
                var item = End;
                while (item.Parent != null)
                {
                    retList.Insert(0, item);
                    item = item.Parent;
                }
                retList.Insert(0, Start);
                return retList;
            }
        }

        private static IAStartItem SearchPath()
        {
            var index = -1;
            IAStartItem previewItem = null;
            // 将 起点 加入开放列表
            OpenList.Add(Start);
            while (OpenList.Count != 0)
            {
                // 取出开放列表中 F 最小的点，并加入关闭列表
                OpenList.Sort((item1, item2) => item1.F - item2.F);
                var current = OpenList[0];
                current.PassCount++;
                if (current.PassCount >= current.PassLimit)
                {
                    OpenList.Remove(current);
                }
                if (!CloseList.Contains(current))
                {
                    CloseList.Add(current);
                }
                // 找出相邻节点
                var neighbors = current.GetNeighbors(previewItem);

                for (var i = 0; i < neighbors.Count; i++)
                {
                    var neighor = neighbors[i];
                    if (neighor.GetAStarItemType() == AStarItemType.Obstacle)
                    {
                        continue;
                    }
                    if (CloseList.Contains(neighor) && neighor.PassCount >= neighor.PassLimit)
                    {
                        continue;
                    }
                    if (OpenList.Contains(neighor))
                    {
                        // 如果存在于开放列表，则计算 G，如果大于原值，不作处理，否则设置当前点为父节点
                        var g = neighor.CalcG(Start);
                        if (g < neighor.G)
                        {
                            neighor.Parent = current;
                            neighor.G = g;
                            neighor.F = neighor.CalcF();
                        }
                    }
                    else
                    {
                        // 如果不在开放列表，则加入
                        OpenList.Add(neighor);
                        // 并设当前点为父节点，计算 G,H,F
                        neighor.Parent = current;
                        neighor.G = neighor.CalcG(Start);
                        neighor.H = neighor.CalcH(End);
                        neighor.F = neighor.CalcF();
                        previewItem = neighor;
                    }
                }
                index = OpenList.IndexOf(End);
                if (index >= 0)
                {
                    return End;
                }
            }
            index = OpenList.IndexOf(End);
            return index >= 0 ? End : null;
        }
    }
}
