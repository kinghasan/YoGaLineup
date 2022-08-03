/////////////////////////////////////////////////////////////////////////////
//
//  Script   : LayerUtil.cs
//  Info     : Layer 操作辅助类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.Util
{
    /// <summary>
    /// Layer 操作辅助
    /// </summary>
    public static class LayerUtil
    {
        /// <summary>
        /// 创建 Layer
        /// </summary>
        /// <param name="layerNames">名字</param>
        /// <returns>Layer</returns>
        public static LayerMask Create(params string[] layerNames)
        {
            return NameToMask(layerNames);
        }

        /// <summary>
        /// 创建 Layer
        /// </summary>
        /// <param name="layerIndexs">索引</param>
        /// <returns>Layer</returns>
        public static LayerMask Create(params int[] layerIndexs)
        {
            return GetLayerMaskByIndex(layerIndexs);
        }

        /// <summary>
        /// 根据索引号获取 Layer
        /// </summary>
        /// <param name="layerIndexs">索引</param>
        /// <returns>Layer</returns>
        public static LayerMask GetLayerMaskByIndex(params int[] layerIndexs)
        {
            var ret = (LayerMask) 0;
            for (var i = 0; i < layerIndexs.Length; i++)
            {
                var layer = layerIndexs[i];
                ret |= (1 << layer);
            }

            return ret;
        }

        /// <summary>
        /// 名字转换为 Layer
        /// </summary>
        /// <param name="layerNames">名字</param>
        /// <returns>Layer</returns>
        public static LayerMask NameToMask(params string[] layerNames)
        {
            var ret = (LayerMask) 0;
            for (var i = 0; i < layerNames.Length; i++)
            {
                var nameTemp = layerNames[i];
                ret |= (1 << LayerMask.NameToLayer(nameTemp));
            }

            return ret;
        }
    }
}