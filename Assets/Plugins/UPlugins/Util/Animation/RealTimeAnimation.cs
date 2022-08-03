/////////////////////////////////////////////////////////////////////////////
//
//  Script : RealTimeAnimation.cs
//  Info   : 实时动画，使动画播放速度不受 TimeScale 影响
//  Author : ls9512
//  E-mail : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2019
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.Util
{
    [RequireComponent(typeof(Animation))]
    public class RealTimeAnimation : MonoBehaviour
    {
        public Animation Animation { get; set; }

        public void Awake()
        {
            Animation = GetComponent<Animation>();
            IsPlaying = false;
        }

        private float _lastFrameTime;
        private float _currentTime;
        private float _progressTime;

        public bool IsPlaying { get; set; }

        public void OnEnable()
        {
            if (Animation.playAutomatically)
            {
                Play();
            }
        }

        public void Play()
        {
            _progressTime = 0f;
            _lastFrameTime = Time.realtimeSinceStartup;
            IsPlaying = true;
        }

        public void Update()
        {
            if (!IsPlaying) return;
            var clip = Animation.clip;
            var state = Animation[clip.name];
            _currentTime = Time.realtimeSinceStartup;
            var deltaTime = _currentTime - _lastFrameTime;
            _lastFrameTime = _currentTime;
            _progressTime += deltaTime * state.speed;
            state.normalizedTime = _progressTime / clip.length;
            Animation.Sample();
            if (_progressTime >= clip.length)
            {
                IsPlaying = false;
            }
        }
    }
}
