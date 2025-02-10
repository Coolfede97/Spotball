using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBlock : MonoBehaviour
{
    void Start()
    {
        LevelManager.onReloadPlayer += Reset;     
    }
    void Update()
    {
            
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        gameObject.SetActive(false);
    }
    private void Reset()
    {
        gameObject.SetActive(true);
    }
    private void OnDestroy()
    {
        LevelManager.onReloadPlayer -= Reset;
    }
}
