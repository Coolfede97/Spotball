using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    GameObject player;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject playerSpawnPos;
    public Transform spawnPoint; 

    void Start()
    {
        GameManager.Instance.lastSpawnPoint = spawnPoint;
        if (GameManager.Instance.levelsManager.Count == 0)
        {
            GameManager.Instance.CreateNewLevel();
            InstantiatePlayer();
        }
        GameManager.Instance.levelsManager.Add(this);
    }
    void Update()
    {
        if (player != null && (player.transform.position-transform.position).magnitude>6)
        {
            Destroy(player);
            InstantiatePlayer();
        }
    }

    //private void OnTriggerExit2D(Collider2D col)
    //{
    //    if (col.gameObject.CompareTag("Player") && this == GameManager.Instance.levelsManager[0])
    //    {
    //        Destroy(player);
    //        InstantiatePlayer();
    //    }
    //}
    public void InstantiatePlayer()
    {
        player = Instantiate(playerPrefab, playerSpawnPos.transform.position, Quaternion.identity, transform);
    }
}
