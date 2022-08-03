using UnityEngine;

namespace Aya.Async
{
    public class WaitForMouseButtonDown : CustomYieldInstruction
    {
        public int Button = 0;

        public override bool keepWaiting => !Input.GetMouseButtonDown(Button);

        public WaitForMouseButtonDown(int button = 0)
        {
            Button = button;
        }
    }
}
