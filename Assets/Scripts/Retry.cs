using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class MainMenu : MonoBehaviour
{
    private void OnEnable()
    {
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
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }

    public void ShowRewarded()
    {
        YandexGame.RewVideoShow(1);
    }

    public void Revive(int id)
    {
        StartCoroutine(ReviveCor());
    }

    private IEnumerator ReviveCor()
    {
        yield return new WaitForSeconds(2);
        PlayerController.instance.Revive();
    }
}
