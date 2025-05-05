using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OuterWilds
{
    public interface ITerrainColorable 
    {
        float GetColorFloor(ref Mesh mesh);
        float GetColorCeiling(ref Mesh mesh);
    }
}