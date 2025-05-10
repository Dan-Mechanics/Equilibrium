using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Equilibrium
{
    [CreateAssetMenu(menuName = "ScriptableObject/" + nameof(PerlinDecorator), fileName = "New " + nameof(PerlinDecorator))]
    public class PerlinDecorator : TerrainDecorator
    {
        [Min(0f)] public float height;
        [Min(0f)] public float noiseScale;
        public float offsetX, offsetZ;

        public override void SetHeightAtPoint(float x, ref float y, float z)
        {
            float perlinX = x + offsetX;
            float perlinZ = z + offsetZ;

            y += Mathf.PerlinNoise(perlinX * noiseScale, perlinZ * noiseScale) * height;
        }
    }
}