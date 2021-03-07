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
    public GameObject PlaceMode;
    public GameObject ChooseMode;

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
                        Set_Furniture();

                }else if (_stuff)
                {
                    Click_Stuff(_stuff);
                }
            }
        }

        Furniture_Move_Pos();

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
    }

    public void Set_Furniture()
    {
        if (!stuff.GetComponent<Stuff>())
        {
            stuff.AddComponent<Stuff>();
        }

        stuff.AddComponent<BoxCollider2D>();

        IdleMode.SetActive(true);
        PlaceMode.SetActive(false);
        ChooseMode.SetActive(false);

        stuff = null;
    }

    public void IdleMode_FurniturePlace()
    {
        GameObject _stuff = new GameObject("stuff");
        _stuff.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Furniture/Table");
        //_stuff.GetComponent<SpriteRenderer>().sortingLayerName = "Stuff";
        stuff = _stuff;

        IdleMode.SetActive(false);
        PlaceMode.SetActive(true);
    }

    public void Furniture_Move_Pos()
    {
        if (stuff)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            stuff.transform.position = FurniturePos(mousePos);
        }
    }

    Vector2 FurniturePos(Vector2 abcd)
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
        PlaceMode.SetActive(false);
    }

    public void ChooseMode_Retrieve()
    {
        Destroy(stuff);
        stuff = null;

        IdleMode.SetActive(true);
        ChooseMode.SetActive(false);
    }

    public void ChooseMode_Cancle()
    {
        Destroy(stuff);
        stuff = null;

        IdleMode.SetActive(true);
        ChooseMode.SetActive(false);
    }
}
