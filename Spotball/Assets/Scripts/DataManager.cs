using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DataManager : MonoBehaviour
{

    public static DataManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    [Header("Data")]
    public int levelsWon;
    public int adsPoints;
    public int sliderSelected;

    void Start()
    {
        LoadData();        
    }

    void Update()
    {
        //GameObject.Find("TEST").GetComponent<TextMeshProUGUI>().text = levelsWon.ToString();
    }

    public void SaveData()
    {
        SaveSystem.SavePlayerData(this);
    }

    void LoadData()
    {
        PlayerData data = SaveSystem.LoadPlayerData();
        levelsWon = data.levelsWon;
        adsPoints = data.adsPoints;
        sliderSelected = data.sliderSelected;
    }

    public DataManager(int levelsWonP, int adsPointsP, int sliderSelectedP)
    {
        levelsWon = levelsWonP;
        adsPoints = adsPointsP;
        sliderSelected = sliderSelectedP;
    }
}
