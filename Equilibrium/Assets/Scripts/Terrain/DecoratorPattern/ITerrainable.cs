using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Equilibrium
{
    /// <summary>
    /// Question for Aaron: does this break the interface segregation principle?
    /// I think im overthinking it in this context but in the future i could split this enum into 
    /// different types of shit.
    /// </summary>
    public interface ITerrainable 
    {
        /// <summary>
        /// This will mean we have to do some memes
        /// </summary>
        float GetHeightAtPoint(float x, float y, float z);

        /// <summary>
        /// COuld make this give meshfilter for offset ???
        /// </summary>
        /// <param name="mesh"></param>
        /// <returns></returns>
        /*float GetColorFloor(float min);
        float GetColorCeiling(float max);*/

        int GetSize();
        float GetWaterHeight();
        Biome GetBiome();
        //Texture2D GetTexture();
    }
}