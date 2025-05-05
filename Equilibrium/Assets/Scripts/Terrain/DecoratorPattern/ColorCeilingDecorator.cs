using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OuterWilds
{
    [CreateAssetMenu(menuName = nameof(ColorCeilingDecorator), fileName = "New " + nameof(ColorCeilingDecorator))]
    public class ColorCeilingDecorator : TerrainDecorator
    {
        public float ceilingHeight;

        public override float GetColorCeiling(ref Mesh mesh)
        {
            float y = terrainable.GetColorCeiling(ref mesh);
            if (y > ceilingHeight)
                y = ceilingHeight;

            return y;
        }
    }
}