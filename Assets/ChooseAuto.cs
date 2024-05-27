using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseAuto : MonoBehaviour
{
public List<GameObject> frames = new List<GameObject>();
    public List<GameObject> cars = new List<GameObject>();

    public void Choose(int index)
    {
        foreach (GameObject go in frames)
        {
            go.SetActive(false);
        }
        frames[index].SetActive(true);
        foreach (GameObject go in cars)
        {
            go.SetActive(false);
        }
        cars[index].SetActive(true);
    }
}
