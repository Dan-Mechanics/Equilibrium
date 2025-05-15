using UnityEngine;

namespace Equilibrium
{
    public class CursorPlacer : MonoBehaviour
    {
        [SerializeField] private RectTransform rect = null;

        private void Start()
        {
            Cursor.visible = false;
        }

        private void Update() => PlaceCursor();

        private void PlaceCursor()
        {
            Cursor.visible = !Application.isFocused;
            
            Vector2 cursorPosition = Input.mousePosition;
            cursorPosition.x -= Screen.width / 2f;
            cursorPosition.y -= Screen.height / 2f;

            rect.anchoredPosition = cursorPosition;
        }
    }
}