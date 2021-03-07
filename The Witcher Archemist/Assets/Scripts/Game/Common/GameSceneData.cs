using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneData : MonoBehaviour
{
    public Dictionary<string, Sprite> tileToName = new Dictionary<string, Sprite>();
    public List<Sprite[]> arrayTiles = new List<Sprite[]>();
    public List<Sprite> tiles2 = new List<Sprite>();

    private void Start()
    {
        Set();
    }

    public void Set()
    {
        arrayTiles.Add(Resources.LoadAll<Sprite>("Tiles/shop_tile_v2"));
        arrayTiles.Add(Resources.LoadAll<Sprite>("Tiles/shop_entire_tile"));

        foreach (var tiles in arrayTiles)
        {
            foreach (var tile in tiles)
            {
                tiles2.Add(tile);
            }
        }

        foreach (var tile in tiles2)
        {
            tileToName.Add(tile.name, tile);
        }

        Debug.Log(tileToName["shop_tile_v2_0"]);
        Debug.Log(tileToName.Count);
        //for (int i = 0; i < tileToName.Count; i++)
        //{
        //    Debug.Log(tileToName[i].name);
        //}
        
    }
}
