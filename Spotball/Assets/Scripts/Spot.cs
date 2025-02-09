using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Spot : MonoBehaviour
{
    [SerializeField] float attractionForce;
    [SerializeField] float winDistance;
    [SerializeField] bool achieved;
    void Start()
    {

    }

    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !achieved)
        {
            if ((col.gameObject.transform.position - transform.position).magnitude <= winDistance)
            {
                achieved = true;
                Debug.Log("Greatness has been achieved");
                Debug.Log("UN EFECTO DEBERÍAS PASAR");
                // EFECTO ESPECIAL
                col.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                GameManager.Instance.ChangeLevel();
            }
            else 
            {
                Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
                Vector2 direction = (transform.position - col.gameObject.transform.position).normalized;
                rb.AddForce(direction * attractionForce * Time.deltaTime, ForceMode2D.Force);
            }
        }
    }
}
