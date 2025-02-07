using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    [Header("Level Generation")]
    int currentLevel;
    public List<LevelManager> levelsManager = new List<LevelManager>();
    public Transform lastSpawnPoint;
    [SerializeField] GameObject level1;
    [SerializeField] Transform levelsContainer;
    [Header("Level Transition")]
    [SerializeField] Transform cameraAim;
    [SerializeField] float transitionSpeed;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ChangeLevel()
    {
        currentLevel++;
        CreateNewLevel();
        GameObject levelToDestroy = levelsManager[0].gameObject;
        levelsManager.RemoveAt(0);
        cameraAim.DOMove(levelsManager[0].transform.position+Vector3.back, transitionSpeed).onComplete = ()=> 
        {
            Destroy(levelToDestroy);
            levelsManager[0].InstantiatePlayer();
        };
    }

    public void CreateNewLevel()
    {
        Debug.Log("DKWODW");
        Debug.Log(lastSpawnPoint.position);
        GameObject newLevel = Instantiate(level1, lastSpawnPoint.position, Quaternion.identity, levelsContainer.transform);
        Debug.Log(newLevel.transform.position);
    }
}
