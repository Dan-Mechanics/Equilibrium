using UnityEngine;

namespace Equilibrium
{
    [CreateAssetMenu(menuName = "ScriptableObject/" + nameof(BaseTerrain), fileName = "New " + nameof(BaseTerrain))]
    public class BaseTerrain : ScriptableObject, ITerrainable, ITerrainableColorable
    {
        [Header("Terrain")]
        public Biome biome;
        public float waterHeight;
        //public float introductionTime;
        public Color iconicColor; // itnroduciton coloir.
        public Color backgroundColor;

        [Header("Mesh")]
        [Min(1)] public int size;
        public SpawnData[] spawnDatas;

        public int GetSize() => size;
        public float GetWaterHeight() => waterHeight;
        // public Biome GetBiome() => biome;

        /// <summary>
        /// Base layer.
        /// </summary>
        //public void SetHeightAtPoint(float x, ref float y, float z) { }

        /// <summary>
        /// idk how i feel about this but whatever.
        /// </summary>
        public float GetColorFloor(float min)
        {
            return min;
        }

        public float GetColorCeiling(float max)
        {
            return max;
        }

        public Texture2D GetTexture() => null;

        public void SetHeightStartup(float x, ref float y, float z) { }

        public void SetHeightTerraform(float x, ref float y, float z) { }

        //public Color GetIntroductionColor() => Color.clear;
    }
}