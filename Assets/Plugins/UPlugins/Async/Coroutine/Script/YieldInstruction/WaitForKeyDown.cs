using UnityEngine;

namespace Aya.Async
{
    public class WaitForKeyDown : CustomYieldInstruction
    {
        public KeyCode KeyCode;

        public override bool keepWaiting => !Input.GetKeyDown(KeyCode);

        public WaitForKeyDown(KeyCode keyCode)
        {
            KeyCode = keyCode;
        }
    }
}