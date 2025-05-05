using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OuterWilds
{
    [CreateAssetMenu(menuName = nameof(MeshClampDecorator), fileName = "New " + nameof(MeshClampDecorator))]
    public class MeshClampDecorator : TerrainDecorator
    {
        public enum ConstraintType { None = 0, Floor = 1, Ceiling = 2 }

        public ConstraintType constraintType;
        public float height;

        public override float GetHeightAtPoint(float x, float z)
        {
            float y = terrainable.GetHeightAtPoint(x, z);
            
            switch (constraintType)
            {
                case ConstraintType.Floor:
                    if (y < height)
                        y = height;
                    break;
                case ConstraintType.Ceiling:
                    if (y > height)
                        y = height;
                    break;
                default:
                    break;
            }

            return y;
        }
    }
}