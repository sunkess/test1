using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Core
{
    class Methods
    {
        public string String_Cut_Char(string _S, char cut_S)
        {
            string sentence = "";

            for (int i = 0; i < _S.Length; i++)
            {
                char word = _S[i];

                if(word == cut_S)
                {
                    break;
                }

                sentence += word;
            }
            return sentence;
        }

        //문자열 비교
        public bool CompareString<T>(T e, string s)
        {

            var eName = Convert.ToString(e);

            if(eName == s)
            {
                return true;
            }

            return false;
        }

        public Dictionary<string, GameObject> FillStringToGameObjectDictinary(string path)
        {
            Dictionary<string, GameObject> list = new Dictionary<string, GameObject>();
            GameObject[] objs = Resources.LoadAll<GameObject>(path);
            int length = objs.Length;
            for (int i = 0; i < length; i++)
            {
                list.Add(objs[i].name, objs[i]);
            }

            return list;
        }
    }
}
