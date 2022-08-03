using System;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.TweenPro
{
    public static partial class MonoBehaviourExtension
    {
        #region TweenAnimation
        
        public static UTweenAnimation GetTweenAnimation(this MonoBehaviour monoBehaviour)
        {
            var tweenAnimation = monoBehaviour.GetComponent<UTweenAnimation>();
            return tweenAnimation;
        }

        public static UTweenAnimation GetTweenAnimationInChildren(this MonoBehaviour monoBehaviour)
        {
            var tweenAnimation = monoBehaviour.GetComponentInChildren<UTweenAnimation>();
            return tweenAnimation;
        }

        public static UTweenAnimation GetTweenAnimation(this MonoBehaviour monoBehaviour, string identifier)
        {
            var tweenAnimations = monoBehaviour.GetComponents<UTweenAnimation>();
            foreach (var tweenAnimation in tweenAnimations)
            {
                if (tweenAnimation.Data.Identifier == identifier) return tweenAnimation;
            }

            return default;
        }

        public static UTweenAnimation GetTweenAnimationInChildren(this MonoBehaviour monoBehaviour, string identifier)
        {
            var tweenAnimations = monoBehaviour.GetComponentsInChildren<UTweenAnimation>();
            foreach (var tweenAnimation in tweenAnimations)
            {
                if (tweenAnimation.Data.Identifier == identifier) return tweenAnimation;
            }

            return default;
        }

        public static List<UTweenAnimation> GetTweenAnimations(this MonoBehaviour monoBehaviour)
        {
            var tweenAnimations = new List<UTweenAnimation>();
            var components = monoBehaviour.GetComponents<UTweenAnimation>();
            foreach (var tweenAnimation in components)
            {
                tweenAnimations.Add(tweenAnimation);
            }

            return tweenAnimations;
        }

        public static List<UTweenAnimation> GetTweenAnimationsInChildren(this MonoBehaviour monoBehaviour)
        {
            var tweenAnimations = new List<UTweenAnimation>();
            var components = monoBehaviour.GetComponentsInChildren<UTweenAnimation>();
            foreach (var tweenAnimation in components)
            {
                tweenAnimations.Add(tweenAnimation);
            }

            return tweenAnimations;
        }

        public static List<UTweenAnimation> GetTweenAnimations(this MonoBehaviour monoBehaviour, string identifier)
        {
            var tweenAnimations = monoBehaviour.GetTweenAnimations(t => t.Data.Identifier == identifier);
            return tweenAnimations;
        }

        public static List<UTweenAnimation> GetTweenAnimationsInChildren(this MonoBehaviour monoBehaviour, string identifier)
        {
            var tweenAnimations = monoBehaviour.GetTweenAnimationsInChildren(t => t.Data.Identifier == identifier);
            return tweenAnimations;
        }

        public static List<UTweenAnimation> GetTweenAnimations(this MonoBehaviour monoBehaviour, Predicate<UTweenAnimation> predicate)
        {
            var tweenAnimations = new List<UTweenAnimation>();
            var components = monoBehaviour.GetComponents<UTweenAnimation>();
            foreach (var tweenAnimation in components)
            {
                if (predicate == null || predicate(tweenAnimation)) tweenAnimations.Add(tweenAnimation);
            }

            return tweenAnimations;
        }

        public static List<UTweenAnimation> GetTweenAnimationsInChildren(this MonoBehaviour monoBehaviour, Predicate<UTweenAnimation> predicate)
        {
            var tweenAnimations = new List<UTweenAnimation>();
            var components = monoBehaviour.GetComponentsInChildren<UTweenAnimation>();
            foreach (var tweenAnimation in components)
            {
                if (predicate == null || predicate(tweenAnimation)) tweenAnimations.Add(tweenAnimation);
            }

            return tweenAnimations;
        }

        #endregion

        #region TweenData

        public static TweenData GetTweenData(this MonoBehaviour monoBehaviour)
        {
            var tweenAnimation = monoBehaviour.GetComponent<UTweenAnimation>();
            if (tweenAnimation != null) return tweenAnimation.Data;
            return default;
        }

        public static TweenData GetTweenDataInChildren(this MonoBehaviour monoBehaviour)
        {
            var tweenAnimation = monoBehaviour.GetComponentInChildren<UTweenAnimation>();
            if (tweenAnimation != null) return tweenAnimation.Data;
            return default;
        }

        public static TweenData GetTweenData(this MonoBehaviour monoBehaviour, string identifier)
        {
            var tweenAnimations = monoBehaviour.GetComponents<UTweenAnimation>();
            foreach (var tweenAnimation in tweenAnimations)
            {
                if (tweenAnimation.Data.Identifier == identifier) return tweenAnimation.Data;
            }

            return default;
        }

        public static TweenData GetTweenDataInChildren(this MonoBehaviour monoBehaviour, string identifier)
        {
            var tweenAnimations = monoBehaviour.GetComponentsInChildren<UTweenAnimation>();
            foreach (var tweenAnimation in tweenAnimations)
            {
                if (tweenAnimation.Data.Identifier == identifier) return tweenAnimation.Data;
            }

            return default;
        }

        public static List<TweenData> GetTweenDatas(this MonoBehaviour monoBehaviour)
        {
            var tweenDatas = new List<TweenData>();
            var components = monoBehaviour.GetComponents<UTweenAnimation>();
            foreach (var tweenAnimation in components)
            {
                tweenDatas.Add(tweenAnimation.Data);
            }

            return tweenDatas;
        }

        public static List<TweenData> GetTweenDatasInChildren(this MonoBehaviour monoBehaviour)
        {
            var tweenDatas = new List<TweenData>();
            var components = monoBehaviour.GetComponentsInChildren<UTweenAnimation>();
            foreach (var tweenAnimation in components)
            {
                tweenDatas.Add(tweenAnimation.Data);
            }

            return tweenDatas;
        }

        public static List<TweenData> GetTweenDatas(this MonoBehaviour monoBehaviour, string identifier)
        {
            var tweenDatas = monoBehaviour.GetTweenDatas(d => d.Identifier == identifier);
            return tweenDatas;
        }

        public static List<TweenData> GetTweenDatasInChildren(this MonoBehaviour monoBehaviour, string identifier)
        {
            var tweenDatas = monoBehaviour.GetTweenDatasInChildren(d => d.Identifier == identifier);
            return tweenDatas;
        }

        public static List<TweenData> GetTweenDatas(this MonoBehaviour monoBehaviour, Predicate<TweenData> predicate)
        {
            var tweenDatas = new List<TweenData>();
            var components = monoBehaviour.GetComponents<UTweenAnimation>();
            foreach (var tweenAnimation in components)
            {
                if (predicate == null || predicate(tweenAnimation.Data)) tweenDatas.Add(tweenAnimation.Data);
            }

            return tweenDatas;
        }

        public static List<TweenData> GetTweenDatasInChildren(this MonoBehaviour monoBehaviour, Predicate<TweenData> predicate)
        {
            var tweenDatas = new List<TweenData>();
            var components = monoBehaviour.GetComponentsInChildren<UTweenAnimation>();
            foreach (var tweenAnimation in components)
            {
                if (predicate == null || predicate(tweenAnimation.Data)) tweenDatas.Add(tweenAnimation.Data);
            }

            return tweenDatas;
        }

        #endregion
    }
}
