using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Equilibrium
{
    [CreateAssetMenu(menuName = "ScriptableObject/" + nameof(MeshOffsetDecorator), fileName = "New " + nameof(MeshOffsetDecorator))]
    public class MeshOffsetDecorator : TerrainDecorator
    {
        public float verticalOffset;

        public override void SetHeightTerraform(float x, ref float y, float z)
        {
            y += verticalOffset;
        }
    }
}