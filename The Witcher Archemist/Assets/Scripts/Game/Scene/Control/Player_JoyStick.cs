using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

public class Player_JoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Vector2 input;

    public RectTransform background;
    public RectTransform handle;

    public Player_Move player;


    public bool is_Player_Control = true; //true = 플레이어가 패널로 조작 false = AI가 자동으로 조작
    private bool is_Panel_Control = true; //true = 화면을 터치하며 움직임 false = 키보드로 조작  


    void Start()
    {
        Assert.IsNotNull(player);

        is_Panel_Control = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = RectTransformUtility.WorldToScreenPoint(null, background.position);
        Vector2 radius = background.sizeDelta / 2;
        input = (eventData.position - position) / radius;

        if(input.magnitude > 1)
        {
            input = input.normalized;
        }

        Move_Help.Set_Flip(player.GetComponent<SpriteRenderer>(), input.normalized);


        handle.anchoredPosition = input * radius;

        is_Panel_Control = true;
    }

    public void Move()
    {
        if (is_Panel_Control)
            return;

        Move_Help.Set_Flip(player.GetComponent<SpriteRenderer>() ,input.normalized);

        Vector2 radius = background.sizeDelta / 2;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical  = Input.GetAxisRaw("Vertical");

        input = new Vector2(horizontal, vertical);

        handle.anchoredPosition = input * radius;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        handle.anchoredPosition = Vector3.zero;
        input = Vector3.zero;
        is_Panel_Control = false;
    }
}
