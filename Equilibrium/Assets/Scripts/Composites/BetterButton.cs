using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using System;

namespace Equilibrium
{
    /// <summary>
    /// Working on: make it have 1 button and make it use interactable and make the cursor system.
    /// </summary>
    public class BetterButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action OnClick = default;
        
        [SerializeField] private KeyCode key = KeyCode.Mouse0;
        [SerializeField] private TMP_Text text = default;

        [SerializeField] private UnityEvent onClick = default;
        [SerializeField] private UnityEvent onSelect = default;
        [SerializeField] private UnityEvent onDeslect = default;

        public bool interactable;

        private bool isSelected;

        private void Start() => Deselect();

        private void Update()
        {
            if (!isSelected)
                return;

            if (!Input.GetKeyDown(key))
                return;

            OnClick?.Invoke();
            onClick?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData) => Select();
        public void OnPointerExit(PointerEventData eventData) => Deselect();
        private void OnDisable() => Deselect();

        private void Select() 
        {
            if (isSelected)
                return;

            isSelected = true;
            onSelect?.Invoke();
        }

        private void Deselect() 
        {
            if (!isSelected)
                return;

            isSelected = false;
            onDeslect?.Invoke();
        }

        public void SetText(string writing) => text.text = writing;
    }
}