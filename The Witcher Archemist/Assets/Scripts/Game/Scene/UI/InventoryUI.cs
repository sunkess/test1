using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance;
    public InventoryKinds inventoryKinds;

    private void Awake()
    {
        Debug.Log(instance);
        instance = this;
    }
}
