using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField] Transform pointer;
    Vector2 direciton;
    [SerializeField] float speed;
    void Start()
    {
        direciton = (pointer.position-transform.position).normalized;
    }

    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && col.gameObject.GetComponent<PlayerMovement>().shot)
        {
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(direciton*speed*Time.deltaTime, ForceMode2D.Force);
        }
    }
}
