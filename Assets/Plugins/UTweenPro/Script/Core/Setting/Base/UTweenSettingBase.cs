using UnityEngine;

namespace Aya.TweenPro
{
    public abstract class UTweenSettingBase<T> : ScriptableObject
        where T : UTweenSettingBase<T>
    {
        #region Instance
       
        public static T Ins
        {
            get
            {
                if (Instance == null) Instance = LoadResources();
                return Instance;
            }
        }

        protected static T Instance;

        public static T LoadResources()
        {
            var setting  = Resources.Load<T>(typeof(T).Name);
            setting.Init();
            return setting;
        }

        #endregion

        public virtual void Init()
        {

        }
    }
}
