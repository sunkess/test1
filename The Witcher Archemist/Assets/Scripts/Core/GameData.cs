using Assets.Scripts.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameData
{
    public static Dictionary<string, Sprite> tileToName = new Dictionary<string, Sprite>();
    public static List<Sprite> tiles = new List<Sprite>();

    public static void NameToSprite()
    {
        object[] sprites = Resources.LoadAll("Tiles");

        for (int i = 0; i < sprites.Length; i++)
        {
            string spriteName = Methods.String_Cut_Char(Convert.ToString(sprites[i]), ' ');
            tileToName.Add(spriteName, sprites[i] as Sprite);
        }

    }
}


