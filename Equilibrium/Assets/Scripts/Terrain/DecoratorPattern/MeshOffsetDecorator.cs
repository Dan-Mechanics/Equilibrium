using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Equilibrium
{
    [CreateAssetMenu(menuName = "ScriptableObject/" + nameof(MeshOffsetDecorator), fileName = "New " + nameof(MeshOffsetDecorator))]
    public class MeshOffsetDecorator : TerrainDecorator
    {
        public float verticalOffset;

        public override float GetHeightAtPoint(float x, float y, float z)
        {
            return terrainable.GetHeightAtPoint(x, y + verticalOffset, z);
        }
    }
}