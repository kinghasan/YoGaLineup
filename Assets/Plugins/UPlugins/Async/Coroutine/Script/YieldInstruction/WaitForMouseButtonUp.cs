using UnityEngine;

namespace Aya.Async
{
    public class WaitForMouseButtonUp : CustomYieldInstruction
    {
        public int Button = 0;

        public override bool keepWaiting => !Input.GetMouseButtonUp(Button);

        public WaitForMouseButtonUp(int button = 0)
        {
            Button = button;
        }
    }
}
