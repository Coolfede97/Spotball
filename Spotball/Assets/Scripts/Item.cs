using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Item : MonoBehaviour
{
    UnityEngine.UI.Image image;
    public bool selected;
    public bool affordable;
    public string _name;
    public int price;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] bool isSlider;
    [SerializeField] bool isDeathParticle;
    [SerializeField] bool isWinParticle;
    public bool isAdItem;
    public GameObject adIcon;
    public GameObject item;
    private void Awake()
    {
        UIManager.onItemSelected += CheckIfSelected;
        UIManager.onCosmeticEnter += CheckIfAffordable;
        if (!isAdItem) priceText.text = price.ToString();
        image = GetComponent<UnityEngine.UI.Image>();
    }
    void Start()
    {
        
    }

    public void CheckIfSelected()
    {
        if (!affordable) return;
        if (isSlider)
        {
            selected = UIManager.Instance.sliderSelected == this ? true : false;
        }
        else if (isDeathParticle)
        {
            selected = UIManager.Instance.deathParticleSelected == this ? true : false;
        }
        else if (isWinParticle)
        {
            selected = UIManager.Instance.winParticleSelected == this ? true : false;
        }
        else GF.DebugFedeError(" Este item #--NO--# es un SLIDER, un DEATHPARTICLE ni un WINPARTICLE");
        SetColor();
    }
    
    public void CheckIfAffordable()
    {
        if (DataManager.Instance.levelsWon < price)
        {
            affordable = false;
        }
        else
        {
            affordable = true;
            if (isAdItem) Destroy(adIcon);
        }
        SetColor();
    }

    void SetColor()
    {
        if (selected) image.color = UIManager.Instance.selectedColor;
        else if (affordable) image.color = UIManager.Instance.unselectedColor;
        else if (isAdItem && CheckIfAdUnlocked())
        {
            image.color = UIManager.Instance.unselectedColor;
            Destroy(adIcon);
        }
        else image.color = UIManager.Instance.noAffordableColor;
    }

    bool CheckIfAdUnlocked()
    {
        if (isSlider && DataManager.Instance.adsSlidersGot.Contains(_name)) return true;
        else if (isDeathParticle && DataManager.Instance.adsDeathParticleGot.Contains(_name)) return true;
        else if (isWinParticle && DataManager.Instance.adsWinParticleGot.Contains(_name)) return true;
        return false;
    }
}
