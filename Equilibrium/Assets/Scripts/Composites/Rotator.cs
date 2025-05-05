using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Equilibrium
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private Vector3 rot = default;

        private void FixedUpdate()
        {
            transform.Rotate(rot * Time.fixedDeltaTime, Space.Self);
        }
    }
}