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
        private float? interactableAgainTime;

        private void Start() 
        {
            Deselect();
            SetInteractable(interactable);
        }

        private void Update()
        {
            if (interactableAgainTime != null) 
            {
                float nextTime = (float)interactableAgainTime;
                if (Time.time >= nextTime)
                {
                    SetInteractable(true);
                    //interactableAgainTime = null;
                }
            }
            
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
            interactableAgainTime = null;
        }

        public void GiveCooldown(float value) 
        {
            SetInteractable(false);
            interactableAgainTime = Time.time + value;
        }

        public void SetText(string writing) => text.text = writing;
        public void SetSprite(Sprite sprite) => image.sprite = sprite;
    }
}