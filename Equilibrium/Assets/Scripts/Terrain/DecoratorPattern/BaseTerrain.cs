using UnityEngine;

namespace Equilibrium
{
    [CreateAssetMenu(menuName = "ScriptableObject/" + nameof(BaseTerrain), fileName = "New " + nameof(BaseTerrain))]
    public class BaseTerrain : ScriptableObject, ITerrainable, ITerrainableColorable
    {
        [Header("Terrain")]
        //public Biome biome;
        public float speedMod;
        public float waterHeight;
        public BaseBrush brush;
        public Color iconicColor; 
        public Color backgroundColor;

        [Header("Mesh")]
        [Min(1)] public int size;
        public SpawnData[] spawnDatas;
        public int GetSize() => size;
        public float GetWaterHeight() => waterHeight;

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
    }
}