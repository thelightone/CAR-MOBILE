using System.Collections.Generic;
using UnityEngine;

public class SpawnFlow : MonoBehaviour
{
    [SerializeField]
    private float axisZ;
    [SerializeField]
    private float rate;
    [SerializeField]
    private GameObject[] _prefabs;
    float timer;

    public static SpawnFlow Instance;
    public List <GameObject> _enemyControllers = new List <GameObject>();


    void Start()
    {
        Instance = this;
        _enemyControllers.Clear();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > rate&& PlayerController.instance.speed>0) 
        {
            timer = 0;
            SpawnMan();
        }
    }
   
    private void SpawnMan()
    {
        var _pos = new Vector3(Random.Range(axisZ,-axisZ), 1.4f,200f );
        int rand = Random.Range(0, _prefabs.Length);
        var obj = Instantiate(_prefabs[rand], _pos, _prefabs[rand].transform.rotation);
        _enemyControllers.Add(obj);
        Destroy(obj, 30f);
    }

    public void Clear()
    {
        foreach (var obj in _enemyControllers)
        {

            Destroy(obj);
        }
        _enemyControllers.Clear();
    }
}
