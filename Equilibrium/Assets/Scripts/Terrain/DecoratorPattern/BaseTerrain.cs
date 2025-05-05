using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OuterWilds
{
    [CreateAssetMenu(menuName = "ScriptableObject/" + nameof(BaseTerrain), fileName = "New " + nameof(BaseTerrain))]
    public class BaseTerrain : ScriptableObject, ITerrainable
    {
        [Header("Terrain")]
        public Biome biome;
        public float waterHeight;

        [Header("Mesh")]
        [Min(1)] public int size;
        //[Min(1)] public int sizeZ;

        [Header("Color")]
        public Gradient gradient;
        [Min(1)] public int colorFidelity;

        public int GetSize() => size;

        public float GetWaterHeight() => waterHeight;

        public Biome GetBiome() => biome;

        /// <summary>
        /// Base layer.
        /// </summary>
        public float GetHeightAtPoint(float x, float z)
        {
            return 0f;
        }

        /// <summary>
        /// idk how i feel about this but whatever.
        /// </summary>
        public float GetColorFloor(ref Mesh mesh)
        {
            return mesh.bounds.min.y;
        }

        public float GetColorCeiling(ref Mesh mesh)
        {
            return mesh.bounds.max.y;
        }

        public Gradient GetGradient() => gradient;

        public int GetColorFidelity() => colorFidelity;
    }
}