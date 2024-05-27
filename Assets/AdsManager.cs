using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using YG;

public class AdsManager : MonoBehaviour
{
    private YGSaveSystem saveSystem;
    [SerializeField] private HighScore highscore;

    [SerializeField] private VolumeProfile volume;

    private void OnEnable()
    {
        if (volume.TryGet<ColorAdjustments>(out ColorAdjustments colorAdj))
        {
            colorAdj.hueShift.value = -35;
        }
        YandexGame.RewardVideoEvent += Revive;
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Revive;

    }

    public void StartNew()
    {
        YandexGame.FullscreenShow();
        StartCoroutine(Load());
    }

    private IEnumerator Load()
    {
        yield return new WaitForSeconds(1);
        LoadScene();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowRewarded(int id)
    {
        YandexGame.RewVideoShow(id);
        //Revive(id);
    }

    public void ResetProgress()
    {
        saveSystem = new YGSaveSystem();
        saveSystem.ResetSaves();
        highscore.Load();
    }

    public void Revive(int id)
    {

        saveSystem = new YGSaveSystem();
        SaveData saveData = saveSystem.Load();
        
        switch (id)
        {
            case 0:
                StartCoroutine(ReviveCor());
                break;
            case 1:
                saveData.car2 = true;
                break;
            case 2:
                saveData.car3 = true;
                break;
            case 3:
                saveData.car4 = true;
                break;
        }

        saveSystem.Save(saveData);
        saveData = saveSystem.Load();

        highscore.Load();
    }

    private IEnumerator ReviveCor()
    {
        yield return new WaitForSeconds(1);
        PlayerController.instance.Revive();
    }
}
