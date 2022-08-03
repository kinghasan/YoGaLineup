using System;
using UnityEngine;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    // TODO..
    // 启动自动播放问题
    // 自动计算时长

    [Tweener("Particle System", "Rendering")]
    [Serializable]
    public partial class TweenParticleSystem : TweenValueFloat<ParticleSystem>
    {
        public bool RandStart;
        public bool WithChildren;

        public override float Value
        {
            get => _value;
            set
            {
                _value = value;

                Target.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                if (WithChildren)
                {
                    foreach (var particleSystem in _particleSystems)
                    {
                        Playback(particleSystem, _value);
                    }
                }
                else
                {
                    Playback(Target, _value);
                }
            }
        }

        private float _value;
        private bool _initRandomSeed;
        private uint _randomSeed;

        private ParticleSystem[] _particleSystems;

        public override void PreSample()
        {
            base.PreSample();
            _particleSystems = Target.GetComponentsInChildren<ParticleSystem>();
            Target.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

            if (Application.isPlaying)
            {
                if (RandStart || !_initRandomSeed)
                {
                    _randomSeed = (uint)Random.Range(0f, 10000f);
                    _initRandomSeed = true;
                }
            }
        }

        public void Playback(ParticleSystem particleSystem, float time)
        {
            var useAutoRandomSeed = particleSystem.useAutoRandomSeed;
            particleSystem.useAutoRandomSeed = false;
            particleSystem.randomSeed = _randomSeed;
            particleSystem.Play(false);
            particleSystem.Simulate(time, false, false, false);
            particleSystem.useAutoRandomSeed = useAutoRandomSeed;
            particleSystem.Play(false);
        }

        public override void StopSample()
        {
            base.StopSample();
            Target.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        public override void Reset()
        {
            base.Reset();

            _initRandomSeed = false;
            RandStart = true;
            WithChildren = true;
        }
    }

#if UNITY_EDITOR

    public partial class TweenParticleSystem : TweenValueFloat<ParticleSystem>
    {
        [TweenerProperty, NonSerialized] public SerializedProperty RandStartProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty WithChildrenProperty;

        public override void DrawAppend()
        {
            base.DrawAppend();

            using (GUIHorizontal.Create())
            {
                GUIUtil.DrawToggleButton(RandStartProperty);
                GUIUtil.DrawToggleButton(WithChildrenProperty);
            }
        }
    }

#endif

    #region Extension

    public static partial class UTween
    {
        public static TweenParticleSystem Time(ParticleSystem particleSystem, float to, float duration)
        {
            var tweener = Play<TweenParticleSystem, ParticleSystem, float>(particleSystem, to, duration);
            return tweener;
        }

        public static TweenParticleSystem Time(ParticleSystem particleSystem, float from, float to, float duration)
        {
            var tweener = Play<TweenParticleSystem, ParticleSystem, float>(particleSystem, from, to, duration);
            return tweener;
        }
    }

    public static partial class TweenParticleSystemExtension
    {
        public static TweenParticleSystem Time(this ParticleSystem particleSystem, float to, float duration)
        {
            var tweener = UTween.Time(particleSystem, to, duration);
            return tweener;
        }

        public static TweenParticleSystem Time(this ParticleSystem particleSystem, float from, float to, float duration)
        {
            var tweener = UTween.Time(particleSystem, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
