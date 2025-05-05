using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Equilibrium
{
    public class BetterButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private KeyCode key = KeyCode.Mouse0;

        [SerializeField] private UnityEvent onClick = default;
        [SerializeField] private UnityEvent onHighlight = default;
        [SerializeField] private UnityEvent onLowlight = default;
        
        private bool isHighlighted;

        private void Start() => Low();

        private void Update()
        {
            if (isHighlighted && Input.GetKeyDown(key))
                onClick?.Invoke();
        }

        //public void DoClick() => onClick?.Invoke();

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (isHighlighted)
                return;
            
            High();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!isHighlighted)
                return;

            Low();
        }

        private void High() 
        {
            isHighlighted = true;
            onHighlight?.Invoke();
        }

        private void Low() 
        {
            isHighlighted = false;
            onLowlight?.Invoke();
        }

        private void OnDisable() => Low();
    }
}