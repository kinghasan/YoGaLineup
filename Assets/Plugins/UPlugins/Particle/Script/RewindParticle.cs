/////////////////////////////////////////////////////////////////////////////
//
//  Script   : RewindParticle.cs
//  Info     : 粒子倒放
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using Aya.Extension;

namespace Aya.Particle
{
    public class RewindParticle : MonoBehaviour
    {
        public float TimeScale = 1.0f;
        public ParticleSystem[] Particles { get; set; }
        public float StartTime { get; set; }

        private float[] _simulationTimes;

        public void Awake()
        {
            Load();
        }

        public void Load(bool reload = false)
        {
            if (Particles == null || Particles.Length == 0 || reload)
            {
                Particles = GetComponentsInChildren<ParticleSystem>(false);
                _simulationTimes = new float[Particles.Length];
                StartTime = gameObject.GetParticleDuration();
            }
        }

        public void OnEnable()
        {
            for (var i = 0; i < _simulationTimes.Length; i++)
            {
                _simulationTimes[i] = 0.0f;
            }
            Particles[0].Simulate(StartTime, true, false, true);
        }
        public void Update()
        {
            Particles[0].Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

            for (var i = Particles.Length - 1; i >= 0; i--)
            {
                var useAutoRandomSeed = Particles[i].useAutoRandomSeed;
                Particles[i].useAutoRandomSeed = false;

                Particles[i].Play(false);

                var deltaTime = Particles[i].main.useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
                _simulationTimes[i] -= (deltaTime * Particles[i].main.simulationSpeed) * TimeScale;

                var currentSimulationTime = StartTime + _simulationTimes[i];
                Particles[i].Simulate(currentSimulationTime, false, false, true);

                Particles[i].useAutoRandomSeed = useAutoRandomSeed;

                if (currentSimulationTime < 0.0f)
                {
                    Particles[i].Play(false);
                    Particles[i].Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
                }
            }
        }
    }
}
