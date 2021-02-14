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
        //public static string path = Application.dataPath
        public static void Save<T>(T data) where T : class
        {
            
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(Application.dataPath + "/Resources/Levels", json);
        }

        /// <summary>
        /// it's just Load json file as class
        /// </summary>
        /// <typeparam name="T">any class</typeparam>
        /// <param name="data">Class To Load</param>
        /// <returns></returns>
        //public static T Load<T>(T data) where T : class
        //{
        //    //string json = File.ReadAllText(Application.dataPath + "/Resources/Levels/" + lv +".json");
        //    //T storeData = JsonUtility.FromJson<T>(json);
        //    //return storeData;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">any class</typeparam>
        /// <param name="data">Class To Load</param>
        /// <param name="lv">Load to Level</param>
        /// <returns></returns>
        public static T LevelLoad<T>(T data, int lv) where T : class
        {
            string json = File.ReadAllText(Application.dataPath + "/Resources/Levels/" + lv + ".json");
            T storeData = JsonUtility.FromJson<T>(json);
            return storeData;
        }
    }
}
