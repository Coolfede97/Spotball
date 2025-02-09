using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : MonoBehaviour
{
    [SerializeField] float attractionForce;
    [SerializeField] float loseDistance;
    [SerializeField] LevelManager levelManager;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if ((col.gameObject.transform.position-transform.position).magnitude<=loseDistance)
            {
                levelManager.ReloadPlayer();
            }
            PlayerMovement playerMovement = col.gameObject.GetComponent<PlayerMovement>();
            if (!playerMovement.shot) return;
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            Vector2 direction = (transform.position-col.gameObject.transform.position).normalized;
            rb.AddForce(direction * attractionForce * Time.deltaTime,ForceMode2D.Force);
        }
    }
}
