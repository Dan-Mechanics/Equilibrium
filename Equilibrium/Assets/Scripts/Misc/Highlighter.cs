using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace OuterWilds
{
    public class Highlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private bool fromStart = default;
        
        [SerializeField] private UnityEvent enter = default;
        [SerializeField] private UnityEvent exit = default;

        private void Awake() 
        {
            if (fromStart)
                exit?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            enter?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            exit?.Invoke();
        }
    }
}