#if UNITY_EDITOR
#if ODIN_INSPECTOR || ODIN_INSPECTOR_3
using System.Collections.Generic;
#endif
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Aya.TweenPro
{
    [CreateAssetMenu(fileName = "UTweenEditorSetting", menuName = "UTween Pro/UTween Editor Setting")]
    public class UTweenEditorSetting : UTweenEditorSettingBase<UTweenEditorSetting>
    {
        [Header("Color")]

        public Color EnableColor;
        public Color DisableColor;
        public Color ErrorColor;
        public Color SelectedColor;

        public Color ProgressColor;
        public Color ProgressBackColor;
        public Color SubProgressColor;
        public Color SubProgressHoldColor;
        public Color ProgressDisableColor;

        [Header("Inspector")]
        public bool ShowGroupReminder;
        public float GroupReminderWidth;
        public bool HideFullSubProgress;

        [Header("Group")]
#if ODIN_INSPECTOR || ODIN_INSPECTOR_3
        [TableList]
#endif
        public List<TweenGroupData> GroupDataList;

        public Dictionary<string, TweenGroupData> GroupDataDic { get; set; }

        public override void Init()
        {
            base.Init();

            GroupDataDic = new Dictionary<string, TweenGroupData>();
            foreach (var data in GroupDataList)
            {
                GroupDataDic.Add(data.Name, data);
            }
        }

        public TweenGroupData GetGroupData(string groupName)
        {
            if (!GroupDataDic.TryGetValue(groupName, out var groupData))
            {
                var tempData = new TweenGroupData()
                {
                    Name = groupName,
                    Color = Color.white,
                    IconPath = null
                };

                return tempData;
            }

            return groupData;
        }

        // [CustomEditor(typeof(UTweenEditorSetting))]
        // public class MyCustomSettingsEditor : Editor
        // {
        //     // Nothing to do, this uses the Generic Editor to display MyCustomSettings properties
        // }

    }
}
#endif