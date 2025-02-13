using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RotativeBlockRB : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] bool flip;
    Rigidbody2D rb;
    float rotationSpeed;
    Quaternion initialRotation;
    void Start()
    {
        initialRotation = transform.rotation;
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;

        rotationSpeed = flip ? -360 / duration : 360 / duration;
        rb.angularVelocity = rotationSpeed;
    }
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        rotationSpeed = flip ? -360 / duration : 360 / duration;
        transform.rotation = initialRotation;
        rb.angularVelocity = rotationSpeed;
    }
}
