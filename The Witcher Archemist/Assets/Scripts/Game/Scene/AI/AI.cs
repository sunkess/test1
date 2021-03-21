using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AI : MonoBehaviour
{
    public Transform[] target_Position;
    private Transform target;

    protected int targetNum = 0;

    protected float last_y;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        target_Position[0] = IngameManager.instance.counter.transform.GetChild(0);
        target = target_Position[0];
        target_Position[1] = House.instance.doorPos;
        targetNum = 0;
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        AIMove();
    }

    protected virtual void AIMove()
    {
        Vector2 dir = target_Position[targetNum].position - transform.position;
        float distance = dir.magnitude;

        if(distance > 0.1f)
        {
            GetComponent<Rigidbody2D>().velocity = dir.normalized * 2;
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }


        if (transform.position.y != last_y)
        {
            Move_Help.sortingSet(GetComponent<SpriteRenderer>(), transform, (int)transform.position.y);
        }


        last_y = transform.position.y;
    }
    
}
