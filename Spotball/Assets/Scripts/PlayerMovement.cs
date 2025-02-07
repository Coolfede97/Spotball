using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [Header("SHOOT ##############")]
    [SerializeField] bool shot;
    [SerializeField] float speed;
    [SerializeField] float maxDistanceMultiplier;
    [SerializeField] float distanceSpeedMultiplier;
    Vector2 clickDownPos;
    Vector2 mousePos;
    [Header("AIM RENDERER ############")]
    [SerializeField] LineRenderer aimLineRenderer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && !shot) clickDownPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonUp(0) && !shot) 
        {
            shot = true;
            // Multiplier va a hacer un número entre 0.5 y 1.5 que se obtiene por la magnitud clampeada del vector igual a la diferencia entre clickDownPos y mousePos
            float multiplier = Mathf.Clamp(GF.ClampVector(clickDownPos - mousePos, 0, maxDistanceMultiplier).magnitude, 0.5f, maxDistanceMultiplier) ;
            float force = speed * multiplier * distanceSpeedMultiplier;
            rb.AddForce((clickDownPos-mousePos).normalized*force, ForceMode2D.Impulse);
        }

        if (Input.GetMouseButton(0) && !shot)
        {
            aimLineRenderer.positionCount = 2;
            aimLineRenderer.SetPosition(0, transform.position);
            aimLineRenderer.SetPosition(1, (Vector2)transform.position + GF.ClampVector(clickDownPos-mousePos, 0, maxDistanceMultiplier));
        }
        else aimLineRenderer.positionCount = 0;
    }
}
