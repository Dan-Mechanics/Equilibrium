using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OuterWilds
{
    public interface ITerrain 
    {
        float GetPerlin(float x, float z);
        float ClampHeight(float y);
    }
}