using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using System;
using UnityEngine.UI;

namespace Equilibrium
{
    /// <summary>
    /// Working on: make it have 1 button and make it use interactable and make the cursor system.
    /// </summary>
    public class BetterButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action OnClick;
        
        [SerializeField] private KeyCode key = KeyCode.Mouse0;
        [SerializeField] private bool interactable = default;
        [SerializeField] private TMP_Text text = default;
        [SerializeField] private Image image = default;
        [SerializeField] private GameObject interactableGraphic = default;

        [SerializeField] private UnityEvent onClick = default;
        [SerializeField] private UnityEvent onSelect = default;
        [SerializeField] private UnityEvent onDeslect = default;

        private bool isSelected;

        private void Start() 
        {
            Deselect();
            SetInteractable(interactable);
        }

        private void Update()
        {
            if (!GetHasClicked())
                return;

            OnClick?.Invoke();
            onClick?.Invoke();
        }

        private bool GetHasClicked() 
        {
            return interactable && isSelected &&
                gameObject.activeInHierarchy && Input.GetKeyDown(key);
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

        public void SetInteractable(bool value) 
        {
            interactable = value;
            interactableGraphic.SetActive(!value);
        }

        /// <summary>
        /// HAKC FIX !!
        /// </summary>
        /// <param name="value"></param>
        public void GiveCooldown(float value) 
        {
            if (!TryGetComponent(out TimerEvent timer))
                return;

            interactable = false;
            timer.SetTimer(value);
        }

        public void SetText(string writing) => text.text = writing;
        public void SetSprite(Sprite sprite) => image.sprite = sprite;
    }
}