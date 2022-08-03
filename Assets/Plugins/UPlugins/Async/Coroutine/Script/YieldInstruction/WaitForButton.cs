using UnityEngine;

namespace Aya.Async
{
    public class WaitForButton : CustomYieldInstruction
    {
        public string ButtonName;

        public override bool keepWaiting => !Input.GetButton(ButtonName);

        public WaitForButton(string buttonName)
        {
            ButtonName = buttonName;
        }
    }
}
