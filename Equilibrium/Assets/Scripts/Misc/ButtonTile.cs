using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace OuterWilds
{
    [RequireComponent(typeof(Image))]
    public class ButtonTile : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
    { 
        [SerializeField] private Color grass = Color.green;
        [SerializeField] private Color water = Color.blue;

        private Image image;

        private void Awake()
        {
            image = GetComponent<Image>();

            image.color = grass;
        }

        public void OnPointerDown(PointerEventData pointerEventData)
        {
            if (Input.GetKey(KeyCode.Mouse0) == Input.GetKey(KeyCode.Mouse1))
                return;

            image.color = Input.GetKey(KeyCode.Mouse0) ? water : grass;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (Input.GetKey(KeyCode.Mouse0) == Input.GetKey(KeyCode.Mouse1))
                return;

            image.color = Input.GetKey(KeyCode.Mouse0) ? water : grass;
        }

        /*public void OnPointerExit(PointerEventData eventData)
        {
            image.color = Color.white;
        }*/
    }
}