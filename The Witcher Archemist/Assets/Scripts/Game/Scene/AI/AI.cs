using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public Transform[] target_Position;
    public Transform target;

    
    private int target_Count = 0;

    protected float last_y;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        target = target_Position[target_Count];
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        AIMove();
    }

    protected void AIMove()
    {
        Vector2 dir = target_Position[target_Count].position - transform.position;
        float distance = dir.magnitude;


        if (distance >= 1)
        {
            GetComponent<Rigidbody2D>().velocity = dir.normalized * 2;
            //Move_Help.Set_Flip(GetComponent<SpriteRenderer>(), dir.normalized);
        }
        else
        {
            target_Count++;
            if (target_Count > target_Position.Length - 1)
            {
                target_Count = 0;
            }
        }

        if (transform.position.y != last_y)
        {
            Move_Help.sortingSet(GetComponent<SpriteRenderer>(), transform, (int)transform.position.y);
        }


        last_y = transform.position.y;
    }
    
}
