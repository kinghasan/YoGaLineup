using UnityEngine;

namespace Aya.Async
{
    public class WaitForMouseButton : CustomYieldInstruction
    {
        public int Button = 0;

        public override bool keepWaiting => !Input.GetMouseButton(Button);

        public WaitForMouseButton(int button = 0)
        {
            Button = button;
        }
    }
}
