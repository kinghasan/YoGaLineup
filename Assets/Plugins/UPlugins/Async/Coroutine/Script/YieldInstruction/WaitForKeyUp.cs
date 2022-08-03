using UnityEngine;

namespace Aya.Async
{
    public class WaitForKeyUp : CustomYieldInstruction
    {
        public KeyCode KeyCode;

        public override bool keepWaiting => !Input.GetKeyUp(KeyCode);

        public WaitForKeyUp(KeyCode keyCode)
        {
            KeyCode = keyCode;
        }
    }
}