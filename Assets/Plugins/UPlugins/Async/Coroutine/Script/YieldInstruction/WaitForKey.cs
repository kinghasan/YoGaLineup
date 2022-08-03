using UnityEngine;

namespace Aya.Async
{
    public class WaitForKey : CustomYieldInstruction
    {
        public KeyCode KeyCode;

        public override bool keepWaiting => !Input.GetKey(KeyCode);

        public WaitForKey(KeyCode keyCode)
        {
            KeyCode = keyCode;
        }
    }
}
