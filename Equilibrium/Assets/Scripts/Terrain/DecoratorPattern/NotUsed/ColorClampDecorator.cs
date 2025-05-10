using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Equilibrium
{
    [CreateAssetMenu(menuName = "ScriptableObject/" + nameof(ColorClampDecorator), fileName = "New " + nameof(ColorClampDecorator))]
    public class ColorClampDecorator : TerrainColorDecorator
    {
        /// <summary>
        /// TODO: do in script.
        /// </summary>
        public ConstraintType constraintType;
        public float height;
        
        /// <summary>
        /// COuld make this give meshfilter for offset ??? or somerthing ish.
        /// i think it would be more about setting the min max meme.
        /// </summary>
        /// <param name="mesh"></param>
        /// <returns></returns>
        public override float GetColorFloor(float min)
        {
            float y = colorable.GetColorFloor(min);

            if (constraintType != ConstraintType.Floor)
                return y;

            if (y < height)
                y = height;

            return y;
        }

        public override float GetColorCeiling(float max)
        {
            float y = colorable.GetColorCeiling(max);

            if (constraintType != ConstraintType.Ceiling)
                return y;
            
            if (y > height)
                y = height;

            return y;
        }
    }
}