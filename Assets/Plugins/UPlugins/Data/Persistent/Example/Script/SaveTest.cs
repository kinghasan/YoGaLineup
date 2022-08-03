using System.Collections.Generic;
using UnityEngine;
using Aya.Data.Persistent;
using Aya.Security;

namespace Aya.Sample
{
    public class SaveTest : MonoBehaviour
    {
        public enum TestType
        {
            Save = 1,
            Load = 2,
        }

        public TestType Action = TestType.Save;

        public enum PlayerType
        {
            Normal = 1,
            GameMaster = 2,
        }

        public class EquipData
        {
            public int ID = 0;
            public int Level = 1;
            public float UpValue = 1.5f;
        }

        public class PlayerData
        {
            public string NickName = "Player";
            public int Level = 100;
            public int Hp = 1000;
            public cInt Money = 99999;
            public List<EquipData> Equips;
            public PlayerType Type = PlayerType.Normal;
            public Texture2D Icon;
            public Vector3 Pos;
            public Quaternion Rot;

            public PlayerData()
            {
                Equips = new List<EquipData> {new EquipData(), new EquipData()};
                Icon = new Texture2D(128, 128);
                // TextureUtil.ClearWithColor(Icon, Color.red);
                Pos = new Vector3(1, 2, 3);
                Rot = Quaternion.Euler(new Vector3(100, 200, 360));
            }
        }

        public void Start()
        {
            SaveData.Type = SaveType.Json;

            switch (Action)
            {
                case TestType.Save:
                    Save();
                    break;
                case TestType.Load:
                    Load();
                    break;
            }
        }

        public void Save()
        {
            var player = new PlayerData();
            cInt c = 2;
            SaveData.Set("Player", player);
            SaveData.Set("A", 4);
            SaveData.Set("B", "2");
            SaveData.Set("C", c);

            SaveData.Save();

            Debug.Log("Save Success");
        }

        public void Load()
        {
            var player = SaveData.Get<PlayerData>("Player");
            var a = SaveData.Get<int>("A");
            var b = SaveData.Get<string>("B");
            var c = SaveData.Get<cInt>("C");

            Debug.Log("Load Success");
        }
    }
}
