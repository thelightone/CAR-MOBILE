using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Environmentcontroller : MonoBehaviour
{
    [SerializeField] private GameObject _sky;
    [SerializeField] private GameObject _road;
    private Vector3 _roadPos;

    [SerializeField] private TMP_Text _textLevel;
    [SerializeField] private TMP_Text _scores;
    private int level = 1;

    [SerializeField] private VolumeProfile volume;
    private float timer = 0;
    private float scoresNum;

    [SerializeField] private TMP_Text _speed;
    [SerializeField] private RectTransform _spdmtr;

    private void Start()
    {
        _roadPos = _road.transform.position;
    }

    void Update()
    {
        var speed = PlayerController.instance._speed/10;

        var newRot = Quaternion.Euler(_sky.transform.rotation.x, _sky.transform.rotation.y + speed, _sky.transform.rotation.z);
        _sky.transform.Rotate(0, speed / 100, 0);
        _road.transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (_road.transform.position.z < 0)
        {
            _road.transform.position = _roadPos;
        }

        if (speed > 0)
        {
            timer += Time.deltaTime;

            if (timer > 10)
            {
                StartCoroutine(NewLevel());
                Time.timeScale += 0.15f;
            }
            scoresNum += Time.deltaTime * 2;
            _scores.text = "SCORES: " + Convert.ToInt32(scoresNum);
        }

        _speed.text = (speed*10).ToString();
        _spdmtr.rotation = Quaternion.Euler(_spdmtr.rotation.x, _spdmtr.rotation.y, 132-speed*10);
    }

    private IEnumerator NewLevel()
    {
        timer = 0;
        var newShift = UnityEngine.Random.Range(-170, 170);
        if (volume.TryGet<ColorAdjustments>(out ColorAdjustments colorAdj))
        {
            var curShift = colorAdj.hueShift.value;
            level++;
            _textLevel.text = "LEVEL " + level;
            _textLevel.gameObject.SetActive(true);

            float elapsedTime = 0;
            while (elapsedTime < 2) 
            {
                colorAdj.hueShift.value = Mathf.Lerp(curShift, newShift, elapsedTime / 2);
                elapsedTime+= Time.deltaTime;
                yield return null;
            }

            _textLevel.gameObject.SetActive(false);
        }
    }
}
