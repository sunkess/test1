using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TileEdit
{
    public int tileNum; // 0 = 설치되는 타일, 1 = 설치되지 않는 타일, 2 = NPC가 나오는 문 타일, 3 = 연금술 방 들어가는 타일
}

public class TileColor
{
    public static Color[] colorArray = { Color.white, Color.red, Color.green, Color.blue };
}