using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Npc npc = collision.GetComponent<Npc>();
        if (npc)
        {
            npc.DoorOut();
        }
    }
}
