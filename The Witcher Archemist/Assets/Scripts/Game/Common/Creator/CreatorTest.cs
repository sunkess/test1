using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatorTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Creator c = new Creator();
        c = c.FindKinds(InventoryKinds.DISSOLVE);
        c.Hello();
    }
}
