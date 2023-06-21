using UnityEngine;

public class SpawnFlow : MonoBehaviour
{
    [SerializeField]
    private float axisZ;
    [SerializeField]
    private float rate;
    [SerializeField]
    private GameObject[] _prefabs;

    void Start()
    {
        InvokeRepeating("SpawnMan", 0, rate);
    }

    private void SpawnMan()
    {
        var _pos = new Vector3(axisZ, 1.4f,200f );
        int rand = Random.Range(0, _prefabs.Length);
        var obj = Instantiate(_prefabs[rand], _pos, _prefabs[rand].transform.rotation);
        Destroy(obj, 30f);
    }
}
