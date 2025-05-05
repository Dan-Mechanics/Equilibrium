using System;
using UnityEngine;

namespace OuterWilds
{
    /// <summary>
    /// This feels like it needs decorator or something idk.
    /// Good for now tho ..
    /// MAKE DECORATOR FOR MEME !!
    /// https://www.youtube.com/watch?v=o5Iwu5wpINQ
    /// </summary>
    [CreateAssetMenu(menuName = "TerrainData")]
    public class TerrainData : ScriptableObject, IDimensionsReadable
    {
        public enum Biome { Mesa, Icey }
        //public int sizeZ => sizeX;

        [Header("Terrain")]
        public Biome biome;
        public float waterHeight;

        [Header("Mesh")]
        [Min(1)] public int sizeX;
        [Min(1)] public int sizeZ;
        public float meshFloorHeight;
        [Min(0f)] public float meshCeilingHeight;

        [Header("Perlin")]
        [Min(0f)] public float height;
        [Min(0f)] public float noiseScale;
        public float offsetX, offsetZ;

        [Header("Color")]
        public Gradient gradient;
        [Min(1)] public int colorFidelity;
        [Min(0f)] public float colorFloorHeight;
        [Min(0f)] public float colorCeilingHeight;

        // maybe remove all these methods ??

        public float GetPerlinHeight(float perlinX, float perlinZ)
        {   
            perlinX += offsetX;
            perlinZ += offsetZ;

            float result = Mathf.PerlinNoise(perlinX * noiseScale, perlinZ * noiseScale) * height;
            return ClampTerrainHeight(result);
        }

        public float ClampTerrainHeight(float result)
        {
            return Mathf.Clamp(result, meshFloorHeight, meshCeilingHeight > 0f ? meshCeilingHeight : result);
        }

        public Vector3 GetRandomPointOnTerrian(float y = 0f) 
        {
            return new Vector3(
                UnityEngine.Random.Range(-sizeX / 2f, sizeX / 2f),
                y, 
                UnityEngine.Random.Range(sizeZ / 2f, sizeZ / 2f));
        }

        public float GetSizeX() => sizeX;

        public float GetSizeZ() => sizeX;
    }
}