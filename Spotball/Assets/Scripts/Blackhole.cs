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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, transform.localScale.x/2);
        foreach (Collider2D col in colliders)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                if ((col.gameObject.transform.position - transform.position).magnitude <= loseDistance)
                {
                    levelManager.ReloadPlayer();
                }
                PlayerMovement playerMovement = col.gameObject.GetComponent<PlayerMovement>();
                if (!playerMovement.shot) return;
                Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
                Vector2 direction = (transform.position - col.gameObject.transform.position).normalized;
                rb.AddForce(direction * attractionForce, ForceMode2D.Force);
            }
        }
    }

}
