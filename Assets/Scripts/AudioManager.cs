using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public List<Button> switchAudio = new List<Button>();
    public bool audioOn;

    public AudioSource audioSourceMain;
    public AudioSource audioSourceNext;

    public AudioClip onHit;
    public AudioClip engineSound;

    void Start()
    {
        audioOn = true;
        Instance = this;

        foreach (Button b in switchAudio)
        {
            b.onClick.AddListener(() => OnOffAudio());
        }
    }

    public void OnHit()
    {
        audioSourceNext.PlayOneShot(onHit);
    }

    public void OnStart()
    {
        audioSourceNext.clip = engineSound;
        audioSourceNext.Play();
        audioSourceNext.volume = 0;
        StartCoroutine(IncreaseVolume());
    }

    private IEnumerator IncreaseVolume()
    {
        float elapsedTime = 0;

        while (elapsedTime<=0.5f)
        {
            audioSourceNext.volume = elapsedTime * 2;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void OnStop()
    {
        audioSourceNext.Stop();
    }

    void OnOffAudio()
    {
        if (audioOn)
        {
            audioOn = false;
            audioSourceMain.volume = 0;
            audioSourceNext.volume = 0;
        }
        else
        {
            audioOn = true;
            audioSourceMain.volume = 1;
            audioSourceNext.volume = 1;
        }
    }
}
