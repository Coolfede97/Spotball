using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;
public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, 
    IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static AdsManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance!=this) Destroy(gameObject);
        else
        {
            Instance = this;
            StartAds();
        }
    }

    [SerializeField] string androidGameID;
    [SerializeField] string iOSGameID;

    [SerializeField] string androidAdsID;
    [SerializeField] string iOSAdsID;

    string selectedID;
    string selectedAdID;

    [SerializeField] bool testMode = true;

    void StartAds()
    {
#if UNITY_ANDROID
        selectedID = androidGameID;
        selectedAdID = androidAdsID;
#elif UNITY_IOS
        selectedID = iOSGameID;
        selectedAdID = iOSAdsID;
#elif UNITY_EDITOR
        selectedID = androidGameID;
        selectedAdID = androidAdsID;
#endif
        if (!Advertisement.isInitialized)
        {
            Advertisement.Initialize(selectedID, testMode, this);
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Inicialización completada épicamente");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("Inicialización #-NO-# completada épicamente");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Advertisement.Show(placementId, this);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId.Equals(selectedAdID) && (showCompletionState.Equals(UnityAdsShowCompletionState.SKIPPED) || showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))) 
        {
            UnlockItem();
        }
    }

    public void ShowAd()
    {
        Advertisement.Load(selectedAdID, this);
    }

    void UnlockItem()
    {
        if (UIManager.Instance.newAdSliderSelected!=null)
        {
            Item item = UIManager.Instance.newAdSliderSelected;
            item.price = 0;
            Destroy(item.adIcon);
            DataManager.Instance.adsSlidersGot.Add(item._name);
            UIManager.Instance.ClickSlider(item._name);
            UIManager.Instance.newAdSliderSelected = null;
        }
        else if (UIManager.Instance.newAdDeathParticleSelected != null)
        {
            Item item = UIManager.Instance.newAdDeathParticleSelected;
            item.price = 0;
            Destroy(item.adIcon);
            DataManager.Instance.adsDeathParticleGot.Add(item._name);
            UIManager.Instance.ClickDeathParticle(item._name);
            UIManager.Instance.newAdDeathParticleSelected = null;
        }
        else if (UIManager.Instance.newAdWinParticleSelected != null)
        {
            Item item = UIManager.Instance.newAdWinParticleSelected;
            item.price = 0;
            Destroy(item.adIcon);
            DataManager.Instance.adsWinParticleGot.Add(item._name);
            UIManager.Instance.ClickWinParticle(item._name);
            UIManager.Instance.newAdWinParticleSelected = null;
        }
        DataManager.Instance.SaveData();
    }
}
