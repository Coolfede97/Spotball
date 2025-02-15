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
    [SerializeField] GameObject testLevel;
    [SerializeField] GameObject[] levels1;
    [SerializeField] GameObject[] levels2;
    [SerializeField] GameObject[] levels3;
    [SerializeField] GameObject[] levels4;
    [SerializeField] GameObject[] levels5;

    List<GameObject[]> levels = new List<GameObject[]>();

    public List<LevelManager> levelsManager = new List<LevelManager>();
    public Transform lastSpawnPoint;
    
    [SerializeField] Transform levelsContainer;
    [SerializeField] GameObject blockLevel;
    [Header("Level Transition")]
    [SerializeField] Transform cameraAim;
    [SerializeField] float transitionSpeed;
    [SerializeField] float transitionDelay;
    [SerializeField] private Ease cameraEaseType = Ease.Linear;

    
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        
    }
    public IEnumerator ChangeLevel()
    {
        DataManager.Instance.levelsWon++;
        DataManager.Instance.SaveData();
        currentLevel += 1f / strikeToGainOneLevel;
        if (currentLevel > levels.Count-1) currentLevel = levels.Count-1;
        yield return new WaitForSecondsRealtime(transitionDelay);
        GameObject newLevel = CreateNewLevel();
        levelsManager.Add(newLevel.GetComponent<LevelManager>());
        GameObject newBlockLevel = Instantiate(blockLevel, lastSpawnPoint.position, Quaternion.identity);
        cameraAim.DOMove(levelsManager[1].transform.position+Vector3.back, transitionSpeed).SetEase(cameraEaseType).onComplete = ()=> 
        {
            Destroy(newBlockLevel);
            Destroy(levelsManager[0].gameObject);
            levelsManager.RemoveAt(0);

            levelsManager[0].InstantiatePlayer();
        };
    }

    public GameObject CreateNewLevel()
    {
        GameObject[] levelList = levels[(int)currentLevel];
        int rand = Random.Range(0, levelList.Length);
        if (testLevel!=null)
        {
            return Instantiate(testLevel, lastSpawnPoint.position+Vector3.up*10, Quaternion.identity, levelsContainer.transform);
        }
        else
        {
            return Instantiate(levelList[rand], lastSpawnPoint.position + Vector3.up * 10, Quaternion.identity, levelsContainer.transform);            
        }
    }

    public void CreateLevelsList()
    {
        levels.Add(levels1);
        levels.Add(levels2);
        levels.Add(levels3);
        levels.Add(levels4);
        levels.Add(levels5);
    }
}
