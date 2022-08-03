using System;
using UnityEngine;

namespace Aya.Async
{
    public class WaitForCondition : CustomYieldInstruction
    {
        public Func<bool> Condition;

        public override bool keepWaiting => !Condition();

        public WaitForCondition(Func<bool> condition)
        {
            Condition = condition;
        }
    }
}