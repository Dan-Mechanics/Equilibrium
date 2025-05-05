using UnityEngine;
using UnityEngine.UI;

namespace OuterWilds
{
    [RequireComponent(typeof(Image))]
    public class SpriteSetter : MonoBehaviour
    {
        [SerializeField] private Sprite[] sprites = default;
        
        private Image image;

        private void Awake() => image = GetComponent<Image>();

        public void Set(int index) 
        {
            if (index < 0 || index >= sprites.Length)
                return;

            image.sprite = sprites[index];
        }
    }
}