using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreateRadiusOfBlocks : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject prefabToDestroy;
    [SerializeField] int numberOfChildren;
    [SerializeField] float radius;
    [SerializeField] float angleOffset;
    [SerializeField] Transform levelManager;
    List<GameObject> blocksCreated = new List<GameObject>();
    [SerializeField] bool culling = true;
    void Start()
    {
        float angleStep = 360f / numberOfChildren;
        for (int i = 0; i < numberOfChildren; i++) 
        {
            float angle = i* angleStep+angleOffset;
            Vector2 spawnPos = (Vector2)transform.position + new Vector2(Mathf.Cos(angle*Mathf.Deg2Rad) * radius, Mathf.Sin(angle * Mathf.Deg2Rad) * radius);
            blocksCreated.Add(Instantiate(prefab, spawnPos, Quaternion.identity, transform));
        }
        Destroy(prefabToDestroy);

    }
    void Update()
    {
        if (!culling) return;
        foreach (GameObject block in blocksCreated)
        {
            Transform blockTrans = block.transform;
            float radius = blockTrans.localScale.x > blockTrans.localScale.y ? blockTrans.localScale.x + 1 : blockTrans.localScale.y + 1;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(block.transform.position, radius);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.transform == levelManager)
                {
                    block.SetActive(true);
                    break;
                }
                else block.SetActive(false);
            }
        }
    }
}
