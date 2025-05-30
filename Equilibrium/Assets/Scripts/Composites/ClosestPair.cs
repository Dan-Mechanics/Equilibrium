using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Equilibrium
{
    /// <summary>
    /// je kan ook zeggen closest pair of T waar T
    /// altijd een component moet zijn.
    /// </summary>
    public struct ClosestPair
    {
        public Transform Transform => component.transform;

        public Component component;
        public float distance;

        public ClosestPair(Component component, float distance)
        {
            this.component = component;
            this.distance = distance;
        }

        public void Set(Component component, float distance)
        {
            this.component = component;
            this.distance = distance;
        }
    }
}