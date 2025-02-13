using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using AllIn1SpriteShader;
public class Spot : MonoBehaviour
{
    [SerializeField] float attractionForce;
    float currentAttractionForce;
    [SerializeField] float increasingForce;
    [SerializeField] float winDistance;
    [SerializeField] bool achieved;
    [SerializeField] ParticleSystem winParticles;
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
            currentAttractionForce = attractionForce;
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !achieved)
        {
            if ((col.gameObject.transform.position - transform.position).magnitude <= winDistance)
            {
                achieved = true;
                Destroy(col.gameObject);
                Instantiate(winParticles,transform.position,Quaternion.identity,transform);
                StartCoroutine(GameManager.Instance.ChangeLevel());
            }
            else 
            {
                currentAttractionForce += Time.deltaTime * increasingForce;
                Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
                Vector2 direction = (transform.position - col.gameObject.transform.position).normalized;
                rb.AddForce(direction * currentAttractionForce, ForceMode2D.Force);
            }
        }
    }
}
