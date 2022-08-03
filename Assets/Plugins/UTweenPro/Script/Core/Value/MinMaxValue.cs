using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.TweenPro
{
    public enum MinMaxValueMode
    {
        Value = 0,
        Random = 1,
    }

    public interface IMinMaxValue<out T>
    {
        T GetValue();
        void Reset();
    }

    public struct MinMaxFloat : IMinMaxValue<float>
    {
        public MinMaxValueMode Mode;
        public float Value;
        public float? RandomValue;
        public float Min;
        public float Max;

        public float GetValue()
        {
            if (Mode == MinMaxValueMode.Value) return Value;
            if (Mode == MinMaxValueMode.Random)
            {
                if (RandomValue == null) RandomValue = Random.Range(Min, Max);
                return RandomValue.Value;
            }
            return default;
        }

        public void Reset()
        {
            Mode = MinMaxValueMode.Value;
            Value = 0;
            RandomValue = null;
            Min = 0;
            Max = 0;
        }
    }
}