using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : AI
{
    [SerializeField]private float original_speed = 0.02f;

    public Player_JoyStick joystick;
    public GameObject playerUI;

    public enum MoveType
    {
        Transform,
        Rigidbody
    }

    public MoveType movetype;

    protected override void Start()
    {
        //IngameManager.instance.playerUI = playerUI;
    }
    // Update is called once per frame
    protected override void FixedUpdate()
    {
        if (joystick.is_Player_Control)
        {
            joystick.Move();
            //움직임을 주는 형태
            if (movetype == MoveType.Transform)
            {
                transform.position += ((Vector3)joystick.input * original_speed);
            }
            else if (movetype == MoveType.Rigidbody)
            {
                GetComponent<Rigidbody2D>().velocity = joystick.input;
            }

            if (transform.position.y != last_y)
            {
                //보이는 순서를 정해줍니다.
                Move_Help.sortingSet(GetComponent<SpriteRenderer>(), transform, transform.position.y);
            }

            last_y = transform.position.y;
        }
        else
        {
            base.FixedUpdate();
        }
        
        

    }
}
