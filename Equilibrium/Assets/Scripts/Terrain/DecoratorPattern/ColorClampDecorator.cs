using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Equilibrium
{
    [CreateAssetMenu(menuName = "ScriptableObject/" + nameof(ColorClampDecorator), fileName = "New " + nameof(ColorClampDecorator))]
    public class ColorClampDecorator : TerrainDecorator
    {
        /// <summary>
        /// TODO: do in script.
        /// </summary>
        public ConstraintType constraintType;
        public float height;

        public override float GetColorFloor(float min)
        {
            float y = terrainable.GetColorFloor(min);

            if (constraintType != ConstraintType.Floor)
                return y;

            if (y < height)
                y = height;

            return y;
        }

        public override float GetColorCeiling(float max)
        {
            float y = terrainable.GetColorCeiling(max);

            if (constraintType != ConstraintType.Ceiling)
                return y;
            
            if (y > height)
                y = height;

            return y;
        }
    }
}