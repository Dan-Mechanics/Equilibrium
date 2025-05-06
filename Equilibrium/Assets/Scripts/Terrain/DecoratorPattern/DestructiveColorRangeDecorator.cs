using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Equilibrium
{
    /// <summary>
    /// NOTE: is destructive.
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableObject/" + nameof(DestructiveColorRangeDecorator), fileName = "New " + nameof(DestructiveColorRangeDecorator))]
    public class DestructiveColorRangeDecorator : TerrainColorDecorator
    {
        public float floorHeight;
        public float ceilingHeight;

        public override float GetColorFloor(float min)
        {
            return floorHeight;
        }

        public override float GetColorCeiling(float max)
        {
            return ceilingHeight;
        }
    }
}