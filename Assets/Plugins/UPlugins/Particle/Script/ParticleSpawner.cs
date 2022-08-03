/////////////////////////////////////////////////////////////////////////////
//
//  Script   : ParticleSpwaner.cs
//  Info     : 粒子生成器
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2018
//
/////////////////////////////////////////////////////////////////////////////
using System.Collections.Generic;
using UnityEngine;
using Aya.Extension;
using Aya.Pool;

namespace Aya.Particle
{
    public class ParticleSpawner : MonoBehaviour
    {
        public GameObject Prefab;
        public float Duration;
        public bool RealTime = false;
        public bool AutoPlay = false;
        public bool AutoClear = true;
        public bool AutoDeSpawn = true;
        public bool Rewind = false;
        public Vector3 ScaleVector;
        public float ScaleValue;
        public bool ClearTrailOnEnable = true;

        public bool AutoDeSpawnParticleSpawner { get; internal set; } = false;

        public static EntityPool EntityPool => PoolManager.Ins["Effect"];
        public GameObject Instance { get; protected set; }
        public ParticleSystem[] Particles { get; protected set; }
        public Renderer[] Renderers { get; set; }
        public TrailRenderer[] TrailRenderers { get; set; }
        public LineRenderer[] LineRenderers { get; set; }

        public float ParticleDuration { get; protected set; }

        private List<ScaleData> _scaleDatas;

        private Coroutine _fadeCoroutine;
        private Coroutine _deSpawnCoroutine;

        #region Static API

        public static ParticleSpawner Spawn(GameObject particle, Transform transform, Vector3 position, bool autoPlay = true, bool autoDeSpawn = true, bool realTime = false)
        {
            var spawner = GetParticleSpawner(transform);

            spawner.Prefab = particle;
            spawner.Duration = 0f;
            spawner.AutoPlay = autoPlay;
            spawner.RealTime = realTime;
            spawner.AutoDeSpawn = autoDeSpawn;
            spawner.AutoDeSpawnParticleSpawner = true;
            spawner.transform.position = position;

            if (autoPlay)
            {
                spawner.Play();
            }

            return spawner;
        }

        public static ParticleSpawner Spawn(GameObject particle, Transform transform, Vector3 position, float duration, bool autoPlay = true, bool autoDeSpawn = true, bool realTime = false)
        {
            var spawner = GetParticleSpawner(transform);

            spawner.Prefab = particle;
            spawner.Duration = duration;
            spawner.AutoPlay = autoPlay;
            spawner.RealTime = realTime;
            spawner.AutoDeSpawn = autoDeSpawn;
            spawner.AutoDeSpawnParticleSpawner = true;
            spawner.transform.position = position;

            if (autoPlay)
            {
                spawner.Play();
            }

            return spawner;
        }

        protected static ParticleSpawner GetParticleSpawner(Transform transform)
        {
            var spawnerPrefab = Resources.Load<ParticleSpawner>("ParticleSpawner");
            var spawner = EntityPool.Spawn(spawnerPrefab, transform);
            if (spawner.Instance != null)
            {
                spawner.DeSpawn(false);
            }

            return spawner;
        }

        #endregion

        #region Monobehaviour

        public void OnEnable()
        {
            if (AutoClear)
            {
                Clear();
            }

            if (AutoPlay)
            {
                Play();
            }

            if (ClearTrailOnEnable)
            {
                ClearTrail();
            }
        }

        public void OnDisable()
        {
            // DeSpawn();
            if (ClearTrailOnEnable)
            {
                ClearTrail();
            }
        }

        public void Reset()
        {
            ScaleVector = Vector3.one;
            ScaleValue = 1f;
        }

        #endregion

        #region Scale

        private class ScaleData
        {
            public Transform Transform;
            public Vector3 BeginScale = Vector3.one;
        }

        public void SetScale(float scale)
        {
            ScaleValue = scale;
            ScaleVector = Vector3.one;
            AdapterScale();
        }

        public void SetScale(Vector3 scale)
        {
            ScaleValue = 1f;
            ScaleVector = scale;
            AdapterScale();
        }

        public void AdapterScale()
        {
            if (_scaleDatas == null)
            {
                Spawn();
            }

            foreach (var data in _scaleDatas)
            {
                var scale = new Vector3(data.BeginScale.x * ScaleVector.x, data.BeginScale.y * ScaleVector.y, data.BeginScale.z * ScaleVector.z);
                scale *= ScaleValue;
                if (data.Transform != null)
                {
                    data.Transform.localScale = scale;
                }
            }
        }

        #endregion

        #region Trail Renderer / Line Renderer

