using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RotativeBlock : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] bool flip;
    float rotation;
    void Start()
    {
        rotation = flip ? -360 : 360;
        transform.DORotate(new Vector3(0, 0, rotation), duration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }
}
