using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFromCenter : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform center;
    void Start()
    {
        
    }

    void Update()
    {
        Vector2 direction = (transform.position-center.position).normalized;
        transform.position+= (Vector3)direction*speed*Time.deltaTime;
    }
}
