using UnityEngine;

public class CamreMove : MonoBehaviour
{
    private Vector3 _offset = new Vector3(0f, 1.2f, -2.5f);

    [SerializeField]
    private GameObject _player;

    void LateUpdate()
    {
        transform.position = _player.transform.position + _offset;
    }
}
