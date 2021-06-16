using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Core;

//public enum MachineKinds
//{
//    DECOMPOSER = 1,
//    DISSOLVE = 2,
//    GRINDER = 3
//}

public class MachineUI : MonoBehaviour
{
    public static MachineUI instance;
    public string machineKinds;
    public Dictionary<string, GameObject> machineList = new Dictionary<string, GameObject>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GetMachineUI();
    }

    void GetMachineUI()
    {
        Methods methods = new Methods();
        machineList = methods.FillStringToGameObjectDictinary("UI/MachineUI");
        GameObject[] objs = Resources.LoadAll<GameObject>("UI/MachineUI");

        GameObject loadObj = null;

        for (int i = 0; i < objs.Length; i++)
        {
            if(objs[i].name == machineKinds)
            {
                loadObj = objs[i];
            }
        }

        GameObject obj = Instantiate(loadObj, transform.position, Quaternion.identity, transform);
        obj.name = loadObj.name;

    }
}
