using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public UIEnum popEnum;
    // Start is called before the first frame update
    void Start()
    {
        PopUpSystem popSys = new PopUpSystem();
        popSys.PopupType(popEnum, gameObject);
    }
}
