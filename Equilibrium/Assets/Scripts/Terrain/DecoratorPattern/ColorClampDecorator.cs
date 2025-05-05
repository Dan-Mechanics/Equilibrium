using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OuterWilds
{
    [CreateAssetMenu(menuName = "ScriptableObject/" + nameof(ColorClampDecorator), fileName = "New " + nameof(ColorClampDecorator))]
    public class ColorClampDecorator : TerrainDecorator
    {
        /// <summary>
        /// TODO: do in script.
        /// </summary>
        public MeshClampDecorator.ConstraintType constraintType;
        public float height;

        public override float GetColorCeiling(ref Mesh mesh)
        {
            float y = terrainable.GetColorCeiling(ref mesh);

            if (constraintType != MeshClampDecorator.ConstraintType.Ceiling)
                return y;
            
            if (y > height)
                y = height;

            return y;
        }

        public override float GetColorFloor(ref Mesh mesh)
        {
            float y = terrainable.GetColorFloor(ref mesh);

            if (constraintType != MeshClampDecorator.ConstraintType.Floor)
                return y;

            if (y < height)
                y = height;

            return y;
        }
    }
}