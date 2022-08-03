/////////////////////////////////////////////////////////////////////////////
//
//  Script   : ICollisionListener2D.cs
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
    public interface ICollisionListener2D
    {
        void OnCollisionEnter2D(Collision2D other);
        void OnCollisionStay2D(Collision2D other);
        void OnCollisionExit2D(Collision2D other);
    }
}