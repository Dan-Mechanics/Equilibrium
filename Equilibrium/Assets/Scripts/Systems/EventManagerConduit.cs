using UnityEngine;
using UnityEngine.Events;

namespace Equilibrium
{
    public class EventManagerConduit : MonoBehaviour
    {
        [SerializeField] private EventManager.EventType eventType = default;
        [SerializeField] private UnityEvent onReceive = default;

        private void OnEnable()
        {
            EventManager.AddListener(eventType, Receive);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener(eventType, Receive);
        }

        private void Receive(EventManager.EventType eventType) => onReceive?.Invoke();

        public void Raise(EventManager.EventType eventType) => EventManager.RaiseEvent(eventType);
    }
}