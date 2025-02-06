using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GF
{
    public static Vector2 ClampVector(Vector2 vector,float min, float max)
    {
        Vector2 direction = vector.normalized;
        if (vector.magnitude > max) vector = direction * max;
        else if (vector.magnitude < min) vector = direction * min;
        return vector;
    }
}
