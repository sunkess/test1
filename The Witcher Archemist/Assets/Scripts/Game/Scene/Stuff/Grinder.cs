using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grinder : Machine, IActive
{
    public void Active()
    {
        Debug.Log("Grinder");
        InventoryUI.instance.inventoryKinds = InventoryKinds.GRINDER;
        MachineUI.instance.machineKinds = "Grinder";
    }
}
