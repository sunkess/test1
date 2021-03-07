﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            instance = null;
        }

        instance = this;
    }
}
