using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stuff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Move_Help.sortingSet(GetComponent<SpriteRenderer>(), transform, transform.position.y);
    }
}
