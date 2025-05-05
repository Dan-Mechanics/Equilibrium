using UnityEngine;

namespace OuterWilds
{
    public class CameraOrbitController : MonoBehaviour
    {
        [SerializeField] private float sensitivity = default;
        [SerializeField] private float minAngle = default;
        [SerializeField] private float maxAngle = default;

        [SerializeField] private Vector3 rotation = default;

        /// <summary>
        /// You could make universal mouse locker here.
        /// </summary>
        private void Start()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                rotation.y += sensitivity * Input.GetAxisRaw("Mouse X");
                rotation.x += sensitivity * -Input.GetAxisRaw("Mouse Y");

                /*rotation.x = Mathf.Clamp(rotation.x, minAngle, maxAngle);

                transform.localRotation = Quaternion.Euler(rotation);*/
            }

            rotation.x = Mathf.Clamp(rotation.x, minAngle, maxAngle);
            transform.localRotation = Quaternion.Euler(rotation);
        }
    }
}