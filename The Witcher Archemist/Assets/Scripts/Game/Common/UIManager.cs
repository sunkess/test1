using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    //public List<GameObject> uIList = new List<GameObject>();
    //[HideInInspector]
    public List<GameObject> popList = new List<GameObject>();

    public string mainUI;

    private void Awake()
    {
        if(instance != null)
        {
            instance = null;
            
        }

        instance = this;
    }

    private void Start()
    {
        PopUpSystem popsys = new PopUpSystem();
        popsys.ShowUI(mainUI);
    }

}
