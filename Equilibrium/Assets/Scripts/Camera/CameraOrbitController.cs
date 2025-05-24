using UnityEngine;
using UnityEngine.Events;

namespace Equilibrium
{
    public class CameraOrbitController : MonoBehaviour
    {
        [SerializeField] private UnityEvent<bool> onChangeDragging = default;
        [SerializeField] private UnityEvent<float> onSensChange = default;

        [SerializeField] private float sensitivity = default;
        [SerializeField] private float minAngle = default;
        [SerializeField] private float maxAngle = default;
        [SerializeField] private Vector3 rotation = default;

        private void Start() => onSensChange?.Invoke(sensitivity);

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
                onChangeDragging?.Invoke(true);

            if (Input.GetKeyUp(KeyCode.Mouse1))
                onChangeDragging?.Invoke(false);

            if (Input.GetKey(KeyCode.Mouse1))
            {
                rotation.y += sensitivity * Input.GetAxisRaw("Mouse X");
                rotation.x += sensitivity * -Input.GetAxisRaw("Mouse Y");
            }

            rotation.x = Mathf.Clamp(rotation.x, minAngle, maxAngle);
            transform.localRotation = Quaternion.Euler(rotation);

            if (Input.GetKey(KeyCode.LeftShift))
                return;

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                sensitivity *= 2f;
                onSensChange?.Invoke(sensitivity);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                sensitivity /= 2f;
                onSensChange?.Invoke(sensitivity);
            }
        }
    }
}