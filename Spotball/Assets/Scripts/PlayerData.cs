using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int levelsWon;
    public int adsPoints;
    public int sliderSelected;

    public PlayerData(DataManager manager)
    {
        levelsWon = manager.levelsWon;
        adsPoints = manager.adsPoints;
        sliderSelected = manager.sliderSelected;
    }
}
