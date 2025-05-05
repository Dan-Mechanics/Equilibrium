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

        public override float GetHeightAtPoint(float x, float y, float z)
        {
            float perlinX = x + offsetX;
            float perlinZ = z + offsetZ;

            return terrainable.GetHeightAtPoint(x, y + Mathf.PerlinNoise(perlinX * noiseScale, perlinZ * noiseScale) * height, z);
        }
    }
}