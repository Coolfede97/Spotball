using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    public bool onUI;
    bool onUITransition;
    public CanvasGroup uiMenuElements;
    public CanvasGroup uiCosmeticsElements;
    public TextMeshProUGUI spotsWon;
    [SerializeField] Transform slidersParent;
    [SerializeField] Transform deathParticlesParent;
    [SerializeField] Transform winParticlesParent;

    [Header("TRANSITIONS ##########################")]
    [SerializeField] float uiFadeOutDuration;
    [SerializeField] float uiFadeInDuration;

    public static event Action onItemSelected;
    public static event Action onCosmeticEnter;

    [Header("ITEMS ##############################")]
    public Item sliderSelected;
    public Item winParticleSelected;
    public Item deathParticleSelected;
    public Color selectedColor;
    public Color unselectedColor;
    public Color noAffordableColor;
    [Header("ADS ################################")]
    public Item? newAdSliderSelected;
    public Item? newAdDeathParticleSelected;
    public Item? newAdWinParticleSelected;

    void Start()
    {
        onUI = true;
        uiCosmeticsElements.gameObject.SetActive(false);
        RememberUnlockedAdItems();
    }
    
    public void UIChange(string function)
    {
        Invoke(function,0);
    }

    public void Play()
    {
        if (!onUI || onUITransition) return;
        onUITransition = true;
        uiMenuElements.DOFade(0, uiFadeOutDuration).onComplete= ()=> 
        {
            onUITransition = false;
            onUI = false;
            uiMenuElements.gameObject.SetActive(false);
        };
        
    }

    public void Menu()
    {
        if (onUITransition) return;
        onUITransition = true;
        onUI = true;
        uiMenuElements.gameObject.SetActive(true);
        uiCosmeticsElements.DOFade(0, uiFadeOutDuration).onComplete=()=> { uiCosmeticsElements.gameObject.SetActive(false); };
        uiMenuElements.DOFade(1, uiFadeOutDuration).onComplete = () => 
        {
            onUITransition = false;
        };
    }

    public void Cosmetics()
    {
        if (onUITransition || !onUI) return;
        onCosmeticEnter?.Invoke();
        onUITransition = true;
        uiCosmeticsElements.alpha = 0;
        uiCosmeticsElements.gameObject.SetActive(true);
        uiCosmeticsElements.DOFade(1, uiFadeOutDuration).onComplete = () => 
        {
            uiMenuElements.gameObject.SetActive(false);
            onUITransition = false; 
        };
    }

    public void ClickSlider(string name)
    {
        foreach (Item item in slidersParent.GetComponentsInChildren<Item>())
        {
            if (item._name == name)
            {
                if (!item.affordable && !item.isAdItem) return;
                if (!item.isAdItem)
                {
                    sliderSelected = item;
                    DataManager.Instance.sliderSelected = name;
                    DataManager.Instance.SaveData();
                    onItemSelected?.Invoke();
                }
                else
                {
                    if (DataManager.Instance.adsSlidersGot.Contains(name))
                    {
                        Debug.Log("LLamado");
                        sliderSelected = item;
                        item.price = 0;
                        item.CheckIfAffordable();
                        DataManager.Instance.sliderSelected = name;
                        DataManager.Instance.SaveData();
                        onItemSelected?.Invoke();
                    }
                    else
                    {
                        newAdSliderSelected = item;
                        AdsManager.Instance.ShowAd();
                    }
                }
            }
        }
    }

    public void ClickWinParticle(string name)
    {
        foreach (Item item in winParticlesParent.GetComponentsInChildren<Item>())
        {
            if (item._name == name)
            {
                if (!item.affordable && !item.isAdItem) return;
                if (!item.isAdItem)
                {
                    winParticleSelected = item;
                    DataManager.Instance.winParticleSelected = name;
                    DataManager.Instance.SaveData();
                    onItemSelected?.Invoke();
                }
                else
                {
                    if (DataManager.Instance.adsWinParticleGot.Contains(name))
                    {
                        winParticleSelected = item;
                        item.price = 0;
                        item.CheckIfAffordable();
                        DataManager.Instance.winParticleSelected = name;
                        DataManager.Instance.SaveData();
                        onItemSelected?.Invoke();
                    }
                    else
                    {
                        newAdWinParticleSelected = item;
                        AdsManager.Instance.ShowAd();
                    }
                }
            }
        }
    }
    public void ClickDeathParticle(string name)
    {
        foreach (Item item in deathParticlesParent.GetComponentsInChildren<Item>())
        {
            if (item._name == name)
            {
                if (!item.affordable && !item.isAdItem) return;
                if (!item.isAdItem) 
                {
                    deathParticleSelected = item;
                    DataManager.Instance.deathParticleSelected = name;
                    DataManager.Instance.SaveData();
                    onItemSelected?.Invoke();
                }
                else
                {
                    if (DataManager.Instance.adsDeathParticleGot.Contains(name))
                    {
                        deathParticleSelected = item;
                        item.price = 0;
                        item.CheckIfAffordable();
                        DataManager.Instance.deathParticleSelected = name;
                        DataManager.Instance.SaveData();
                        onItemSelected?.Invoke();
                    }
                    else
                    {
                        newAdDeathParticleSelected = item;
                        AdsManager.Instance.ShowAd();
                    }
                }
            }
        }
    }

    void RememberUnlockedAdItems()
    {
        foreach (Item item in slidersParent.GetComponentsInChildren<Item>())
        {
            //if (DataManager.Instance.)
        }
    }
    void Update()
    {
        
    }
}
