using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decomposer : Machine, IActive
{
    public void Active()
    {
        Debug.Log("Decomposer");
        InventoryUI.instance.inventoryKinds = InventoryKinds.DECOMPOSER;
        MachineUI.instance.machineKinds = "Decomposer";
    }
}
