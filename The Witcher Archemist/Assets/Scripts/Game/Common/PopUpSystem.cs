using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Core;


public enum UIEnum
{
    UI = 0,
    TEXT = 1
}

public class PopUpSystem
{
    public Dictionary<string, GameObject> popList = new Dictionary<string, GameObject>();
    public UIManager uiManager = UIManager.instance;

    //딕셔너리를 만듭니다.
    public void SetPopList()
    {
        //GameObject[] objs = Resources.LoadAll<GameObject>("UI");
        //int length = objs.Length;
        //for (int i = 0; i < length; i++)
        //{
        //    popList.Add(objs[i].name, objs[i]);
        //}
        Methods methods = new Methods();

        popList = methods.FillStringToGameObjectDictinary("UI");
    }
    public void ShowUI(string type)
    {
        SetPopList();
        
        if(CheckList(uiManager, type))
        {
            //창을 엽니다.
            Show(type);
            Debug.Log("창을 엽니다.");
        }


    }

    public void RemoveUI(string type)
    {
        Remove(uiManager, type);
    }

    void Remove(UIManager _uiManager, string type)
    {
        Methods method = new Methods();
        List<GameObject> list = _uiManager.popList;

        for (int i = 0; i < list.Count; i++)
        {
            var value = list[i];


            if (method.CompareString(type, value.name))
            {


                UnityEngine.Object.Destroy(UIManager.instance.popList[i]);
                UIManager.instance.popList.RemoveAt(i);
            }
        }

    }

    void Show(string type)
    {

        CreatePopUp(uiManager, type);
    }

    void CreatePopUp(UIManager _uiManager,string type)
    {
        MonoBehaviour mono = new MonoBehaviour();
        List<GameObject> list = _uiManager.popList;


        if (!CheckList(_uiManager, type))
            return;

        string typeName = Convert.ToString(type);


        GameObject pop = UnityEngine.Object.Instantiate(popList[type], Vector2.zero, Quaternion.identity, _uiManager.transform);
        pop.name = popList[type].name;

        UIManager.instance.popList.Add(pop);
    }

    bool CheckList(UIManager _uiManager, string type)
    {
        Methods method = new Methods();
        List<GameObject> list = _uiManager.popList;


        for (int i = 0; i < list.Count; i++)
        { 
            
            var value = list[i];

            if (method.CompareString(type, value.name))
                return false;
        }

        return true;
    }

    public void PopupType(UIEnum popEnum, GameObject obj)
    {
        if (popEnum == UIEnum.TEXT)
        {
            UnityEngine.Object.Destroy(obj, 1);
        }
    }
}