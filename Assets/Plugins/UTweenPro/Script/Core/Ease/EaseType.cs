using System;
using System.Collections.Generic;

namespace Aya.TweenPro
{
    public static partial class EaseType
    {
        [EaseFunction(typeof(EaseCustom), "Custom", "Custom")] 
        public const int Custom = -1;
        [EaseFunction(typeof(EaseLinear), "Linear", "Linear")]
        public const int Linear = 0;
        [EaseFunction(typeof(EaseCircular), "Circular", "Circular")]
        public const int Circular = 1;
        [EaseFunction(typeof(EaseInQuad), "In Quad", "Quad/In Quad")]
        public const int InQuad = 2;
        [EaseFunction(typeof(EaseOutQuad), "Out Quad", "Quad/Out Quad")]
        public const int OutQuad = 3;
        [EaseFunction(typeof(EaseInOutQuad), "In Out Quad", "Quad/In Out Quad")]
        public const int InOutQuad = 4;
        [EaseFunction(typeof(EaseInCubic), "In Cubic", "Cubic/In Cubic")]
        public const int InCubic = 5;
        [EaseFunction(typeof(EaseOutCubic), "Out Cubic", "Cubic/Out Cubic")]
        public const int OutCubic = 6;
        [EaseFunction(typeof(EaseInOutCubic), "In Out Cubic", "Cubic/In Out Cubic")]
        public const int InOutCubic = 7;
        [EaseFunction(typeof(EaseInQuart), "In Quart", "Quart/In Quart")]
        public const int InQuart = 8;
        [EaseFunction(typeof(EaseOutQuart), "Out Quart", "Quart/Out Quart")]
        public const int OutQuart = 9;
        [EaseFunction(typeof(EaseInOutQuart), "In Out Quart", "Quart/In Out Quart")]
        public const int InOutQuart = 10;
        [EaseFunction(typeof(EaseInQuint), "In Quint", "Quint/In Quint")]
        public const int InQuint = 11;
        [EaseFunction(typeof(EaseOutQuint), "Out Quint", "Quint/Out Quint")]
        public const int OutQuint = 12;
        [EaseFunction(typeof(EaseInOutQuint), "In Out Quint", "Quint/In Out Quint")]
        public const int InOutQuint = 13;
        [EaseFunction(typeof(EaseInSine), "In Sine", "Sine/In Sine")]
        public const int InSine = 14;
        [EaseFunction(typeof(EaseOutSine), "Out Sine", "Sine/Out Sine")]
        public const int OutSine = 15;
        [EaseFunction(typeof(EaseInOutSine), "In Out Sine", "Sine/In Out Sine")]
        public const int InOutSine = 16;
        [EaseFunction(typeof(EaseInExpo), "In Expo", "Expo/In Expo")]
        public const int InExpo = 17;
        [EaseFunction(typeof(EaseOutExpo), "Out Expo", "Expo/Out Expo")]
        public const int OutExpo = 18;
        [EaseFunction(typeof(EaseInOutExpo), "In Out Expo", "Expo/In Out Expo")]
        public const int InOutExpo = 19;
        [EaseFunction(typeof(EaseInCirc), "In Circ", "Circ/In Circ")]
        public const int InCirc = 20;
        [EaseFunction(typeof(EaseOutCirc), "Out Circ", "Circ/Out Circ")]
        public const int OutCirc = 21;
        [EaseFunction(typeof(EaseInOutCirc), "In Out Circ", "Circ/In Out Circ")]
        public const int InOutCirc = 22;
        [EaseFunction(typeof(EaseSpring), "Spring", "Spring")]
        public const int Spring = 23;
        [EaseFunction(typeof(EaseInBack), "In Back", "Back/In Back")]
        public const int InBack = 24;
        [EaseFunction(typeof(EaseOutBack), "Out Back", "Back/Out Back")]
        public const int OutBack = 25;
        [EaseFunction(typeof(EaseInOutBack), "In Out Back", "Back/In Out Back")]
        public const int InOutBack = 26;
        [EaseFunction(typeof(EasePunch), "Punch", "Punch")]
        public const int Punch = 27;
        [EaseFunction(typeof(EaseInBounce), "In Bounce", "Bounce/In Bounce")]
        public const int InBounce = 28;
        [EaseFunction(typeof(EaseOutBounce), "Out Bounce", "Bounce/Out Bounce")]
        public const int OutBounce = 29;
        [EaseFunction(typeof(EaseInOutBounce), "In Out Bounce", "Bounce/In Out Bounce")]
        public const int InOutBounce = 30;
        [EaseFunction(typeof(EaseInElastic), "In Elastic", "Elastic/In Elastic")]
        public const int InElastic = 31;
        [EaseFunction(typeof(EaseOutElastic), "Out Elastic", "Elastic/Out Elastic")]
        public const int OutElastic = 32;
        [EaseFunction(typeof(EaseInOutElastic), "In Out Elastic", "Elastic/In Out Elastic")]
        public const int InOutElastic = 33;

        [EaseFunction(typeof(EaseSin), "Sin", "Misc/Sin")]
        public const int Sin = 101;
        [EaseFunction(typeof(EaseCos), "Cos", "Misc/Cos")]
        public const int Cos = 102;
        [EaseFunction(typeof(EaseFlash), "Flash", "Misc/Flash")]
        public const int Flash = 103;
        [EaseFunction(typeof(EaseStep), "Step", "Misc/Step")]
        public const int Step = 104;
        [EaseFunction(typeof(EaseParabola), "Parabola", "Misc/Parabola")]
        public const int Parabola = 105;

        public static readonly Dictionary<int, EaseFunction> FunctionDic = new Dictionary<int, EaseFunction>();
        public static readonly Dictionary<int, EaseFunctionAttribute> FunctionInfoDic = new Dictionary<int, EaseFunctionAttribute>();

        static EaseType()
        {
            Cache(typeof(EaseType));
        }

        public static void Cache(Type type)
        {
            var fieldInfos = type.GetFields();
            foreach (var fieldInfo in fieldInfos)
            {
                var attributes = fieldInfo.GetCustomAttributes(true);
                foreach (var attribute in attributes)
                {
                    if (!(attribute is EaseFunctionAttribute easeFunctionAttribute)) continue;
                    var index = (int) fieldInfo.GetValue(null);
                    var function = (EaseFunction)Activator.CreateInstance(easeFunctionAttribute.Type);
                    FunctionDic.Add(index, function);
                    FunctionInfoDic.Add(index, easeFunctionAttribute);
                }
            }
        }
    }
}
