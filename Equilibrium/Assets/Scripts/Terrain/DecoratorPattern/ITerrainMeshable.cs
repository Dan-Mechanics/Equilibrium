using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OuterWilds
{
    public interface ITerrainMeshable 
    {
        float GetVertexHeight(float x, float y, float z);
    }
}