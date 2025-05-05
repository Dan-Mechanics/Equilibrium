using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OuterWilds
{
    public class Zoomer : MonoBehaviour
    {
        //[SerializeField] private Camera cam = default;
        [SerializeField] private float minFov = default;
        [SerializeField] private float maxFov = default;
        [SerializeField] private float zoomSens = default;

        [SerializeField] private float fov = default;

        /*private void Start()
        {
            size = cam.orthographicSize;
        }*/

        private void Update()
        {
            fov += Input.mouseScrollDelta.y * -zoomSens;
            fov = Mathf.Clamp(fov, minFov, maxFov);

            //cam.fieldOfView = size;
            transform.localPosition = Vector3.back * fov;
        }
    }
}