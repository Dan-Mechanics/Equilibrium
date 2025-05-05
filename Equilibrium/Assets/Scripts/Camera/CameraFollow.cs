using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipProject.Looking
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target = null;
        [SerializeField] private float smoothSpeed = 0f;

        private void FixedUpdate()
        {
            Vector3 desiredPos = target.position;
            Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);

            Quaternion desiredRot = target.rotation;
            Quaternion smoothedRot = Quaternion.Lerp(transform.rotation, desiredRot, smoothSpeed);

            transform.position = smoothedPos;
            transform.rotation = smoothedRot;
        }
    }
}