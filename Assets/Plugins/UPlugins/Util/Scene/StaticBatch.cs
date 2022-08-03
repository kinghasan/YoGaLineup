/////////////////////////////////////////////////////////////////////////////
//
//  Script   : StaticBatch.cs
//  Info     : 静态批处理 - 挂在在场景根节点，运行时自动Batch static，减少Draw call 同时减少包体积。
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.Util
{
    public class StaticBatch : MonoBehaviour
    {
        private void Start() { StaticBatchingUtility.Combine(gameObject); }
    }
}