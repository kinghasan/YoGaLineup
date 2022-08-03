/////////////////////////////////////////////////////////////////////////////
//
//  Script   : IColliderListener.cs
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
    public interface IColliderListener
    {
        void OnTriggerEnter(Collider other);
        void OnTriggerStay(Collider other);
        void OnTriggerExit(Collider other);
    }
}