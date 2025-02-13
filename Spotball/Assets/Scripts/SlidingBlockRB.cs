using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SlidingBlockRB : MonoBehaviour
{
    [SerializeField] Transform copy;
    Vector3 copyPos;
    Vector2 originalPos;
    [SerializeField] float duration;
    [SerializeField] float delay;
    Rigidbody2D rb;
    Vector2 velocityToApply;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        copyPos = copy.position;
        Destroy(copy.gameObject);
        originalPos = transform.position;
        velocityToApply = ((Vector2)copyPos- originalPos) /duration;
        StartCoroutine(Slide());
    }

    private void OnEnable()
    {
        StartCoroutine(Slide());
    }

    IEnumerator Slide()
    {
        yield return new WaitForSeconds(delay);
        while(true)
        {
            transform.position = originalPos;
            rb.velocity = velocityToApply;
            yield return new WaitForSecondsRealtime(duration);
            rb.velocity = -velocityToApply;
            yield return new WaitForSecondsRealtime(duration);
        }
    }
}
