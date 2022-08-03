/////////////////////////////////////////////////////////////////////////////
//
//  Script   : RealTimeParticle.cs
//  Info     : 实时粒子，使ParticleSystem不受TimeScale影响
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.Particle
{
    public class RealTimeParticle : MonoBehaviour
    {
        public ParticleSystem[] Particles { get; protected set; }

        private float _deltaTime;
        private float _timeAtLastFrame;

        private void Awake()
        {
            Load();
        }

        public void Load(bool reload = false)
        {
            if (Particles == null || Particles.Length == 0 || reload)
            {
                Particles = GetComponentsInChildren<ParticleSystem>(false);
            }
        }

        private void Update()
        {
            if(Particles == null) return;
            for (var i = 0; i < Particles.Length; i++)
            {
                var particle = Particles[i];
                if (particle == null) return;
                _deltaTime = Time.realtimeSinceStartup - _timeAtLastFrame;
                _timeAtLastFrame = Time.realtimeSinceStartup;
                if (Mathf.Abs(Time.timeScale - 1f) < 1e-6) return;
                particle.Simulate(_deltaTime, true, false);
                particle.Play();
            }
        }
    }
}