        public void ClearTrail()
        {
            if (TrailRenderers != null)
            {
                for (var i = 0; i < TrailRenderers.Length; i++)
                {
                    var trailRenderer = TrailRenderers[i];
                    trailRenderer.Clear();
                }
            }

            if (LineRenderers != null)
            {
                for (var i = 0; i < LineRenderers.Length; i++)
                {
                    var lineRenderer = LineRenderers[i];
                    lineRenderer.Clear();
                }
            }
        }

        #endregion

        #region Load / Unload

        public void Spawn()
        {
            if (Instance != null) return;

            Instance = EntityPool.Spawn(Prefab, transform);
            Instance.transform.ResetLocal();
            Particles = Instance.GetComponentsInChildren<ParticleSystem>(false);
            Renderers = Instance.GetComponentsInChildren<Renderer>(false);
            TrailRenderers = Instance.GetComponentsInChildren<TrailRenderer>(false);
            LineRenderers = Instance.GetComponentsInChildren<LineRenderer>(false);

            if (Mathf.Abs(Duration) < 1e-6)
            {
                ParticleDuration = Instance.GetParticleDuration();
            }
            else
            {
                ParticleDuration = Duration;
            }

            if (RealTime)
            {
                var realTimeParticle = Instance.GetOrAddComponent<RealTimeParticle>();
                realTimeParticle.Load(true);
            }

            if (Rewind)
            {
                var rewindParticle = Instance.GetOrAddComponent<RewindParticle>();
                rewindParticle.Load(true);
            }

            if (_scaleDatas == null)
            {
                _scaleDatas = new List<ScaleData>();
                for (var i = 0; i < Particles.Length; i++)
                {
                    var p = Particles[i];
                    _scaleDatas.Add(new ScaleData()
                    {
                        Transform = p.transform,
                        BeginScale = p.transform.localScale
                    });
                }
            }

            AdapterScale();

            Instance.SetActive(false);
        }

        public void DeSpawn(bool deSpawnSpawaner = true)
        {
            if (Instance != null)
            {
                EntityPool.DeSpawn(Instance);
                Instance = null;
            }

            if (Particles != null)
            {
                Particles = null;
            }

            if (deSpawnSpawaner)
            {
                Prefab = null;
                EntityPool.DeSpawn(this);
            }
        }

        #endregion

        #region Play / Stop

        public void Play()
        {
            if (Prefab == null) return;

            Spawn();
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }

            // 粒子特效位于UI上，则需要处理排序和UI缩放适配
            // var uiBehaviour = Instance.FindComponentInParents<UIBehaviour>();
            // if (uiBehaviour != null)
            // {
            //     UISortingOrder.Sort(Instance);
            //     var uiParticleScaler = Instance.GetOrAddComponent<UIParticleAdapter>();
            //     uiParticleScaler.SetDesignSize(UIController.Ins.DesignWidth, UIController.Ins.DesignHeight);
            // }

            Instance.transform.localPosition = Vector3.zero;
            Instance.SetActive(false);
            Instance.SetActive(true);

            if (ParticleDuration > 0f && AutoDeSpawn)
            {
                if (_deSpawnCoroutine != null)
                {
                    StopCoroutine(_deSpawnCoroutine);
                }

                _deSpawnCoroutine = this.ExecuteDelay(() => DeSpawn(AutoDeSpawnParticleSpawner), ParticleDuration + 0.1f);
            }
        }

        public void Stop()
        {
            if (_deSpawnCoroutine != null)
            {
                StopCoroutine(_deSpawnCoroutine);
            }

            DeSpawn(AutoDeSpawnParticleSpawner);
        }

        public void Clear()
        {
            if (Particles == null) return;
            for (var i = 0; i < Particles.Length; i++)
            {
                var particle = Particles[i];
                particle.Clear();
            }
        }

        #endregion

        #region Fade In / Out

        public void FadeIn(float time = 0)
        {
            AutoDeSpawn = false;
            Play();
            if (_fadeCoroutine != null) StopCoroutine(_fadeCoroutine);
            _fadeCoroutine = this.ExecuteDelay(
                () =>
                {
                    for (var i = 0; i < Particles.Length; i++)
                    {
                        var particle = Particles[i];
                        var emission = particle.emission;
                        emission.enabled = true;
                    }
                }, time);
        }

        public void FadeOut(float time = 0)
        {
            if (Particles == null) return;
            if (_fadeCoroutine != null) StopCoroutine(_fadeCoroutine);
            _fadeCoroutine = this.ExecuteDelay(
                () =>
                {
                    for (var i = 0; i < Particles.Length; i++)
                    {
                        var particle = Particles[i];
                        var emission = particle.emission;
                        emission.enabled = false;
                    }
                }, time);
        }

        #endregion
    }
}