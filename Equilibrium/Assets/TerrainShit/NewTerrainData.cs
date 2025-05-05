using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OuterWilds
{
    public class NewTerrainData : ITerrain
    {
        public enum Biome { Mesa, Icey }
        //public int sizeZ => sizeX;

        [Header("Terrain")]
        public Biome biome;
        public float waterHeight;

        [Header("Mesh")]
        [Min(1)] public int sizeX;
        [Min(1)] public int sizeZ;
        /*public float meshFloorHeight;
        [Min(0f)] public float meshCeilingHeight;*/

        /*[Header("Perlin")]
        [Min(0f)] public float height;
        [Min(0f)] public float noiseScale;
        public float offsetX, offsetZ;*/

        [Header("Color")]
        public Gradient gradient;
        [Min(1)] public int colorFidelity;
        /*[Min(0f)] public float colorFloorHeight;
        [Min(0f)] public float colorCeilingHeight;*/

        public float GetPerlin(float x, float z)
        {
            throw new System.NotImplementedException();
        }

        public float ClampHeight(float y)
        {
            throw new System.NotImplementedException();
        }
    }
}