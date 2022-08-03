using System.Collections;
using UnityEngine;

namespace Aya.SDK
{
    public class SDKSettingBase<T> : ScriptableObject where T : SDKSettingBase<T>
    {
        public static T Ins
        {
            get
            {
                if (Instance == null)
                {
                    Instance = Load(typeof(T).Name);
                }
                return Instance;
            }
        }
        protected static T Instance;

        public static T Load(string fileName)
        {
            T ins = null;
            if (fileName != "SDKSetting" && SDKSetting.Ins.DebugMode)
            {
                ins = Resources.Load<T>("Setting/Debug/" + fileName);
            }
#if UNITY_ANDROID
            if (ins == null)
            {
                ins = Resources.Load<T>("Setting/Android/" + fileName);
            }
#elif UNITY_IOS
            if (ins == null)
            {
                ins = Resources.Load<T>("Setting/iOS/" + fileName);
            }
#endif
            if (ins == null)
            {
                ins = Resources.Load<T>("Setting/" + fileName);
            }

            Instance = ins;
            return ins;
        }

        public static void Unload()
        {
            Instance = null;
        }

        public static void Reload()
        {
            Unload();
        }
    }
}
