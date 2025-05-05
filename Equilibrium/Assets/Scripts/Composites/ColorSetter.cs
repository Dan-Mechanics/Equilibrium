using UnityEngine;
using UnityEngine.UI;

namespace OuterWilds
{
    [RequireComponent(typeof(Image))]
    public class ColorSetter : MonoBehaviour
    {
        [SerializeField] private Color[] colors = new Color[] { Color.white };
        
        private Image image;

        private void Awake() => image = GetComponent<Image>();

        public void Set(int index) 
        {
            if (index < 0 || index >= colors.Length)
                return;

            image.color = colors[index];
        }
    }
}