using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    public string[] popNames;
    public void OpenPopup()
    {
        ShowUIAll(popNames);
    }

    public void ClosePopup()
    {
        CloseUIAll(popNames);
    }

    //지정된 이름의 UI를 엽니다.
    void ShowUIAll(string[] popNames)
    {
        

        for (int i = 0; i < popNames.Length; i++)
        {
            PopUpSystem popup = new PopUpSystem();
            Debug.Log(popNames[i]);
            popup.ShowUI(popNames[i]);
        }
    }

    //지정된 이름의 UI를 닫습니다.
    void CloseUIAll(string[] popNames)
    {
        for (int i = 0; i < popNames.Length; i++)
        {
            PopUpSystem popup = new PopUpSystem();
            Debug.Log(popNames[i]);
            popup.RemoveUI(popNames[i]);
        }
    }
}
