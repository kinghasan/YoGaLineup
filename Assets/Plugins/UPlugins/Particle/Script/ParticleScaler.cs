/////////////////////////////////////////////////////////////////////////////
//
//  Script   : ParticleScaler.cs
//  Info     : 粒子尺寸整体调整工具类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2019
//
/////////////////////////////////////////////////////////////////////////////
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.Particle
{
#if UNITY_EDITOR
    [CanEditMultipleObjects, CustomEditor(typeof(ParticleScaler))]
    public class ParticleScalerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var scaler = target as ParticleScaler;
            if (scaler == null) return;
            base.OnInspectorGUI();

            GUILayout.Space(10);

            // Quick Scale
            GUILayout.BeginHorizontal();
            GUI.color = Color.white;
            for (var i = 0.25f; i <= 16f; i *= 2)
            {
                var value = i;
                var btenScale = GUILayout.Button("x" + i.ToString("F2"));
                if (btenScale)
                {
                    scaler.ScaleValue = value;
                    scaler.Adapter();
                }
                GUILayout.Space(5);
            }
            GUILayout.EndHorizontal();

            // Quuck Action
            GUILayout.BeginHorizontal();
            GUI.color = Color.cyan;
            var btnReset = GUILayout.Button("Reset");
            if (btnReset)
            {
                scaler.Reset();
                scaler.Adapter();
            }
            GUILayout.Space(5);
            GUI.color = Color.red;
            var btnRemove = GUILayout.Button("Remove");
            if (btnRemove)
            {
                DestroyImmediate(scaler);
            }
            GUILayout.EndHorizontal();
        }
    }
#endif

    [DisallowMultipleComponent]
    public class ParticleScaler : MonoBehaviour
    {
        public Vector3 ScaleVector;
        public float ScaleValue;

#if UNITY_EDITOR
        public ParticleSystem[] Particles { get; protected set; }

        private List<ScaleData> _scaleDatas;

        private class ScaleData
        {
            public Transform Transform;
            public Vector3 BeginScale = Vector3.one;
        }

        public void Awake()
        {
            Reset();
            Load();
            Adapter();
        }

        public void OnEnable()
        {
            Load();
            Adapter();
        }

        public void Reset()
        {
            ScaleVector = Vector3.one;
            ScaleValue = 1f;
        }

        public void Load()
        {
            if (_scaleDatas != null) return;
            _scaleDatas = new List<ScaleData>();
            Particles = GetComponentsInChildren<ParticleSystem>(true);
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

        public void Adapter()
        {
            if (_scaleDatas == null)
            {
                Load();
            }
            foreach (var data in _scaleDatas)
            {
                var scale = new Vector3(data.BeginScale.x * ScaleVector.x, data.BeginScale.y * ScaleVector.y, data.BeginScale.z * ScaleVector.z);
                scale *= ScaleValue;
                data.Transform.localScale = scale;
            }
        }

        public void OnValidate()
        {
            Adapter();
        }
#endif
    }
}
