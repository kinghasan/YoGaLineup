using System;
using Object = UnityEngine.Object;

namespace Aya.TweenPro
{
    public static partial class TweenerExtension
    {

    }

    public abstract partial class Tweener<TTarget, TValue> : Tweener<TTarget>
        where TTarget : Object
    {
        #region Set From / To
       
        public Tweener<TTarget, TValue> SetFrom(TValue from)
        {
            From = from;
            return this;
        }

        public Tweener<TTarget, TValue> SetTo(TValue to)
        {
            To = to;
            return this;
        }

        #endregion

        #region Set FromGetter / ToGetter / ValueGetter / ValueSetter

        public Tweener<TTarget, TValue> SetFromGetter(Func<TValue> fromGetter)
        {
            FromGetter = fromGetter;
            return this;
        }

        public Tweener<TTarget, TValue> SetToGetter(Func<TValue> toGetter)
        {
            ToGetter = toGetter;
            return this;
        }

        public Tweener<TTarget, TValue> SetValueGetter(Func<TValue> valueGetter)
        {
            ValueGetter = valueGetter;
            return this;
        }

        public Tweener<TTarget, TValue> SetValueSetter(Action<TValue> valueSetter)
        {
            ValueSetter = valueSetter;
            return this;
        }

        #endregion

        #region Set Current <-> Value

        public Tweener<TTarget, TValue> SetCurrent2From()
        {
            From = Value;
            return this;
        }

        public Tweener<TTarget, TValue> SetFrom2Current()
        {
            Value = From;
            return this;
        }

        public Tweener<TTarget, TValue> SetCurrent2To()
        {
            To = Value;
            return this;
        }

        public Tweener<TTarget, TValue> SetTo2Current()
        {
            Value = To;
            return this;
        } 

        #endregion

        #region Set Axis

        public Tweener<TTarget, TValue> SetAxisX(bool enable)
        {
            AxisX = enable;
            return this;
        }

        public Tweener<TTarget, TValue> SetAxisY(bool enable)
        {
            AxisY = enable;
            return this;
        }

        public Tweener<TTarget, TValue> SetAxisZ(bool enable)
        {
            AxisZ = enable;
            return this;
        }

        public Tweener<TTarget, TValue> SetAxisW(bool enable)
        {
            AxisW = enable;
            return this;
        }

        public Tweener<TTarget, TValue> SetAxis(bool enable)
        {
            AxisX = enable;
            AxisY = enable;
            AxisZ = enable;
            AxisW = enable;
            return this;
        }

        public Tweener<TTarget, TValue> SetAxis(AxisConstraint axis)
        {
            Axis = axis;
            EnableAxis = true;
            return this;
        }

        #endregion

        #region Set Event

        public Tweener<TTarget, TValue> SetOnUpdate(Action<TValue> onUpdate)
        {
            OnUpdate += onUpdate;
            return this;
        }

        #endregion
    }
}