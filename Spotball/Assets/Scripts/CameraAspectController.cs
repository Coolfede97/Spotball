using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAspectController : MonoBehaviour
{
    // Define tu relación de aspecto de diseño (por ejemplo, 16:9)
    public float designWidth = 16f;
    public float designHeight = 9f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();

        if (cam.orthographic)
        {
            // Calcula el aspect ratio del diseño y del dispositivo
            float designAspect = designWidth / designHeight;
            float deviceAspect = (float)Screen.width / Screen.height;

            // Calcula el tamaño base de la cámara según la altura del diseño
            float baseSize = designHeight / 2f;  // porque en ortográfica, la altura visible = 2 * size

            if (deviceAspect < designAspect)
            {
                // El dispositivo es más angosto que el diseño.
                // Ajustamos la cámara para que se vea el ancho completo del juego:
                // 2 * orthographicSize * deviceAspect = designWidth  => orthographicSize = designWidth / (2 * deviceAspect)
                cam.orthographicSize = designWidth / (2f * deviceAspect);
            }
            else
            {
                // El dispositivo es lo suficientemente ancho o más ancha que el diseño.
                // Mantenemos el tamaño basado en la altura del diseño.
                cam.orthographicSize = baseSize;
            }
        }
        else
        {
            Debug.LogError("La cámara debe estar en modo Orthographic para este ajuste.");
        }
    }
}
