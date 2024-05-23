using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
    public float _speed2 = 20.0f;

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * _speed2*PlayerController.instance._speed/10);
    }

    private void OnTriggerEnter(Collider other)
    {
      
        if (other.gameObject.CompareTag("Finish"))
        {
            Destroy(gameObject);
        }
    }
}
