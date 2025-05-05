using System;
using UnityEngine;

namespace OuterWilds
{
    [Serializable]
    public struct RaycastSettings
    {
        public float range;
        public LayerMask mask;
    }
}