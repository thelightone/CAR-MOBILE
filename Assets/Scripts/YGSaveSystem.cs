using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public  class YGSaveSystem 
{
    public void Save(SaveData data)
    {
        YandexGame.savesData+=data;
        YandexGame.SaveProgress();
    }

    public  SaveData Load()
    {
        return (SaveData)YandexGame.savesData;
    }

    public void ResetSaves()
    {
        YandexGame.ResetSaveProgress();
    }
}
[Serializable]
public class SaveData
{
    public int HighScore = 0;
    public bool car1 = true;
    public bool car2 = false;
    public bool car3 = false;
    public bool car4 = false;


    public static explicit operator SaveData(SavesYG savesYG)
    {
        return new SaveData()
        {
            HighScore = savesYG.HighScore,
            car1 = savesYG.car1,
            car2 = savesYG.car2,
            car3 = savesYG.car3,
            car4 = savesYG.car4
    };
    }
}
