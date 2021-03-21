using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sizetest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GetComponent<SpriteRenderer>().bounds.size);
    }
}
