using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Serializable]
    public abstract partial class Tweener<TTarget, TValue> : Tweener<TTarget>
        where TTarget : UnityEngine.Object
    {
        public TValue From;
        public TValue To;
        public AxisConstraint Axis;
        public bool EnableAxis = false;

        [NonSerialized] public Func<TValue> FromGetter;
        [NonSerialized] public Func<TValue> ToGetter;
        [NonSerialized] public Func<TValue> ValueGetter;
        [NonSerialized] public Action<TValue> ValueSetter;

        public Action<TValue> OnUpdate;

        #region Axis Constraint

        public bool AxisX
        {
            get => !EnableAxis || (Axis & AxisConstraint.X) > 0;
            set
            {
                if (value)
                {
                    Axis |= AxisConstraint.X;
                }
                else
                {
                    Axis &= ~AxisConstraint.X;
                    EnableAxis = true;
                }
            }
        }

        public bool AxisY
        {
            get => !EnableAxis || (Axis & AxisConstraint.Y) > 0;
            set
            {
                if (value)
                {
                    Axis |= AxisConstraint.Y;
                }
                else
                {
                    Axis &= ~AxisConstraint.Y;
                    EnableAxis = true;
                }
            }
        }

        public bool AxisZ
        {
            get => !EnableAxis || (Axis & AxisConstraint.Z) > 0;
            set
            {
                if (value)
                {
                    Axis |= AxisConstraint.Z;
                }
                else
                {
                    Axis &= ~AxisConstraint.Z;
                    EnableAxis = true;
                }
            }
        }

        public bool AxisW
        {
            get => !EnableAxis || (Axis & AxisConstraint.W) > 0;
            set
            {
                if (value)
                {
                    Axis |= AxisConstraint.W;
                }
                else
                {
                    Axis &= ~AxisConstraint.W;
                    EnableAxis = true;
                }
            }
        }

        #endregion

        public abstract TValue Value { get; set; }

        public virtual TValue RecordValue { get; set; }

        public override void PreSample()
        {
            base.PreSample();
            RefreshGetterSetter();
        }

        public override void RecordObject()
        {
            RecordValue = Value;
        }

        public override void RestoreObject()
        {
            Value = RecordValue;
        }

        public virtual void DisableIndependentAxis()
        {
            Axis = AxisConstraint.All;
            EnableAxis = false;
        }

        public override void Reset()
        {
            base.Reset();
            From = default(TValue);
            To = default(TValue);
            ResetGetterSetter();
            DisableIndependentAxis();
        }

        public virtual void RefreshGetterSetter()
        {
            if (FromGetter == null) FromGetter = () => From;
            if (ToGetter == null) ToGetter = () => To;
            if (ValueGetter == null) ValueGetter = () => Value;
            if (ValueSetter == null) ValueSetter = value => Value = value;
        }

        public virtual void ResetGetterSetter()
        {
            FromGetter = () => From;
            ToGetter = () => To;
            ValueGetter = () => Value;
            ValueSetter = value => Value = value;
        }

        public override void ReverseFromTo()
        {
            var temp = From;
            From = To;
            To = temp;
        }

        public override void ResetCallback()
        {
            OnUpdate = null;
        }
    }
}