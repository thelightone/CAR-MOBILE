using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class OnTriggerEvent : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _onTriggerEnterEvent;
    [SerializeField]
    private UnityEvent _onTriggerExitEvent;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            _onTriggerEnterEvent.Invoke();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            _onTriggerExitEvent.Invoke();
        }
    }
}