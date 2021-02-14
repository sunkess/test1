﻿using Assets.Scripts.Game.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Contexts;
using UnityEngine;
using UnityScript.Steps;

[Serializable]
public class Store : MonoBehaviour
{
    public StoreData storeData;

    private void Start()
    {
        storeData = SaveManager.LevelLoad<StoreData>(storeData, 1);
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

        if(storeData.exp >= storeData.nextExp)
        {
            storeData = SaveManager.LevelLoad<StoreData>(storeData, Convert.ToInt32(storeData.lv) + 1);
        }

    }

}
