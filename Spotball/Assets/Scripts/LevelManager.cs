using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    GameObject player;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject playerSpawnPos;
    public Transform spawnPoint;
    public static event Action onReloadPlayer;

    void Start()
    {
        GameManager.Instance.lastSpawnPoint = spawnPoint;
        if (GameManager.Instance.levelsManager.Count == 0)
        {
            InstantiatePlayer();
            GameManager.Instance.levelsManager.Add(this);
        }
    }
    void Update()
    {
        if (player != null && (player.transform.position-transform.position).magnitude>6)
        {
            ReloadPlayer();
        }
        if (Input.GetMouseButtonUp(0) && player.GetComponent<PlayerMovement>().shot)
        {
            ReloadPlayer();
        }
    }
    public void ReloadPlayer()
    {
        onReloadPlayer?.Invoke();
        GameManager.Instance.currentLevel -= 1f / GameManager.Instance.strikeToGainOneLevel;
        if (GameManager.Instance.currentLevel < 0) GameManager.Instance.currentLevel = 0;
        Destroy(player);
        InstantiatePlayer();
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
