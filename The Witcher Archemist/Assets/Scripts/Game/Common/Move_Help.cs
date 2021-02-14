using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Help
{
    public static void sortingSet(SpriteRenderer sr, Transform transform, float sort)
    {
        sr.sortingOrder = (int)(-sort * 10);
    }

    public static  void Set_Flip(SpriteRenderer sr,Vector2 normalized)
    {
        if (normalized.x >= 0.1f)
        {
            sr.flipX = true;
        }
        else if (normalized.x <= -0.1f)
        {
            sr.flipX = false;
        }
    }
}
