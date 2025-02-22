using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTrailRenderer : MonoBehaviour
{
    [SerializeField] TrailRenderer trail;
    [SerializeField] float baseScale = 1f;
    float lastSize;
    void Start()
    {
        lastSize = transform.localScale.x;
    }

    void Update()
    {
        float scaleFactor  = transform.localScale.x / lastSize;

        AnimationCurve originalCurve = trail.widthCurve;
        AnimationCurve newCurve = new AnimationCurve();

        for (int i = 0; i < originalCurve.keys.Length; i++) 
        {
            Keyframe key = originalCurve.keys[i];
            key.value *= scaleFactor;
            newCurve.AddKey(key);
        }

        trail.widthCurve = newCurve;
        lastSize = transform.localScale.x;
    }
}
