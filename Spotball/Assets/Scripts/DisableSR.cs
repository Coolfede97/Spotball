using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSR : MonoBehaviour
{

    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;    
    }
}
