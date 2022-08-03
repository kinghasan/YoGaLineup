using UnityEngine;

namespace Aya.TweenPro
{
    [CreateAssetMenu(fileName = "UTweenSetting", menuName = "UTween Pro/UTween Setting")]
    public class UTweenSetting : UTweenSettingBase<UTweenSetting>
    {
        [Header("UTweenManager")] 
        public bool ShowManagerInHierarchy = true;
    }
}