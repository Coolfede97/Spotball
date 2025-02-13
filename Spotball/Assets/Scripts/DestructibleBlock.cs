using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBlock : MonoBehaviour
{
    Material material;
    [SerializeField] float fadeOutDuration;
    [SerializeField] float fadeInDuration;
    [SerializeField] private Ease fadeOutEaseType = Ease.OutCubic;
    [SerializeField] private Ease fadeInEaseType = Ease.InCubic;
    Collider2D _collider;
    void Start()
    {
        _collider = GetComponent<Collider2D>();
        material = GetComponent<SpriteRenderer>().material;
        LevelManager.onReloadPlayer += Reset;
    }
    void Update()
    {
            
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (_collider != null) _collider.enabled = false;
        material.DOFloat(1, "_FadeAmount", fadeOutDuration).SetEase(fadeOutEaseType).onComplete = ()=> 
        {
            GameObject? player = GameObject.FindGameObjectWithTag("Player");
            if (player!=null && player.GetComponent<PlayerMovement>().shot)
            {
                gameObject.SetActive(false);
            }
        };
    }
    private void Reset()
    {
        gameObject.SetActive(true);
        if (_collider != null) _collider.enabled = true;
        material.DOFloat(0, "_FadeAmount", fadeInDuration).SetEase(fadeInEaseType);
    }
    private void OnDestroy()
    {
        LevelManager.onReloadPlayer -= Reset;
    }
}
