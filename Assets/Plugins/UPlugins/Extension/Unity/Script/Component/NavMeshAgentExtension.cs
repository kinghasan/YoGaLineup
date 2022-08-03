/////////////////////////////////////////////////////////////////////////////
//
//  Script   : NavMeshAgentExtension.cs
//  Info     : NavMeshAgent扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine.AI;

namespace Aya.Extension
{
    public static class NavMeshAgentExtension
    {
        /// <summary>
        /// 是否到达
        /// </summary>
        /// <param name="agent">导航代理</param>
        /// <returns>结果</returns>
        public static bool IsArrive(this NavMeshAgent agent)
        {
            if (!agent.enabled) return false;
            return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;
        }
    }
}