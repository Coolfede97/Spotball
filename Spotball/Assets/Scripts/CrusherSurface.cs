using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CrusherSurface : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;
    [SerializeField] float extraRayLength=-0.1f;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            float playerLength = col.gameObject.transform.localScale.x; // Asumiendo que el jugador es perfectamente circular
            Ray2D ray = new Ray2D();
            ContactPoint2D contactPoint = col.GetContact(0);
            ray.origin = contactPoint.point;
            ray.direction = contactPoint.normal*-1;
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, playerLength + extraRayLength);
            foreach (RaycastHit2D hit in hits ) 
            {
                if (!hit.collider.isTrigger && hit.collider.gameObject != gameObject && hit.collider != col.collider)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    levelManager.ReloadPlayer();
                }
            }
        }
    }
}
