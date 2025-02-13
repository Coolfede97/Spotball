using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    GameObject player;
    ParticleSystem winParticles;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject playerSpawnPos;
    public Transform spawnPoint;
    bool levelFailed;
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
        if (!levelFailed)
        {
            GameManager.Instance.currentLevel -= 1f / GameManager.Instance.strikeToGainOneLevel;
            if (GameManager.Instance.currentLevel < 0) GameManager.Instance.currentLevel = 0;
            levelFailed = true;
        }
        Instantiate(player.GetComponent<PlayerMovement>().deathParticles, player.transform.position, Quaternion.identity, transform);
        Destroy(player);
        InstantiatePlayer();
    }
    
    public void InstantiatePlayer()
    {
        player = Instantiate(playerPrefab, playerSpawnPos.transform.position, Quaternion.identity, transform);
    }
}
