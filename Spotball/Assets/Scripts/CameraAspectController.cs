using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAspectController : MonoBehaviour
{
    // Define tu relaci�n de aspecto de dise�o (por ejemplo, 16:9)
    public float designWidth = 16f;
    public float designHeight = 9f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();

        if (cam.orthographic)
        {
            // Calcula el aspect ratio del dise�o y del dispositivo
            float designAspect = designWidth / designHeight;
            float deviceAspect = (float)Screen.width / Screen.height;

            // Calcula el tama�o base de la c�mara seg�n la altura del dise�o
            float baseSize = designHeight / 2f;  // porque en ortogr�fica, la altura visible = 2 * size

            if (deviceAspect < designAspect)
            {
                // El dispositivo es m�s angosto que el dise�o.
                // Ajustamos la c�mara para que se vea el ancho completo del juego:
                // 2 * orthographicSize * deviceAspect = designWidth  => orthographicSize = designWidth / (2 * deviceAspect)
                cam.orthographicSize = designWidth / (2f * deviceAspect);
            }
            else
            {
                // El dispositivo es lo suficientemente ancho o m�s ancha que el dise�o.
                // Mantenemos el tama�o basado en la altura del dise�o.
                cam.orthographicSize = baseSize;
            }
        }
        else
        {
            Debug.LogError("La c�mara debe estar en modo Orthographic para este ajuste.");
        }
    }
}
