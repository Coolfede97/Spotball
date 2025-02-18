using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AntiglowingForUI : MonoBehaviour
{
    Material material;
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
        UIManager.onCosmeticEnter += QuitGlowing;
        UIManager.onMenuEnter += ReturnGlowingBack;
    }


    void QuitGlowing()
    {
        material.SetFloat("_Glow", 0);
        material.SetFloat("_GlowGlobal", 1);
    }
    void ReturnGlowingBack()
    {
        //material.SetFloat("_Glow", 100);
        //material.SetFloat("_GlowGlobal", 100);
        material.DOFloat(100, "_Glow", 0.1f);
        material.DOFloat(100, "_GlowGlobal", 0.1f);
    }
}
