using UnityEngine;

namespace Aya.Async
{
    public class WaitForButtonUp : CustomYieldInstruction
    {
        public string ButtonName;

        public override bool keepWaiting => !Input.GetButtonUp(ButtonName);

        public WaitForButtonUp(string buttonName)
        {
            ButtonName = buttonName;
        }
    }
}