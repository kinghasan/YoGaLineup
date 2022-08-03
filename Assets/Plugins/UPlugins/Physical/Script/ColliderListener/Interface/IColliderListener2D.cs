/////////////////////////////////////////////////////////////////////////////
//
//  Script   : IColliderListener2D.cs
//  Info     : 碰撞接口
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.Physical
{
    public interface IColliderListener2D
    {
        void OnTriggerEnter2D(Collider2D other);
        void OnTriggerStay2D(Collider2D other);
        void OnTriggerExit2D(Collider2D other);
    }
}