using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.AD
{
    public abstract class ADLocationBase
    {
        public string Key { get; protected set; }

        public ADLocationType Type
        {
            get { return CurrentSource.Type; }
        }

        public Action<bool> OnInited = delegate { };
        public Action<bool> OnLoaded = delegate { };
        public Action OnShowed = delegate { };
        public Action OnClosed = delegate { };
        public Action<bool> OnResult = delegate { };

        public List<ADSourceBase> Sources { get; protected set; }

        public abstract bool IsReady { get; }
        public abstract bool IsShowing { get; }
        public abstract ADSourceBase CurrentSource { get; }

        public abstract void Init(params object[] args);
        public abstract void Load(Action<bool> onDone = null);
        public abstract void Show(Action<bool> onDone = null);
        public abstract void Close();
    }

    public abstract class ADLocationBase<T> : ADLocationBase where T : ADSourceBase
    {
        public override bool IsReady
        {
            get
            {
                return CurrentSource != null;
            }
        }

        public override bool IsShowing
        {
            get
            {
                foreach (var source in Sources)
                {
                    if (source.IsShowing) return true;
                }
                return false;
            }
        }

        public override ADSourceBase CurrentSource
        {
            get
            {
                foreach (var source in Sources)
                {
                    if (source.IsReady) return source;
                }
                return null;
            }
        }

        public override void Init(params object[] args)
        {
            var param = args[0] as ADLocationParam;
            if(param == null) return;
            Key = param.Key;
            Sources = new List<ADSourceBase>();
            var index = 1;
            foreach (var id in param.IDs)
            {
                var source = Activator.CreateInstance(typeof(T)) as T;
                if(source == null) continue;
                source.Location = this;
                source.ID = index;
                source.OnInited += ret => { OnInited(ret); };
                source.OnLoaded += ret => { OnLoaded(ret); };
                source.OnShowed += () => { OnShowed(); };
                source.OnCloseed += () => { OnClosed(); };
                source.OnResult += ret => { OnResult(ret); };
                source.Init(id);
                Sources.Add(source);
                index++;
            }
        }

        public override void Load(Action<bool> onDone = null)
        {
            foreach (var source in Sources)
            {
                if (onDone != null)
                {
                    source.Load(onDone);
                    continue;
                }
                source.Load();
            }
        }

        public override void Show(Action<bool> onDone = null)
        {
            if (CurrentSource == null)
            {
                Load(ret =>
                {
                    if (ret)
                    {
                        CurrentSource.Show(onDone);
                    }
                });
            }
            else
            {
                CurrentSource.Show(onDone);
            }
        }

        public override void Close()
        {
            if (CurrentSource == null) return;
            CurrentSource.Close();
        }
    }
}
