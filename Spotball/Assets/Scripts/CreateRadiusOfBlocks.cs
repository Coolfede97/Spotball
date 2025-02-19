using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreateRadiusOfBlocks : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] int numberOfChildren;
    [SerializeField] float radius;
    [SerializeField] float angleOffset;
    [SerializeField] Transform levelManager;
    List<GameObject> blocksCreated = new List<GameObject>();
    void Start()
    {
        float angleStep = 360f / numberOfChildren;
        for (int i = 0; i < numberOfChildren; i++) 
        {
            float angle = i* angleStep+angleOffset;
            Vector2 spawnPos = (Vector2)transform.position + new Vector2(Mathf.Cos(angle*Mathf.Deg2Rad) * radius, Mathf.Sin(angle * Mathf.Deg2Rad) * radius);
            blocksCreated.Add(Instantiate(prefab, spawnPos, Quaternion.identity, transform));
        }

    }
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(levelManager.position, GameManager.Instance.levelSize, 0);
        List<GameObject> collidersList = new List<GameObject>();
        foreach (Collider2D collider in colliders) collidersList.Add(collider.gameObject);
        foreach (GameObject block in blocksCreated)
        {
            if (collidersList.Contains(block)) block.SetActive(true);
            else block.SetActive(false);
        }
    }
}
