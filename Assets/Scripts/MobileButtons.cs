using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
    public class MobileButtons : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public UnityEvent onPointerDown;
        public UnityEvent onPointerUp;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            onPointerDown?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            onPointerUp?.Invoke();
        }
    }