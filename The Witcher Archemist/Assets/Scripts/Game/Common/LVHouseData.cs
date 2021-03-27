using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class LVHouseData
{
    public string lv;
    public int exp;
    public int nextExp;
    public int x;
    public int y;
    //public string textureKey;
    public List<TileEdit> tiles = new List<TileEdit>();
}
