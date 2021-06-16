using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Common
{
    
    class SaveManager
    {

    }

    class StoreData : SaveManager
    {
        public void Save<T>(T data) where T : class
        {

            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(Application.dataPath + "/Resources/Levels", json);
        }

        public T LevelLoad<T>(int lv) where T : class
        {
            string json = File.ReadAllText(Application.dataPath + "/Resources/Levels/" + lv + ".json");
            T storeData = JsonUtility.FromJson<T>(json);
            return storeData;
        }
    }

    class PlayerData : SaveManager
    {
        public void Save<T>(T data) where T : class
        {

            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(Application.dataPath + "/Resources/player", json);
        }

        public T LevelLoad<T>(int lv)
        {
            string json = File.ReadAllText(Application.dataPath + "/Resources/Player/" + lv + ".json");
            T playerData = JsonUtility.FromJson<T>(json);
            return playerData;
        }

        
    }
}
