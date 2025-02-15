using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Test : MonoBehaviour
{
    [SerializeField] float duration;
    void Start()
    {
        GetComponent<SpriteRenderer>().DOFade(0, duration);        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GetComponent<SpriteRenderer>().DOFade(0, duration);
        }
        if (Input.GetKey(KeyCode.D)) 
        {
            GetComponent<SpriteRenderer>().DOFade(1, duration);
        }
    }

}
