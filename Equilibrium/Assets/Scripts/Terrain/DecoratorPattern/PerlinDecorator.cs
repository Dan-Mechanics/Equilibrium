using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OuterWilds
{
    //[CreateAssetMenu(menuName = "PerlinDecorator")]
    public class PerlinDecorator : TerrainMeshDecorator
    {
        public float height;
        public float noiseScale;
        public float offsetX, offsetZ;

        public override float GetVertexHeight(float x, float y, float z)
        {
            float perlinx = x + offsetX;
            float perlinz = z + offsetZ;
            y += Mathf.PerlinNoise(perlinx * noiseScale, perlinz * noiseScale) * height;

            return wrappedTerrain.GetVertexHeight(x, y, z);
        }
    }
}