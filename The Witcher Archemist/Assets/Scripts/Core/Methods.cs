using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Core
{
    class Methods
    {
        public static string String_Cut_Char(string _S, char cut_S)
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
    }
}
