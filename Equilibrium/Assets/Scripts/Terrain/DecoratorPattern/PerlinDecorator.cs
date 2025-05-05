using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OuterWilds
{
    [CreateAssetMenu(menuName = "ScriptableObject/" + nameof(PerlinDecorator), fileName = "New " + nameof(PerlinDecorator))]
    public class PerlinDecorator : TerrainDecorator
    {
        [Min(0f)] public float height;
        [Min(0f)] public float noiseScale;
        public float offsetX, offsetZ;

        public override float GetHeightAtPoint(float x, float z)
        {
            float perlinX = x + offsetX;
            float perlinZ = z + offsetZ;

            //Debug.Log(nameof(PerlinDecorator));

            return terrainable.GetHeightAtPoint(x, z) + Mathf.PerlinNoise(perlinX * noiseScale, perlinZ * noiseScale) * height;
        }
    }
}