using UnityEngine;

namespace Equilibrium
{
    public class CustomCursorHandler : MonoBehaviour
    {
        [SerializeField] private RectTransform rect = default;

        private void Update() 
        {
            SetCursorVisible();
            PlaceCursor();
        }

        private void PlaceCursor()
        {
            Vector2 cursorPosition = Input.mousePosition;

            cursorPosition.x -= Screen.width / 2f;
            cursorPosition.y -= Screen.height / 2f;

            rect.anchoredPosition = cursorPosition;
        }

        private void SetCursorVisible() 
        {
            bool onScreen = true;

            if (Input.mousePosition.x < 0f)
                onScreen = false;

            if (Input.mousePosition.y < 0f)
                onScreen = false;

            if (Input.mousePosition.y > Screen.height)
                onScreen = false;

            if (Input.mousePosition.x > Screen.width)
                onScreen = false;

            Cursor.visible = !onScreen && Application.isFocused;
        }
    }
}