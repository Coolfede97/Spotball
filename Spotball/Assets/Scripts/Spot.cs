using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Spot : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }   

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Greatness has been achieved");
            Debug.Log("UN EFECTO DEBERÍAS PASAR");
            // EFECTO ESPECIAL
            col.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
        GameManager.Instance.ChangeLevel();
    }
}
