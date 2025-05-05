using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OuterWilds
{
    public interface ITerrainable 
    {
        /// <summary>
        /// This will mean we have to do some memes
        /// </summary>
        float GetHeightAtPoint(float x, float z);

        /// <summary>
        /// COuld make this give meshfilter for offset ???
        /// </summary>
        /// <param name="mesh"></param>
        /// <returns></returns>
        float GetColorFloor(ref Mesh mesh);
        float GetColorCeiling(ref Mesh mesh);

        float GetSizeX();
        float GetSizeZ();
        float GetWaterHeight();
        Biome GetBiome();
        Gradient GetGradient();
        int GetColorFidelity();
    }
}