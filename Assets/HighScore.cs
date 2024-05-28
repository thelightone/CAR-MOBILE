using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HighScore : MonoBehaviour
{
    [SerializeField] private TMP_Text hs;
    [SerializeField] private GameObject car10;
    [SerializeField] private GameObject car11;
    [SerializeField] private GameObject car20;
    [SerializeField] private GameObject car21;
    [SerializeField] private GameObject car30;
    [SerializeField] private GameObject car31;
    [SerializeField] private GameObject car40;
    [SerializeField] private GameObject car41;

    void Start()
    {
        ///saveSystem = new YGSaveSystem();
        //YandexGame.GetDataEvent += Load;
        Load();
    }

    private void OnEnable()
    {
        Load();
    }

    public void Load()
    {
        //saveSystem = new YGSaveSystem();
        //SaveData data = saveSystem.Load();

        //var lang =  YandexGame.savesData.language;
        //if (lang=="ru")
        //hs.text = "ÐÅÊÎÐÄ: " + data.HighScore.ToString();
        //else
        //hs.text = "HIGHSCORES: " + data.HighScore.ToString();

        //car10.SetActive(data.car1);
        //car11.SetActive(!data.car1);
        //car20.SetActive(data.car2);
        //car21.SetActive(!data.car2);
        //car30.SetActive(data.car3);
        //car31.SetActive(!data.car3);
        //car40.SetActive(data.car4);
        //car41.SetActive(!data.car4);
    }
    public void SwLang(string lang)
    {
       // YandexGame.SwitchLanguage(lang);
    }
}
