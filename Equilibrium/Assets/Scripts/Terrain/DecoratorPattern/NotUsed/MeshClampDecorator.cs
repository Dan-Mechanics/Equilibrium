using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Equilibrium
{
    [CreateAssetMenu(menuName = "ScriptableObject/" + nameof(MeshClampDecorator), fileName = "New " + nameof(MeshClampDecorator))]
    public class MeshClampDecorator : TerrainDecorator
    {
        public ConstraintType constraintType;
        public float height;

        public override void SetHeightTerraform(float x, ref float y, float z)
        {
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

            //return terrainable.GetHeightAtPoint(x, y, z);
        }
    }
}