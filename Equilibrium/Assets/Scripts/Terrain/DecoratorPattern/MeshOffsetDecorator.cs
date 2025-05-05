using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OuterWilds
{
    [CreateAssetMenu(menuName = "ScriptableObject/" + nameof(MeshOffsetDecorator), fileName = "New " + nameof(MeshOffsetDecorator))]
    public class MeshOffsetDecorator : TerrainDecorator
    {
        public float verticalOffset;

        public override float GetHeightAtPoint(float x, float z)
        {
            //Debug.Log(nameof(MeshOffsetDecorator));

            return terrainable.GetHeightAtPoint(x, z) + verticalOffset;
        }
    }
}