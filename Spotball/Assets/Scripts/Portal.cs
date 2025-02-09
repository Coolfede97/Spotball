using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Portal otherPortal;
    [SerializeField] string[] transferableTags;
    public List<GameObject> translatedObject = new List<GameObject>();
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (transferableTags.Contains(col.gameObject.tag) && !translatedObject.Contains(col.gameObject))
        {
            col.gameObject.transform.position = otherPortal.transform.position;
            otherPortal.translatedObject.Add(col.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (transferableTags.Contains(col.gameObject.tag) && translatedObject.Contains(col.gameObject))
        {
            translatedObject.Remove(col.gameObject);
        }
    }
}
