using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.AD
{
    public abstract class ADSourceBase
    {
        public abstract ADLocationType Type { get; }

        public int ID { get; internal set; }

        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_name))
                {
                    _name = Type + "_" + ID;
                }
                return _name;
            }
        }
        private string _name;

        public ADLocationBase Location { get; internal set; }

        public Action<bool> OnInited = delegate { };
        public Action<bool> OnLoaded = delegate { };
        public Action OnShowed = delegate { };
        public Action OnCloseed = delegate { };
        public Action<bool> OnResult = delegate { };

        public bool IsInited { get; protected set; }
        public bool IsLoading { get; protected set; }
        public bool IsShowing { get; protected set; }
        public abstract bool IsReady { get; }

        public abstract void Init(params object[] args);
        public abstract void Load(Action<bool> onDone = null);
        public abstract void Show(Action<bool> onDone = null);
        public abstract void Close();
    }
}

