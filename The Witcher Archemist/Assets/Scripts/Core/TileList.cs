using Assets.Scripts.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TileList
{
    public static Dictionary<string, Texture> textureToName = new Dictionary<string, Texture>();
    public static Dictionary<string, Sprite> spriteToName = new Dictionary<string, Sprite>();


    public static void SetImageList()
    {
        SetSpriteList();
        SetTextureList();
    }
    public static void SetSpriteList()
    {
        Methods method = new Methods();
        object[] sprites = Resources.LoadAll("Tiles");

        for (int i = 0; i < sprites.Length; i++)
        {
            string spriteName = method.String_Cut_Char(Convert.ToString(sprites[i]), ' ');
            spriteToName.Add(spriteName, sprites[i] as Sprite);
        }
        //Debug.Log(spriteToName["shop_entire_tile_Wall"]);
    }

    public static void SetTextureList()
    {
        Methods method = new Methods();
        object[] textures = Resources.LoadAll<Texture>("EditorTiles/Level");

        for (int i = 0; i < textures.Length; i++)
        {
            string textureName = method.String_Cut_Char(Convert.ToString(textures[i]), ' ');
            //Debug.Log(textureName);

            textureToName.Add(textureName, textures[i] as Texture);
        }
    }

    public static void ClearTileList()
    {
        textureToName.Clear();
        spriteToName.Clear();
    }

    public static bool isEmptyImageList()
    {
        bool isEmpty = false;

        foreach (var texture in TileList.textureToName)
        {
            if (TileList.spriteToName.ContainsKey(texture.Key))
            {
                isEmpty = false;
                return isEmpty;
            }
        }

        foreach (var sprite in TileList.spriteToName)
        {
            if (TileList.spriteToName.ContainsKey(sprite.Key))
            {
                isEmpty = false;
                return isEmpty;
            }
        }

        isEmpty = true;

        return isEmpty;
    }
}


