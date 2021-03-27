﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            instance = null;
        }

        instance = this;
        if (TileList.isEmptyImageList())
        {
            TileList.SetSpriteList();
        }
        
    }
    private void Start()
    {
        
    }
}
