using Assets.Scripts.Game.Common;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityScript.Steps;

[Serializable]
public class House : MonoBehaviour
{
    public static House instance;
    public LVHouseData lvHouseData;
    public HouseData housedata;

    public GameObject tiles;

    public Transform doorPos;

    public GameObject npcPrefab;

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
        LoadLevelData();

        Set_StoreLv();

        StartCoroutine(CreateNpc());
    }

    void LoadLevelData()
    {
        SaveManager storeData = new StoreData();
        SaveManager playerData = new PlayerData();

        //현재 세이브 된 level을 가져와야함.
        int level = 1; //((PlayerData)playerData).LevelLoad<int>(1);

        lvHouseData = ((StoreData)storeData).LevelLoad<LVHouseData>(level);
    }


    IEnumerator CreateNpc()
    {
        yield return new WaitForSeconds(2f);
        GameObject npc = Instantiate(npcPrefab, doorPos.position, Quaternion.identity);

    }
    

    void GetExp()
    {
        SaveManager storeData = new StoreData();
        lvHouseData.exp++;

        if (lvHouseData.exp >= lvHouseData.nextExp)
        {
            lvHouseData = ((StoreData)storeData).LevelLoad<LVHouseData>(Convert.ToInt32(lvHouseData.lv) + 1);
            Set_StoreLv();
        }
    }

    void Set_StoreLv()
    {
        
        Set_Tile();
    }


    void Set_Tile()
    {
        DeleteTile();

        GameObject storeTile = new GameObject("StoreTile");

        int widthWall = 2;

        lvHouseData.x = lvHouseData.x + widthWall;

        for (int y = 0; y < lvHouseData.y; y++)
        {
            for (int x = 0; x < lvHouseData.x; x++)
            {
                GameObject tile = new GameObject("object");
                tile.AddComponent<SpriteRenderer>();
                #region ==============벽===================

                if(x == 0 || x == lvHouseData.x - 1)
                {
                    tile.name = "shop_entire_tile_Wall";
                    tile.GetComponent<SpriteRenderer>().sprite = TileList.spriteToName["shop_entire_tile_Wall"];
                    tile.AddComponent<BoxCollider2D>().isTrigger = false;
                }

                //================================벽 끝=========================================
                #endregion
                #region ==============바닥=================
                //================================바닥 타일====================================
                //왼쪽 타일과 오른쪽 타일
                if (x == 1 || x == lvHouseData.x - 2)
                {
                    if (x == 1)
                    {
                        tile.name = "Left_Ground";
                        tile.GetComponent<SpriteRenderer>().sprite = TileList.spriteToName["Shop_Ground_Left"];
                    }
                    else if (x == lvHouseData.x - 2)
                    {
                        tile.name = "Right_Ground";
                        tile.GetComponent<SpriteRenderer>().sprite = TileList.spriteToName["Shop_Ground_Right"];
                    }
                    tile.AddComponent<BoxCollider2D>().isTrigger = true;
                    tile.AddComponent<Place_Tile>();
                }
                //중간 타일
                else if ((1 < x && x < lvHouseData.x - 2))
                {
                    tile.name = "Middle_Ground";
                    tile.GetComponent<SpriteRenderer>().sprite = TileList.spriteToName["Shop_Ground_Middle"];
                    tile.AddComponent<BoxCollider2D>().isTrigger = true;
                    tile.AddComponent<Place_Tile>();
                }

                //문 위치
                if (x == lvHouseData.x / 2 && y == 0)
                {
                    
                    tile.name = "Door";
                    tile.GetComponent<SpriteRenderer>().sprite = TileList.spriteToName["Shop_Ground_Middle"];
                    tile.GetComponent<SpriteRenderer>().color = Color.green;
                    tile.AddComponent<BoxCollider2D>().isTrigger = true;
                    //tile.AddComponent<Place_Tile>();
                    tile.AddComponent<Door>();
                    doorPos = tile.transform;
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
        for (int y = 0; y < wall_height + 1; y++)
        {
            for (int x = 0; x < lvHouseData.x; x++)
            {
                GameObject tile = new GameObject("object");
                tile.AddComponent<SpriteRenderer>();

                #region =================벽==============

                //양쪽 벽
                if(x == 0 || x == lvHouseData.x - 1 && y < wall_height + 1)
                {
                    tile.name = "shop_entire_tile_Wall";
                    tile.GetComponent<SpriteRenderer>().sprite = TileList.spriteToName["shop_entire_tile_Wall"];
                    tile.AddComponent<BoxCollider2D>().isTrigger = false;
                    tile.AddComponent<Tile>();
                }
                //뒷 벽
                else if(x > 0 && x < lvHouseData.x - 1 && y < wall_height + 1)
                {
                    tile.name = "shop_entire_tile_Back";
                    tile.GetComponent<SpriteRenderer>().sprite = TileList.spriteToName["shop_entire_tile_Back"];
                    tile.AddComponent<BoxCollider2D>().isTrigger = false;
                    tile.AddComponent<Tile>();
                }

                if(y == wall_height)
                {
                    tile.name = "shop_entire_tile_Roof";
                    tile.GetComponent<SpriteRenderer>().sprite = TileList.spriteToName["shop_entire_tile_Wall"];
                    tile.AddComponent<BoxCollider2D>().isTrigger = false;
                    tile.AddComponent<Tile>();
                }
                
                #endregion

                tile.GetComponent<SpriteRenderer>().sortingLayerName = "Tile";
                tile.transform.position = new Vector2(x, y + lvHouseData.y);
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
