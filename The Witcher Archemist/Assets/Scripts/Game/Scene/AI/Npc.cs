using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Npc : AI
{
    public GameObject ui;
    public Text npcText;
    public bool isOut;
    protected override void FixedUpdate()
    {
        AIMove();
    }

    protected override void AIMove()
    {
        Vector2 dir = target_Position[targetNum].position - transform.position;
        float distance = dir.magnitude;

        if (distance > 0.1f)
        {
            GetComponent<Rigidbody2D>().velocity = dir.normalized * 2;
        }
        else
        {
            ui.SetActive(true);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }


        if (transform.position.y != last_y)
        {
            Move_Help.sortingSet(GetComponent<SpriteRenderer>(), transform, (int)transform.position.y);
        }


        last_y = transform.position.y;
    }

    void NextTarget()
    {
        targetNum++;
    }

    void ChangeText()
    {
        npcText.text = "좋아";
    }

    void SendMoney()
    {
        House.instance.housedata.money += 100;
    }

    //NPC의 목적달성
    void AchieveTheGoal()
    {
        NextTarget();
        ChangeText();
        SendMoney();
        isOut = true;
    }

    public void DoorOut()
    {
        if(isOut)
            Destroy(gameObject, 2);
    }

    public void NpcButton()
    {
        if(!isOut)
            AchieveTheGoal();
    }
}
