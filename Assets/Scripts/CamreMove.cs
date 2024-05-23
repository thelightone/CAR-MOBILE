using UnityEngine;

public class CamreMove : MonoBehaviour
{
    private Vector3 _offset = new Vector3(0f, 1.2f, -2.5f);
    private Camera camera;
    private float fov;
    [SerializeField]
    private GameObject _player;

    private void Start()
    {
        camera = GetComponent<Camera>();
        fov =camera.fieldOfView;
    }

    void LateUpdate()
    {
        transform.position = _player.transform.position + _offset;
        camera.fieldOfView = fov+PlayerController.instance._speed;
    }
}
