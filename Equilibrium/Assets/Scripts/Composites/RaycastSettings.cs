using System;
using UnityEngine;

namespace Equilibrium
{
    [Serializable]
    public struct RaycastSettings
    {
        public float range;
        public LayerMask mask;
    }
}