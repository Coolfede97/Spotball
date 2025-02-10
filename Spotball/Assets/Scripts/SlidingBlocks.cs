using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SlidingBlocks : MonoBehaviour
{
    [SerializeField] Transform copy;
    Vector3 copyPos;
    [SerializeField] float duration;
    [SerializeField] float delay;
    [SerializeField] private Ease easeType = Ease.Linear;
    void Start()
    {
        copyPos = copy.position;
        Destroy(copy.gameObject);
        Invoke("StartSliding", delay);
    }
    void StartSliding()
    {
        transform.DOMove(copyPos, duration).SetEase(easeType).SetLoops(-1, LoopType.Yoyo);
    }
    void Update()
    {
        
    }
}
