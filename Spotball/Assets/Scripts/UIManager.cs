using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    public bool onUI;
    public List<CanvasGroup> uiElements;
    [SerializeField] float uiFadeOutDuration;
    [SerializeField] float uiFadeInDuration;
    void Start()
    {
        onUI = true;     
    }
    
    public void UIChange(string function)
    {
        Invoke(function,0);
    }

    public void Play()
    {

        foreach (CanvasGroup ui in uiElements) 
        {
            ui.DOFade(0, uiFadeOutDuration).onComplete = ()=> { onUI = false; };
        }
    }

    public void Menu()
    {
        onUI = true;
        foreach (CanvasGroup ui in uiElements)
        {
            ui.DOFade(1, uiFadeOutDuration);
        }
    }
    void Update()
    {
        
    }
}
