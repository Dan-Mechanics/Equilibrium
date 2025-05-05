using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Equilibrium
{
    /// <summary>
    /// NOTE: is destructive.
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableObject/" + nameof(DestructiveColorRangeDecorator), fileName = "New " + nameof(DestructiveColorRangeDecorator))]
    public class DestructiveColorRangeDecorator : TerrainDecorator
    {
        public float floorHeight;
        public float ceilingHeight;

        public override float GetColorFloor(ref Mesh mesh)
        {
            return floorHeight;
        }

        public override float GetColorCeiling(ref Mesh mesh)
        {
            return ceilingHeight;
        }
    }
}