using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Wind : MonoBehaviour
{
    [SerializeField] Transform pointer;
    Vector2 direciton;
    [SerializeField] float speed;
    [SerializeField] Transform particles;
    [SerializeField] ParticleSystem ps;
    [SerializeField] float particlesSpeedMultiplier;
    void Start()
    {
        direciton = (pointer.position-transform.position).normalized;
        var velocityOverLifeTime = ps.velocityOverLifetime;
        velocityOverLifeTime.enabled = true;
        velocityOverLifeTime.space = ParticleSystemSimulationSpace.World;
        velocityOverLifeTime.x = new ParticleSystem.MinMaxCurve(direciton.x*speed*particlesSpeedMultiplier);
        velocityOverLifeTime.y = new ParticleSystem.MinMaxCurve(direciton.y * speed*particlesSpeedMultiplier);
        velocityOverLifeTime.z = 0f;
        var shape = ps.shape;
        shape.enabled = true;
        shape.scale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && col.gameObject.GetComponent<PlayerMovement>().shot)
        {
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(direciton*speed, ForceMode2D.Force);
        }
    }
}
