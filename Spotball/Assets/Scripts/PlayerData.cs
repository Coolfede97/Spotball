using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int levelsWon;
    public int adsPoints;
    public string sliderSelected;
    public string deathParticleSelected;
    public string winParticleSelected;
    public PlayerData(DataManager manager)
    {
        levelsWon = manager.levelsWon;
        adsPoints = manager.adsPoints;
        sliderSelected = manager.sliderSelected;
        deathParticleSelected = manager.deathParticleSelected;
        winParticleSelected = manager.winParticleSelected;
    }
}
