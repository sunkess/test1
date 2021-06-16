using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class IngameManager : MonoBehaviour
{
    public static IngameManager instance;

    [HideInInspector]
    public GameObject stuff;

    public GameObject IdleMode;
    public GameObject placeMode;
    public GameObject ChooseMode;

    public GameObject playerUI;

    public GameObject player;

    public GameObject counter;

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
