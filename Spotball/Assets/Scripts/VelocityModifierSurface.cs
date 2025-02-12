using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityModifierSurface : MonoBehaviour
{
    [Tooltip("Asignar número negativo si se quiere reducir la velocidad del player en tal porcentaje. Dejar en positivo si se quiere aumentar la velocidad")]
    [SerializeField] float percentageVariation;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = col.gameObject.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity += rb.velocity * percentageVariation;
        }
    }
}
