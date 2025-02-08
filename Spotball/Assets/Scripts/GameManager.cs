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
        CreateLevelsList();
    }

    [Header("Level Generation")]
    public float currentLevel;
    public int strikeToGainOneLevel;
    [SerializeField] GameObject level1;
    [SerializeField] GameObject[] levels1;
    [SerializeField] GameObject[] levels2;
    [SerializeField] GameObject[] levels3;
    [SerializeField] GameObject[] levels4;
    [SerializeField] GameObject[] levels5;
    [SerializeField] GameObject[] levels6;
    [SerializeField] GameObject[] levels7;
    [SerializeField] GameObject[] levels8;
    [SerializeField] GameObject[] levels9;
    [SerializeField] GameObject[] levels10;

    List<GameObject[]> levels = new List<GameObject[]>();

    public List<LevelManager> levelsManager = new List<LevelManager>();
    public Transform lastSpawnPoint;
    
    [SerializeField] Transform levelsContainer;
    
    [Header("Level Transition")]
    [SerializeField] Transform cameraAim;
    [SerializeField] float transitionSpeed;
    void Start()
    {

    }

    public void ChangeLevel()
    {
        currentLevel += 1f / strikeToGainOneLevel;
        if (currentLevel > 9) currentLevel = 9;
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
        GameObject[] levelList = levels[(int)currentLevel];
        int rand = Random.Range(0, levelList.Length);
        GameObject newLevel = Instantiate(levelList[rand], lastSpawnPoint.position, Quaternion.identity, levelsContainer.transform);
    }

    public void CreateLevelsList()
    {
        levels.Add(levels1);
        levels.Add(levels2);
        levels.Add(levels3);
        levels.Add(levels4);
        levels.Add(levels5);
        levels.Add(levels6);
        levels.Add(levels7);
        levels.Add(levels8);
        levels.Add(levels9);
        levels.Add(levels10);
    }
}
