using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OuterWilds
{
    //[CreateAssetMenu(menuName = "PerlinDecorator")]
    public class MeshCeilingDecorator : TerrainMeshDecorator
    {
        public enum ConstraintType { None = 0, Floor = 1, Ceiling = 2 }
        
        public ConstraintType constraintType;
        public float height;

        public override float GetVertexHeight(float x, float y, float z)
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

            return wrappedTerrain.GetVertexHeight(x, y, z);
        }
    }
}