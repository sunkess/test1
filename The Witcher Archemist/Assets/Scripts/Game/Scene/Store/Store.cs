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
public class Store : MonoBehaviour
{
    public StoreData storeData;

    public GameObject tiles;

    public Text store_lv;

    public GameObject furniture;

    public GameSceneData gameSceneData;

    private void Start()
    {
        storeData = SaveManager.LevelLoad<StoreData>(storeData, 1);

        Set_StoreLv();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit)
            {
                Tile tile = hit.collider.GetComponent<Tile>();

                if (tile)
                {
                    //현재 가구를 배치하려고 할때
                    if(furniture)
                        Set_Furniture();
                }
                
            }
        }

        Furniture_Move_Pos();

    }

    void GetExp()
    {
        storeData.exp++;

        if (storeData.exp >= storeData.nextExp)
        {
            storeData = SaveManager.LevelLoad<StoreData>(storeData, Convert.ToInt32(storeData.lv) + 1);
            Set_StoreLv();
        }
    }

    void Set_StoreLv()
    {
        store_lv.text = storeData.lv;
        Set_Tile();
    }


    void Set_Tile()
    {
        DeleteTile();

        GameObject storeTile = new GameObject("StoreTile");

        Sprite[] tileGroundSprites = Resources.LoadAll<Sprite>("Tiles/shop_tile_v2");
        Sprite[] tileWall = Resources.LoadAll<Sprite>("Tiles/shop_entire_tile");

        int wall_height = 3;

        storeData.y = storeData.y + wall_height;

        for (int y = 0; y < storeData.y; y++)
        {
            
            for (int x = 0; x < storeData.x; x++)
            {
                GameObject tile = new GameObject("object");
                tile.AddComponent<SpriteRenderer>();
                #region ==============벽===================
                //================================벽=========================================

                //벽 왼쪽 위 타일, 벽 오른쪽 위 타일, 벽 왼쪽 아래 타일, 벽 오른쪽 아래 타일
                if ((x == 0 && y == 0) ||
                    (x == storeData.x - 1 && y == 0) ||
                    (x == 0 && y == storeData.y - 1) ||
                    (x == storeData.x - 1 && y == storeData.y - 1))
                {
                    //왼쪽 위 타일
                    if((x == 0 && y == 0))
                    {
                        tile.name = "Left_Top_Wall";
                        //tile.GetComponent<SpriteRenderer>().sprite = tileWall[0];
                        tile.GetComponent<SpriteRenderer>().sprite = tileWall[3];
                    }
                    //오른쪽 위 타일
                    else if((x == storeData.x - 1 && y == 0))
                    {
                        tile.name = "Right_Top_Wall";
                        //tile.GetComponent<SpriteRenderer>().sprite = tileWall[0];
                        tile.GetComponent<SpriteRenderer>().sprite = tileWall[3];
                    }
                    //왼쪽 아래 타일
                    else if ((x == 0 && y == storeData.y - 1))
                    {
                        tile.name = "Left_Bottom_Wall";
                        //tile.GetComponent<SpriteRenderer>().sprite = tileWall[2];
                        tile.GetComponent<SpriteRenderer>().sprite = tileWall[3];

                    }
                    //오른쪽 아래 타일
                    else if ((x == storeData.x - 1 && y == storeData.y - 1))
                    {
                        tile.name = "Right_Bottom_Wall";
                        //tile.GetComponent<SpriteRenderer>().sprite = tileWall[2];
                        tile.GetComponent<SpriteRenderer>().sprite = tileWall[3];

                    }
                    tile.AddComponent<BoxCollider2D>().isTrigger = false;

                }
                //
                else if(0 < x && x < storeData.x - 1 && y < wall_height)
                {
                    tile.name = "Middle_Wall";
                    //tile.GetComponent<SpriteRenderer>().sprite = tileWall[3];
                    tile.GetComponent<SpriteRenderer>().sprite = tileWall[1];
                    tile.AddComponent<BoxCollider2D>().isTrigger = false;
                }
                //벽 왼쪽 타일, 벽 오른쪽 타일
                else if ((x == 0 && y > 0) || (x == storeData.x - 1) && y < storeData.y)
                {
                    if((x == 0 && y > 0))
                    {
                        tile.name = "Left_Middle_Wall";
                        //tile.GetComponent<SpriteRenderer>().sprite = tileWall[1];
                        tile.GetComponent<SpriteRenderer>().sprite = tileWall[3];
                        tile.AddComponent<BoxCollider2D>().isTrigger = false;
                    }
                    else if((x == storeData.x - 1) && y < storeData.y)
                    {
                        tile.name = "Right_Middle_Wall";
                        //tile.GetComponent<SpriteRenderer>().sprite = tileWall[1];
                        tile.GetComponent<SpriteRenderer>().sprite = tileWall[3];
                        tile.AddComponent<BoxCollider2D>().isTrigger = false;
                    }
                    
                }
                //================================벽 끝=========================================
                #endregion
                #region ==============바닥=================
                //================================바닥 타일====================================
                //왼쪽 타일과 오른쪽 타일
                if (x == 1 || x == storeData.x - 2)
                {
                    if (x == 1 && y >= wall_height)
                    {
                        tile.name = "Left_Ground";
                        tile.GetComponent<SpriteRenderer>().sprite = tileGroundSprites[0];
                    }
                    else if (x == storeData.x - 2 && y >= wall_height)
                    {
                        tile.name = "Right_Ground";
                        tile.GetComponent<SpriteRenderer>().sprite = tileGroundSprites[2];
                    }
                    tile.AddComponent<BoxCollider2D>().isTrigger = true;
                    tile.AddComponent<Tile>();
                }
                //중간 타일
                else if ((1 < x && x < storeData.x - 2) && y >= wall_height)
                {
                    tile.name = "Middle_Ground";
                    tile.GetComponent<SpriteRenderer>().sprite = tileGroundSprites[1];
                    tile.AddComponent<BoxCollider2D>().isTrigger = true;
                    tile.AddComponent<Tile>();
                }


                //================================바닥 타일 끝=================================
                #endregion


                tile.GetComponent<SpriteRenderer>().sortingLayerName = "Tile";
                tile.transform.position = new Vector2(x, -y);
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

    public void Set_Furniture()
    {
        furniture.AddComponent<Stuff>();
        furniture.AddComponent<BoxCollider2D>();
        furniture = null;
    }

    public void Create_Furniture()
    {
        if (furniture)
            Destroy(furniture);

        GameObject _furniture = new GameObject("Furniture");
        _furniture.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Furniture/Table");
        furniture = _furniture;
    }

    public void Furniture_Move_Pos()
    {
        if (furniture)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            furniture.transform.position = test(mousePos);
        }
    }

    Vector2 test(Vector2 abcd)
    {
        float x = (float)Math.Round(abcd.x);
        float y = (float)Math.Round(abcd.y);

        return new Vector2(x, y);
    }

}
