using UnityEngine;

namespace Equilibrium
{
    public class Zoomer : MonoBehaviour
    {
        [SerializeField] private Camera cam = default;
        [SerializeField] private float minFov = default;
        [SerializeField] private float maxFov = default;
        [SerializeField] private float zoomSens = default;
        [SerializeField] private float fov = default;

        private void Update()
        {
            fov += Input.mouseScrollDelta.y * -zoomSens;
            fov = Mathf.Clamp(fov, minFov, maxFov);
            
            if (cam.orthographic)
            {
                cam.orthographicSize = fov;
            }
            else 
            {
                transform.localPosition = Vector3.back * fov;
            }
        }
    }
}