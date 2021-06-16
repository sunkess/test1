using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    public void Active()
    {
        Debug.Log("DISSOLVE");
        InventoryUI.instance.inventoryKinds = InventoryKinds.DISSOLVE;
        MachineUI.instance.machineKinds = "Dissolve";
    }
}
