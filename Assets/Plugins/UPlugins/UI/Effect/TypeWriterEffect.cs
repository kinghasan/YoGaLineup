/////////////////////////////////////////////////////////////////////////////
//
//  Script   : TypeWriterEffect.cs
//  Info     : UI Text 打字机效果
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Internet
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Text;
using System.Collections.Generic;

namespace UnityEngine.UI
{
    [RequireComponent(typeof(Text))]
    [AddComponentMenu("UI/Effects/TypeWriter Effect")]
    public class TypeWriterEffect : MonoBehaviour
    {
        public static TypeWriterEffect Instance;

        private struct FadeEntry
        {
            public int Index;
            public string Text;
            public float Alpha;
        }

        /// <summary>
        /// How many characters will be printed per second.
        /// </summary>
        public int CharsPerSecond = 20;

        /// <summary>
        /// How long it takes for each character to fade in.
        /// </summary>
        public float FadeInTime = 0f;

        /// <summary>
        /// How long to pause when a period is encountered (in seconds).
        /// </summary>
        public float DelayOnPeriod = 0f;

        /// <summary>
        /// How long to pause when a new line character is encountered (in seconds).
        /// </summary>
        public float DelayOnNewLine = 0f;

        /// <summary>
        /// If a scroll view is specified, its UpdatePosition() function will be called every time the text is updated.
        /// </summary>
        public ScrollRect ScrollView;

        /// <summary>
        /// If set to 'true', the label's dimensions will be that of a fully faded-in content.
        /// </summary>
        public bool KeepFullDimensions = false;

        /// <summary>
        /// Is affected by Time.timeScale.
        /// </summary>
        public bool IsTimeScale = true;

        /// <summary>
        /// Event delegate triggered when the typewriter effect finishes.
        /// </summary>
        // public List<EventDelegate> OnFinished = new List<EventDelegate>();
        public Action OnFinished = delegate { };

        private Text _label;
        private string _fullText = "";
        private int _currentOffset = 0;
        private float _nextChar = 0f;
        private bool _reset = true;
        private bool _active = false;

        private float _timer = 0;

        private readonly List<FadeEntry> _mFade = new List<FadeEntry>();

        /// <summary>
        /// Whether the typewriter effect is currently active or not.
        /// </summary>
        public bool IsActive => _active;

        /// <summary>
        /// Reset the typewriter effect to the beginning of the label.
        /// </summary>
        public void ResetToBeginning()
        {
            Finish();
            _reset = true;
            _active = true;
            _nextChar = 0f;
            _currentOffset = 0;
            Update();
        }

        public void Finish()
        {
            if (!_active) return;
            _active = false;

            if (!_reset)
            {
                _currentOffset = _fullText.Length;
                _mFade.Clear();
                _label.text = _fullText;
            }

            Instance = this;
            // EventDelegate.Execute(OnFinished);
            OnFinished();
            Instance = null;
        }

        void OnEnable()
        {
            _reset = true;
            _active = true;
            _timer = Time.realtimeSinceStartup;
        }

        void Update()
        {
            if (!_active) return;
            _timer += IsTimeScale ? Time.deltaTime : Time.unscaledDeltaTime;

            if (_reset)
            {
                _currentOffset = 0;
                _reset = false;
                _label = GetComponent<Text>();
                _fullText = _label.text;
                _mFade.Clear();

            }

            while (_currentOffset < _fullText.Length && _nextChar <= _timer)
            {
                var lastOffset = _currentOffset;
                CharsPerSecond = Mathf.Max(1, CharsPerSecond);

                //TODO 添加标签过滤
                ++_currentOffset;

                // Reached the end? We're done.
                if (_currentOffset > _fullText.Length) break;

                // Periods and end-of-line characters should pause for a longer time.
                var delay = 1f / CharsPerSecond;
                var c = (lastOffset < _fullText.Length) ? _fullText[lastOffset] : '\n';

                if (c == '\n')
                {
                    delay += DelayOnNewLine;
                }
                else if (lastOffset + 1 == _fullText.Length || _fullText[lastOffset + 1] <= ' ')
                {
                    if (c == '.')
                    {
                        if (lastOffset + 2 < _fullText.Length && _fullText[lastOffset + 1] == '.' &&
                            _fullText[lastOffset + 2] == '.')
                        {
                            delay += DelayOnPeriod * 3f;
                            lastOffset += 2;
                        }
                        else delay += DelayOnPeriod;
                    }
                    else if (c == '!' || c == '?')
                    {
                        delay += DelayOnPeriod;
                    }
                }

                if (Math.Abs(_nextChar) < 1e-6)
                {
                    _nextChar = _timer + delay;
                }
                else _nextChar += delay;

                if (Math.Abs(FadeInTime) > 1e-6)
                {
                    // There is smooth fading involved
                    var fe = new FadeEntry
                    {
                        Index = lastOffset,
                        Alpha = 0f,
                        Text = _fullText.Substring(lastOffset, _currentOffset - lastOffset)
                    };
                    _mFade.Add(fe);
                }
                else
                {
                    // No smooth fading necessary
                    _label.text = KeepFullDimensions
                        ? _fullText.Substring(0, _currentOffset) + "[00]" + _fullText.Substring(_currentOffset)
                        : _fullText.Substring(0, _currentOffset);
                }
            }

            // Alpha-based fading
            if (_mFade.Count != 0)
            {
                for (var i = 0; i < _mFade.Count;)
                {
                    var fe = _mFade[i];
                    fe.Alpha += (IsTimeScale ? Time.deltaTime : Time.unscaledDeltaTime) / FadeInTime;

                    if (fe.Alpha < 1f)
                    {
                        _mFade[i] = fe;
                        ++i;
                    }
                    else _mFade.RemoveAt(i);
                }

                if (_mFade.Count == 0)
                {
                    if (KeepFullDimensions)
                        _label.text = _fullText.Substring(0, _currentOffset) + "[00]" +
                                       _fullText.Substring(_currentOffset);
                    else _label.text = _fullText.Substring(0, _currentOffset);
                }
                else
                {
                    var sb = new StringBuilder();

                    for (var i = 0; i < _mFade.Count; ++i)
                    {
                        var fe = _mFade[i];

                        if (i == 0)
                        {
                            sb.Append(_fullText.Substring(0, fe.Index));
                        }

                        sb.Append(EncodeAlpha(fe.Text, fe.Alpha));
                    }

                    if (KeepFullDimensions)
                    {
                        sb.Append("[00]");
                        sb.Append(_fullText.Substring(_currentOffset));
                    }

                    _label.text = sb.ToString();
                }
            }
            else if (_currentOffset >= _fullText.Length)
            {
                Instance = this;
                // EventDelegate.Execute(OnFinished);
                OnFinished();
                Instance = null;
                _active = false;
            }
        }

        private string EncodeAlpha(string text, float a)
        {
            var colorCode = "";
            var alpha = Mathf.Clamp(Mathf.RoundToInt(a * 255f), 0, 255);
            colorCode += DecimalToHex8(Mathf.RoundToInt(_label.color.r * 255f));
            colorCode += DecimalToHex8(Mathf.RoundToInt(_label.color.g * 255f));
            colorCode += DecimalToHex8(Mathf.RoundToInt(_label.color.b * 255f));
            colorCode += DecimalToHex8(alpha);
            return "<color=#" + colorCode + ">" + text + "</color>";
        }

        [System.Diagnostics.DebuggerHidden]
        [System.Diagnostics.DebuggerStepThrough]
        public static string DecimalToHex8(int num)
        {
            num &= 0xFF;
            return num.ToString("X2");
        }

    }
}