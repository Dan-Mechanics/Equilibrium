using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OuterWilds
{
    /// <summary>
    /// NOTE: is destructive.
    /// </summary>
    [CreateAssetMenu(menuName = nameof(ColorRangeDecorator), fileName = "New " + nameof(ColorRangeDecorator))]
    public class ColorRangeDecorator : TerrainDecorator
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