using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OuterWilds
{
    [CreateAssetMenu(menuName = "PerlinDecorator")]
    public class PerlinDecorator : TerrainDecorator
    {
        public float height;
        public float noiseScale;
        public float offsetX, offsetZ;

        public override float GetPerlin(float x, float z)
        {
            x += offsetX;
            z += offsetZ;

            return Mathf.PerlinNoise(x * noiseScale, z * noiseScale) * height;
        }
    }
}