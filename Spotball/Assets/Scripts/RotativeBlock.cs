using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotativeBlock : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] bool flip;
    void Start()
    {
        if (flip) speed*=-1;
    }

    void Update()
    {
        Vector3 rotation = new Vector3(0, 0, Time.deltaTime * speed);
        transform.Rotate(rotation);
    }
}
