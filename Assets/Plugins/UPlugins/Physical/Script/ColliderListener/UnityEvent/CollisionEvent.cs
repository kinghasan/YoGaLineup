/////////////////////////////////////////////////////////////////////////////
//
//  Script   : CollisionEvent.cs
//  Info     : Åö×²ÊÂ¼þ
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Aya.Physical
{
    [Serializable]
    public class CollisionEvent : UnityEvent<Collision>
    {
    }
}