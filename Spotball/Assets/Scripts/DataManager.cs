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
    public string sliderSelected;
    public string deathParticleSelected;
    public string winParticleSelected;
    public List<string> adsSlidersGot = new List<string>();
    public List<string> adsDeathParticleGot = new List<string>();
    public List<string> adsWinParticleGot = new List<string>();

    void Start()
    {
        LoadData();
        UIManager.Instance.ClickSlider(sliderSelected);
        UIManager.Instance.ClickDeathParticle(deathParticleSelected);
        UIManager.Instance.ClickWinParticle(winParticleSelected);

        UIManager.onCosmeticEnter += () => 
        {
            UIManager.Instance.ClickSlider(sliderSelected);
            UIManager.Instance.ClickDeathParticle(deathParticleSelected);
            UIManager.Instance.ClickWinParticle(winParticleSelected);
            UIManager.Instance.spotsWon.text = levelsWon.ToString(); 
        };
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    adsSlidersGot = new List<string>();
        //    adsDeathParticleGot = new List<string>();
        //    adsWinParticleGot = new List<string>();
        //    SaveData();
        //}
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
        deathParticleSelected = data.deathParticleSelected;
        winParticleSelected = data.winParticleSelected;
        adsSlidersGot = data.adsSlidersGot;
        if (adsSlidersGot==null) adsSlidersGot=new List<string>();
        adsDeathParticleGot = data.adsDeathParticleGot;
        if (adsDeathParticleGot== null) adsDeathParticleGot = new List<string>();
        adsWinParticleGot = data.adsWinParticleGot;
        if (adsSlidersGot == null) adsWinParticleGot = new List<string>();
    }

    public DataManager(int levelsWonP, int adsPointsP, string sliderSelectedP, string deathParticleSelectedP, string winParticleSelectedP,
        List<string> adsSlidersGotP, List<string> adsDeathParticleGotP, List<string> adsWinParticleGotP
        )
    {
        levelsWon = levelsWonP;
        adsPoints = adsPointsP;
        sliderSelected = sliderSelectedP;
        deathParticleSelected = deathParticleSelectedP;
        winParticleSelected = winParticleSelectedP;
        adsSlidersGot = adsSlidersGotP;
        adsDeathParticleGot = adsDeathParticleGotP;
        adsWinParticleGot = adsWinParticleGotP;
    }
}
