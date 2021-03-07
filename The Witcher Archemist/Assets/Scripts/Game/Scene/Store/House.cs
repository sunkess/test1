using Assets.Scripts.Game.Common;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Contexts;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityScript.Steps;

[Serializable]
public class House : MonoBehaviour
{
    public static House instance;
    public HouseData houseData;

    public GameObject tiles;

    public Text store_lv;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            instance = null;
        }

        instance = this;
    }
    private void Start()
    {
        GameData.NameToSprite();

        houseData = SaveManager.LevelLoad<HouseData>(houseData, 1);

        Set_StoreLv();
    }

    

    void GetExp()
    {
        houseData.exp++;

        if (houseData.exp >= houseData.nextExp)
        {
            houseData = SaveManager.LevelLoad<HouseData>(houseData, Convert.ToInt32(houseData.lv) + 1);
            Set_StoreLv();
        }
    }

    void Set_StoreLv()
    {
        store_lv.text = houseData.lv;
        Set_Tile();
    }


    void Set_Tile()
    {
        DeleteTile();

        GameObject storeTile = new GameObject("StoreTile");

        Sprite[] tileGroundSprites = Resources.LoadAll<Sprite>("Tiles/shop_tile_v2");
        Sprite[] tileWall = Resources.LoadAll<Sprite>("Tiles/shop_entire_tile");

        for (int y = 0; y < houseData.y; y++)
        {
            for (int x = 0; x < houseData.x; x++)
            {
                GameObject tile = new GameObject("object");
                tile.AddComponent<SpriteRenderer>();
                #region ==============벽===================

                if(x == 0 || x == houseData.x - 1)
                {
                    tile.name = "shop_entire_tile_Wall";
                    tile.GetComponent<SpriteRenderer>().sprite = GameData.tileToName["shop_entire_tile_Wall"];
                    tile.AddComponent<BoxCollider2D>().isTrigger = false;
                }

                //================================벽 끝=========================================
                #endregion
                #region ==============바닥=================
                //================================바닥 타일====================================
                //왼쪽 타일과 오른쪽 타일
                if (x == 1 || x == houseData.x - 2)
                {
                    if (x == 1)
                    {
                        tile.name = "Left_Ground";
                        tile.GetComponent<SpriteRenderer>().sprite = GameData.tileToName["Shop_Ground_Left"];
                    }
                    else if (x == houseData.x - 2)
                    {
                        tile.name = "Right_Ground";
                        tile.GetComponent<SpriteRenderer>().sprite = GameData.tileToName["Shop_Ground_Right"];
                    }
                    tile.AddComponent<BoxCollider2D>().isTrigger = true;
                    tile.AddComponent<Place_Tile>();
                }
                //중간 타일
                else if ((1 < x && x < houseData.x - 2))
                {
                    tile.name = "Middle_Ground";
                    tile.GetComponent<SpriteRenderer>().sprite = GameData.tileToName["Shop_Ground_Middle"];
                    tile.AddComponent<BoxCollider2D>().isTrigger = true;
                    tile.AddComponent<Place_Tile>();
                }


                //================================바닥 타일 끝=================================
                #endregion


                tile.GetComponent<SpriteRenderer>().sortingLayerName = "Tile";
                tile.transform.position = new Vector2(x, y);
                tile.transform.parent = storeTile.transform;
                storeTile.transform.parent = transform;
            }
        }

        int wall_height = 3;

        //벽 늘리기
        for (int y = 0; y < wall_height; y++)
        {
            for (int x = 0; x < houseData.x; x++)
            {
                GameObject tile = new GameObject("object");
                tile.AddComponent<SpriteRenderer>();

                #region =================벽==============

                //양쪽 벽
                if(x == 0 || x == houseData.x - 1)
                {
                    tile.name = "shop_entire_tile_Wall";
                    tile.GetComponent<SpriteRenderer>().sprite = GameData.tileToName["shop_entire_tile_Wall"];
                    tile.AddComponent<BoxCollider2D>().isTrigger = true;
                    tile.AddComponent<Tile>();
                }
                //뒷 벽
                else if(x > 0 && x < houseData.x - 1)
                {
                    tile.name = "shop_entire_tile_Back";
                    tile.GetComponent<SpriteRenderer>().sprite = GameData.tileToName["shop_entire_tile_Back"];
                    tile.AddComponent<BoxCollider2D>().isTrigger = true;
                    tile.AddComponent<Tile>();
                }
                
                #endregion


                tile.GetComponent<SpriteRenderer>().sortingLayerName = "Tile";
                tile.transform.position = new Vector2(x, y + houseData.y);
                tile.transform.parent = storeTile.transform;
                storeTile.transform.parent = transform;
            }
        }

        tiles = storeTile;
    }

    void DeleteTile()
    {
        Destroy(tiles);
    }

}
