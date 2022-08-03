/////////////////////////////////////////////////////////////////////////////
//
//  Script : WaitForSecondsUserTime.cs
//  Info   : 提供任意时间缩放下的协程等待
//  Author : ls9512
//  E-mail : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using System;
using UnityEngine;

namespace Aya.Async
{
    public class WaitForSecondsUserTime : CustomYieldInstruction
    {
        public float WaitTime { get; set; }
        public Func<float> DeltaTimeGetter { get; set; }

        private float _timer;

        public WaitForSecondsUserTime(float time)
        {
            WaitTime = time;
            DeltaTimeGetter = () => Time.deltaTime;
            _timer = 0f;
        }

        public WaitForSecondsUserTime(float time, Func<float> deltaTimeGetter)
        {
            WaitTime = time;
            DeltaTimeGetter = deltaTimeGetter;
            _timer = 0f;
        }

        public override bool keepWaiting
        {
            get
            {
                if (_timer <= WaitTime)
                {
                    _timer += DeltaTimeGetter();
                }

                var result = _timer <= WaitTime;
                return result;
            }
        }
    }
}
