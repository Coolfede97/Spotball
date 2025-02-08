using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotativeBlock : MonoBehaviour
{
    [SerializeField] float speed;
    void Start()
    {
        
    }

    void Update()
    {
        transform.rotation = new Quaternion(transform.rotation.x,transform.rotation.y,transform.rotation.z+Time.deltaTime*speed,transform.rotation.w);
    }
}
