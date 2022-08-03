using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aya.Async;
using Aya.SDK;
using UnityEngine;

namespace Aya.AD
{
    public enum SourceState
    {
        Unload = 0,
        Loading = 1,
        Ready = 2,
        Showing,
    }
    public class ADSource : ADSourceBase
    {
        public int MaxFailLoadCount { get; } = 10;
        public int LoadFailCount { get; set; }
        public string UnitID { get; protected set; }
        public SourceState sourceState { get; private set; }

        public override ADLocationType Type { get; }

        public event Action OnShowRequest, OnLoadRequest;

        Action<bool> _onLoadedCallback;

        public SourceState GetState()
        {
            return sourceState;
        }
        public override string ToString()
        {
            return Name + "_" + UnitID;
        }

        public override bool IsReady => sourceState == SourceState.Ready;

        public override void Init(params object[] args)
        {
            UnitID = args[0] as string;
            sourceState = SourceState.Unload;
            IsInited = true;
            SDKDebug.Log("AD", ToString() + "\t Init request");
        }

        public override void Load(Action<bool> onDone = null)
        {
            sourceState = SourceState.Loading;
            IsLoading = true;
            _onLoadedCallback = onDone;
            SDKDebug.Log("AD", ToString() + "\t Load request");
            OnLoadRequest?.Invoke();
        }

        public override void Show(Action<bool> onDone = null)
        {
            sourceState = SourceState.Showing;
            SDKDebug.Log("AD", ToString() + "\t Show request");
            OnShowRequest?.Invoke();
        }
        public override void Close()
        {
            SDKDebug.Log("AD", ToString() + "\t Close request");
        }

        public virtual void OnInit(bool isInit, string msg = null)
        {
            if (isInit) SDKDebug.Log("AD", ToString() + "\t OnInit Success!");
            else SDKDebug.Log("AD", ToString() + "\t OnInit Failed! " + msg ?? "");
            OnInited?.Invoke(isInit);
        }

        public virtual void OnLoad(bool isload, string msg = null)
        {
            sourceState = isload ? SourceState.Ready : SourceState.Unload;
            IsLoading = false;
            if (isload) SDKDebug.Log("AD", ToString() + "\t OnLoad Success!");
            else SDKDebug.Log("AD", ToString() + "\t OnLoad Failed! " + msg ?? "");
            OnLoaded?.Invoke(isload);
            _onLoadedCallback?.Invoke(isload);
            _onLoadedCallback = null;
        }

        public virtual void OnShow(bool isShow, string msg = null)
        {
            sourceState = isShow ? SourceState.Showing : SourceState.Unload;
            if (isShow) SDKDebug.Log("AD", ToString() + "\t OnShow Success!");
            else SDKDebug.Log("AD", ToString() + "\t OnShow Failed! " + msg ?? "");
            if (isShow) OnShowed?.Invoke();
        }

        public virtual void OnClose()
        {
            sourceState = SourceState.Unload;
            SDKDebug.Log("AD", ToString() + "\t OnClose");
            OnCloseed?.Invoke();
        }

        public virtual void OnReward()
        {
            sourceState = SourceState.Unload;
            SDKDebug.Log("AD", ToString() + "\t OnReward");
            OnResult?.Invoke(true);
        }

    }

    public class ADRewardSource : ADSource
    {
        public override ADLocationType Type => ADLocationType.RewardedVideo;

        Action<bool> _onShowedCallback;

        bool isReward;
        bool isClosed;

        public override void Show(Action<bool> onDone = null)
        {
            base.Show(onDone);
            _onShowedCallback = onDone;
            isReward = false;
            isClosed = false;
        }

        public override void OnReward()
        {
            base.OnReward();
            isReward = true;
            UnityThread.ExecuteWhen(() =>
            {
                _onShowedCallback?.Invoke(isReward);
                _onShowedCallback = null;
            }, () => isClosed);
        }

        public override void OnClose()
        {
            //_onShowedCallback?.Invoke(isReward);
            //_onShowedCallback = null;
            base.OnClose();
            isClosed = true;
        }
    }

