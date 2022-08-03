/////////////////////////////////////////////////////////////////////////////
//
//  Script   : LayerMaskExtension.cs
//  Info     : LayerMask扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////
using System.Collections.Generic;
using UnityEngine;

namespace Aya.Extension
{
    public static class LayerMaskExtension
    {
        /// <summary>
        /// 是否包含层级
        /// </summary>
        /// <param name="layerMask">层</param>
        /// <param name="otherLayerMask">其他层</param>
        /// <returns>结果</returns>
        public static bool Contains(this LayerMask layerMask, LayerMask otherLayerMask)
        {
            var value = 1 << layerMask.value;
            var result = (value & otherLayerMask.value) > 0;
            return result;
        }

        /// <summary>
        /// 是否包含某个层
        /// </summary>
        /// <param name="layerMask">层</param>
        /// <param name="layerIndex">其他层索引</param>
        /// <returns>结果</returns>
        public static bool Contains(this LayerMask layerMask, int layerIndex)
        {
            return (layerMask.value & (1 << layerIndex)) > 0;
        }

        /// <summary>
        /// 获取层索引号
        /// </summary>
        /// <param name="layerMask">层</param>
        /// <returns>索引</returns>
        public static int GetLayerIndex(this LayerMask layerMask)
        {
            var i = 0;
            while (layerMask.value >> i != 0x1)
            {
                i++;
                if (i <= 32) continue;
                Debug.LogError("Get LayerMask Index Error");
                return -1;
            }

            return i;
        }

        /// <summary>
        /// 反转
        /// </summary>
        /// <param name="layerMask">层</param>
        /// <returns>结果</returns>
        public static LayerMask Inverse(this LayerMask layerMask)
        {
            return ~layerMask;
        }

        /// <summary>
        /// 叠加层
        /// </summary>
        /// <param name="layerMask">层</param>
        /// <param name="name">名字</param>
        /// <returns>结果</returns>
        public static LayerMask AddToMask(this LayerMask layerMask, params string[] name)
        {
            return layerMask | NameToMask(name);
        }

        /// <summary>
        /// 移除层
        /// </summary>
        /// <param name="layerMask">层</param>
        /// <param name="name">名字</param>
        /// <returns>结果</returns>
        public static LayerMask RemoveFromMask(this LayerMask layerMask, params string[] name)
        {
            var invertedOriginal = ~layerMask;
            return ~(invertedOriginal | NameToMask(name));
        }

        /// <summary>
        /// 层转换为名字
        /// </summary>
        /// <param name="layerMask">层</param>
        /// <returns>结果</returns>
        public static string[] MaskToNames(this LayerMask layerMask)
        {
            var output = new List<string>();

            for (var i = 0; i < 32; ++i)
            {
                var shifted = 1 << i;
                if ((layerMask & shifted) != shifted) continue;
                var layerName = LayerMask.LayerToName(i);
                if (!string.IsNullOrEmpty(layerName))
                {
                    output.Add(layerName);
                }
            }

            return output.ToArray();
        }

        /// <summary>
        /// 层转换为字符串
        /// </summary>
        /// <param name="layerMask">层</param>
        /// <returns>结果</returns>
        public static string MaskToString(this LayerMask layerMask)
        {
            return MaskToString(layerMask, ", ");
        }

        /// <summary>
        /// 层转换为字符串
        /// </summary>
        /// <param name="layerMask">层</param>
        /// <param name="delimiter">分隔符</param>
        /// <returns>结果</returns>
        public static string MaskToString(this LayerMask layerMask, string delimiter)
        {
            return string.Join(delimiter, MaskToNames(layerMask));
        }

        /// <summary>
        /// 名字转换为 Layer
        /// </summary>
        /// <param name="layerNames">名字</param>
        /// <returns>Layer</returns>
        internal static LayerMask NameToMask(params string[] layerNames)
        {
            var ret = (LayerMask)0;
            for (var i = 0; i < layerNames.Length; i++)
            {
                var nameTemp = layerNames[i];
                ret |= (1 << LayerMask.NameToLayer(nameTemp));
            }

            return ret;
        }
    }
}