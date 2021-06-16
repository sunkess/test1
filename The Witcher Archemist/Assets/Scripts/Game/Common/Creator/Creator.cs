using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryKinds
{
    DECOMPOSER = 1,
    DISSOLVE = 2,
    GRINDER = 3
}

public class Creator
{
    public virtual void Hello()
    {
        Debug.Log("안농");
    }

    public Creator FindKinds(InventoryKinds kinds)
    {

        Creator c = new Creator();

        if (kinds == InventoryKinds.DECOMPOSER)
        {
            c = new Decomposition();
            //c.Hello();
        }
        else if (kinds == InventoryKinds.DISSOLVE)
        {
            c = new Fusion();
            //c.Hello();
        }
        else if (kinds == InventoryKinds.GRINDER)
        {
            c = new Comminution();
            //c.Hello();
        }

        return c;
        
    }
}
