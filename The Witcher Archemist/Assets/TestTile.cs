using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTile : MonoBehaviour
{
    public Pos pos;
    public int count = 0;
}

public struct Pos
{
    
    public int x;
    public int y;

    public Pos(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
}
