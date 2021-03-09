using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class IngameManager : MonoBehaviour
{
    public static IngameManager instance;

    [HideInInspector]
    public GameObject stuff;

    public GameObject IdleMode;
    public GameObject placeMode;
    public GameObject ChooseMode;

    public GameObject playerUI;

    public GameObject player;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            instance = null;
        }

        instance = this;
    }

    private void Update()
    {
        if (stuff)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit)
            {
                Place_Tile place_tile = hit.collider.GetComponent<Place_Tile>();

                if (place_tile)
                {
                    //현재 가구를 배치하려고 할때
                    if (stuff)
                        Show_Stuff(true);
                }
                else
                {
                    if (stuff)
                        Show_Stuff(false);
                }
            }
            else
            {
                if (stuff)
                    Show_Stuff(false);

            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit)
            {
                Place_Tile place_Tile = hit.collider.GetComponent<Place_Tile>();
                Stuff _stuff = hit.collider.GetComponent<Stuff>();

                if (place_Tile)
                {
                    //현재 가구를 배치하려고 할때
                    if (stuff)
                        Set_Stuff();

                }else if (_stuff)
                {
                    Click_Stuff(_stuff);
                }
            }
        }

        Stuff_Move_Pos();

    }

    public void Show_Stuff(bool isShow)
    {
        stuff.SetActive(isShow);
    }

    public void Click_Stuff(Stuff _stuff)
    {
        Destroy(_stuff.GetComponent<BoxCollider2D>());
        stuff = _stuff.gameObject;

        IdleMode.SetActive(false);
        ChooseMode.SetActive(true);

        playerUI.SetActive(false);
    }

    public void Set_Stuff()
    {
        if (!stuff.GetComponent<Stuff>())
        {
            stuff.AddComponent<Stuff>();
        }

        stuff.AddComponent<BoxCollider2D>();

        IdleMode.SetActive(true);
        placeMode.SetActive(false);
        ChooseMode.SetActive(false);

        playerUI.SetActive(true);

        stuff = null;
    }

    public void IdleMode_PlaceButton()
    {
        GameObject _stuff = new GameObject("stuff");
        _stuff.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Furniture/Table");
        //_stuff.GetComponent<SpriteRenderer>().sortingLayerName = "Stuff";
        stuff = _stuff;

        IdleMode.SetActive(false);
        placeMode.SetActive(true);

        playerUI.SetActive(false);
    }

    public void Stuff_Move_Pos()
    {
        if (stuff)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            stuff.transform.position = StuffPos(mousePos);
        }
    }

    Vector2 StuffPos(Vector2 abcd)
    {
        float x = (float)Math.Round(abcd.x);
        float y = (float)Math.Round(abcd.y);

        return new Vector2(x, y);
    }

    public void PlaceMode_Cancle()
    {
        Destroy(stuff);
        stuff = null;

        IdleMode.SetActive(true);
        placeMode.SetActive(false);

        playerUI.SetActive(true);
    }

    public void ChooseMode_Retrieve()
    {
        Destroy(stuff);
        stuff = null;

        IdleMode.SetActive(true);
        ChooseMode.SetActive(false);

        playerUI.SetActive(true);
    }

    public void ChooseMode_Cancle()
    {
        Destroy(stuff);
        stuff = null;

        IdleMode.SetActive(true);
        ChooseMode.SetActive(false);

        playerUI.SetActive(true);
    }
}