    public class ADInterstitialSource : ADSource
    {
        public override ADLocationType Type => ADLocationType.Interstitial;

        Action<bool> _onShowedCallback;

        public override void Show(Action<bool> onDone = null)
        {
            base.Show(onDone);
            _onShowedCallback = onDone;
        }

        public override void OnClose()
        {
            _onShowedCallback?.Invoke(true);
            _onShowedCallback = null;
            base.OnClose();
        }
    }

    public abstract class ADChannelAutoLoadAble<TLocationBanner, TLocationInterstitial, TLocationRewardedVideo> :
        ADChannelBase<TLocationBanner, TLocationInterstitial, TLocationRewardedVideo>
        where TLocationBanner : ADLocationBase
    where TLocationInterstitial : ADLocationBase
    where TLocationRewardedVideo : ADLocationBase
    {
        class AutoLoaderSource
        {
            public ADSource Source { get; private set; }

            public bool NeedLoad => Source.sourceState == SourceState.Unload || Source.sourceState != SourceState.Ready && OverTime;


            float timer = 0;
            public AutoLoaderSource(ADSource source)
            {
                Source = source;
                Source.OnLoadRequest += () => SetTimer(20);
                Source.OnLoaded += b => { if (!b) SetTimer(5); };
                Source.OnShowRequest += () => SetTimer(10);
            }

            public void Load(Action<bool> callBack)
            {
                Source.Load(callBack);
            }

            public void SetTimer(float time)
            {
                timer = Time.time + time;
            }

            public bool OverTime => Time.time > timer;

        }
        Dictionary<string, ADSource> allSource = new Dictionary<string, ADSource>();
        List<AutoLoaderSource> autoLoadSources = new List<AutoLoaderSource>();
        Queue<AutoLoaderSource> waiting = new Queue<AutoLoaderSource>();
        AutoLoaderSource current_sources;

        public ADSource GetSource(string id)
        {
            if (allSource.ContainsKey(id)) return allSource[id];
            return null;
        }
        public virtual void Update()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable) return;
            foreach (var s in autoLoadSources)
            {
                if (s != current_sources && s.NeedLoad && !waiting.Contains(s))
                {
                    waiting.Enqueue(s);
                }
            }
            if (current_sources == null && waiting.Count > 0) current_sources = waiting.Dequeue();
            if (current_sources != null)
            {
                if (current_sources.NeedLoad)
                {
                    var s = current_sources;
                    current_sources.Load((b) =>
                    {
                        if (s == current_sources)// 防止回调掉多次 导致当前source被置空
                            current_sources = null;
                    });
                }
                else if (current_sources.Source.sourceState == SourceState.Loading && current_sources.OverTime)
                {
                    current_sources = null;
                }
                else if (current_sources.Source.sourceState == SourceState.Loading && current_sources.OverTime)
                {
                    current_sources = null;
                }
            }
        }

        void AddAutoLoadSources()
        {
            foreach (var l in RewardedVideos)
                foreach (var s in l.Sources)
                {
                    var ads = s as ADSource;
                    if (ads == null) continue;
                    autoLoadSources.Add(new AutoLoaderSource(ads));
                    allSource[ads.UnitID] = ads;
                }

            foreach (var l in Interstitials)
                foreach (var s in l.Sources)
                {
                    var ads = s as ADSource;
                    if (ads == null) continue;
                    autoLoadSources.Add(new AutoLoaderSource(ads));
                    allSource[ads.UnitID] = ads;
                }
        }

        public override void OnInit(params object[] args)
        {
            base.OnInit(args);

            AddAutoLoadSources();

            var obj = new GameObject("ADChannelAutoLoadAble");
            obj.AddComponent<UpdateListener>().action = Update;
            obj.hideFlags = HideFlags.HideInHierarchy;
            GameObject.DontDestroyOnLoad(obj);
        }

        public override void Load()
        {
            // 只加载banner 其他不做任何处理 走自动加载
            foreach (var b in Banners) b.Load();
        }
    }

    public class UpdateListener : MonoBehaviour
    {
        public Action action;
        void Update()
        {
            action?.Invoke();
        }
    }

}