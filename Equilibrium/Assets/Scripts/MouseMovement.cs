using UnityEngine;

namespace Equilibrium
{
    public class MouseMovement : MonoBehaviour
    {
        private const float MIN_CAM_ANGLE = 0f;
        private const float MAX_CAM_ANGLE = 0f;

        [SerializeField] private Transform cam = null;
        [SerializeField] private float sens = 0.33f;

        private Vector2 mouseDirection;
        private float mouseX;
        private float mouseY;

        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            mouseX = Input.GetAxisRaw("Mouse X");
            mouseY = 0f;

            Vector2 mouseDirectionChange = new Vector2(mouseX, mouseY);

            mouseDirection += mouseDirectionChange * sens;

            mouseDirection.y = Mathf.Clamp(mouseDirection.y, MIN_CAM_ANGLE, MAX_CAM_ANGLE);

            cam.localRotation = Quaternion.AngleAxis(-mouseDirection.y, Vector3.right);
            transform.localRotation = Quaternion.AngleAxis(mouseDirection.x, Vector3.up);
        }
    }
}