using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Equilibrium
{
    /// <summary>
    /// Question for Aaron: does this break the interface segregation principle?
    /// I think im overthinking it in this context but in the future i could split this enum into 
    /// different types of shit.
    /// 
    /// Maybe make different method for terraform and perlin??
    /// </summary>
    public interface ITerrainable 
    {
        /// <summary>
        /// This will mean we have to do some memes
        /// 
        /// Maybe use REF y pos instead of return ??
        /// </summary>
        void SetHeightStartup(float x, ref float y, float z);
        void SetHeightTerraform(float x, ref float y, float z);
        int GetSize();
        float GetWaterHeight();
        //Biome GetBiome();
    }
}