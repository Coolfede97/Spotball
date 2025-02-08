using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAspectController : MonoBehaviour
{
    public float designWidth = 16f;
    public float designHeight = 9f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();

        if (cam.orthographic)
        {
            float designAspect = designWidth / designHeight;
            float deviceAspect = (float)Screen.width / Screen.height;

            float baseSize = designHeight / 2f;  

            if (deviceAspect < designAspect)
            {
                cam.orthographicSize = designWidth / (2f * deviceAspect);
            }
            else
            {
                cam.orthographicSize = baseSize;
            }
        }
        else
        {
            Debug.LogError("La cámara debe estar en modo Orthographic para este ajuste.");
        }
    }
    private void Update()
    {
        
    }
}
