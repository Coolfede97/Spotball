using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingSize : MonoBehaviour
{
    [SerializeField] float growSpeed;
    void Start()
    {
        
    }

    void Update()
    {
        transform.localScale += new Vector3(growSpeed * Time.deltaTime, growSpeed * Time.deltaTime);
    }
}
