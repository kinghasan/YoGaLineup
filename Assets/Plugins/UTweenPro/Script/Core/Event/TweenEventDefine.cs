using System;
using UnityEngine;
using UnityEngine.Events;

namespace Aya.TweenPro
{
    #region Tween Play State Event

    [Serializable]
    public class OnPlayEvent : TweenEvent
    {

    }

    [Serializable]
    public class OnStartEvent : TweenEvent
    {

    }

    [Serializable]
    public class OnLoopStartEvent : TweenEvent
    {

    }

    [Serializable]
    public class OnLoopEndEvent : TweenEvent
    {

    }

    [Serializable]
    public class OnUpdateEvent : TweenEvent
    {

    }

    [Serializable]
    public class OnDelayEvent : TweenEvent
    {

    }

    [Serializable]
    public class OnPauseEvent : TweenEvent
    {

    }

    [Serializable]
    public class OnResumeEvent : TweenEvent
    {

    }

    [Serializable]
    public class OnStopEvent : TweenEvent
    {

    }

    [Serializable]
    public class OnCompleteEvent : TweenEvent
    {

    }

    #endregion

    #region Unity Event Value

    [Serializable]
    public class OnValueBooleanEvent : UnityEvent<bool>
    {

    }

    [Serializable]
    public class OnValueFloatEvent : UnityEvent<float>
    {

    }

    [Serializable]
    public class OnValueDoubleEvent : UnityEvent<double>
    {

    }

    [Serializable]
    public class OnValueIntegerEvent : UnityEvent<int>
    {

    }

    [Serializable]
    public class OnValueLongEvent : UnityEvent<long>
    {

    }

    [Serializable]
    public class OnValueStringEvent : UnityEvent<string>
    {

    }

    [Serializable]
    public class OnValueVector2Event : UnityEvent<Vector2>
    {

    }


    [Serializable]
    public class OnValueVector3Event : UnityEvent<Vector3>
    {

    }


    [Serializable]
    public class OnValueVector4Event : UnityEvent<Vector4>
    {

    }

    [Serializable]
    public class OnValueColorEvent : UnityEvent<Color>
    {

    }

    [Serializable]
    public class OnValueRectEvent : UnityEvent<Rect>
    {

    }

    [Serializable]
    public class OnValueRectOffsetEvent : UnityEvent<RectOffset>
    {

    }

    [Serializable]
    public class OnValueQuaternionEvent : UnityEvent<Quaternion>
    {

    }

    #endregion
}