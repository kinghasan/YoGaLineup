/////////////////////////////////////////////////////////////////////////////
//
//  Script   : ColliderFilter.cs
//  Info     : 碰撞过滤器 用于处理指定类型和层级的碰撞对象
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using System;
using UnityEngine;

namespace Aya.Physical
{
    public struct ColliderFilter<T>
    {
        public Type Type;
        public LayerMask? Layer;
        public Action<object> Callback;
        public ColliderEvent Event;
        internal int ActionHashCode;
    }
}