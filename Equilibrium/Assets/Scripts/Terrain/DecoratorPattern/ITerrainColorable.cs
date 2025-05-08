using UnityEngine;

namespace Equilibrium
{
    public interface ITerrainableColorable 
    {
        Texture2D GetTexture();
        float GetColorFloor(float min);
        float GetColorCeiling(float max);
        //Color GetIntroductionColor();
    }
}