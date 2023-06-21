using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
    private float _speed2 = 20.0f;

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * _speed2);
    }
}
