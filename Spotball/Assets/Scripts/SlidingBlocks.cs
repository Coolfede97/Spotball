using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SlidingBlocks : MonoBehaviour
{
    [SerializeField] Transform copy;
    [SerializeField] float duration;
    [SerializeField] private Ease easeType = Ease.Linear;
    void Start()
    {
        Destroy(copy.gameObject);
        transform.DOMove(copy.position,duration).SetEase(easeType).SetLoops(-1,LoopType.Yoyo);
    }

    void Update()
    {
        
    }
}
