using UnityEngine;

namespace Aya.Async
{
    public class WaitForButtonDown : CustomYieldInstruction
    {
        public string ButtonName;

        public override bool keepWaiting => !Input.GetButtonDown(ButtonName);

        public WaitForButtonDown(string buttonName)
        {
            ButtonName = buttonName;
        }
    }
}
