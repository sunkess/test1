using Assets.Scripts.Game.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Contexts;
using UnityEngine;
using UnityEngine.UI;
using UnityScript.Steps;

[Serializable]
public class Store : MonoBehaviour
{
    public StoreData storeData;

    public Text store_lv;

    private void Start()
    {
        storeData = SaveManager.LevelLoad<StoreData>(storeData, 1);

        Set_StoreLv();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GetExp();
        }
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
        GameObject tileParent = new GameObject("tileParent");

        for (int y = 0; y < storeData.y; y++)
        {
            for (int x = 0; x < storeData.x; x++)
            {
                GameObject tile = new GameObject("tile");
                tile.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Tiles/Square");
                tile.GetComponent<SpriteRenderer>().sortingLayerName = "Tile";
                tile.transform.position = new Vector2(x, -y);
                tile.transform.parent = tileParent.transform;
                tileParent.transform.parent = transform;
            }
        }
    }

}
