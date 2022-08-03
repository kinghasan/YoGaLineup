using System.Collections;
using System.Collections.Generic;
using Aya.TweenPro;
using UnityEngine;
using PlayMode = Aya.TweenPro.PlayMode;

namespace Aya.Samples
{
    public class UTweenSamplesStagger : MonoBehaviour
    {
        public List<Transform> Transforms;
        public float Duration;
        public float Offset;
        public AnimationCurve Curve;

        public void Start()
        {
            UTween.CreateTweenData()
                .Append(UTween.Position(transform, Vector3.one, 1f))
                .Append(UTween.Position(transform, Vector3.zero, 1f));

            UTween.Stagger<TweenPosition, Transform, Vector3>(Transforms, Vector3.zero, Vector3.one, Duration, Offset, (index, tweener) =>
            {
                tweener.SetCurve(Curve);
            }).SetLoopUnlimited();

            UTween.Stagger<TweenScale, Transform, Vector3>(Transforms, Vector3.one, Vector3.zero, Duration, Offset * 0.25f, (index, tweener) =>
            {
                tweener.SetCurve(Curve);
            }).SetLoopUnlimited();

            UTween.Value(()=> 0, () => 100, 100f, value => Debug.Log(value));
        }
    }
}
