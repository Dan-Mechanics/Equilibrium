using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OuterWilds
{
    [CreateAssetMenu(menuName = nameof(ColorFloorDecorator), fileName = "New " + nameof(ColorFloorDecorator))]
    public class ColorFloorDecorator : TerrainDecorator
    {
        public float floorHeight;

        public override float GetColorFloor(ref Mesh mesh)
        {
            float y = terrainable.GetColorFloor(ref mesh);
            if (y < floorHeight)
                y = floorHeight;

            return y;
        }
    }
}