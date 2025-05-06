using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Equilibrium
{
    [CreateAssetMenu(menuName = "ScriptableObject/" + nameof(TerrainCrunchDecorator), fileName = "New " + nameof(TerrainCrunchDecorator))]
    public class TerrainCrunchDecorator : TerrainDecorator
    {
        public float meshFloor;        // min max etc etc
        public float meshCeiling;      // min max etc etc

        public override float GetHeightAtPoint(float x, float y, float z)
        {
            return base.GetHeightAtPoint(x, Mathf.Clamp(y, meshFloor, meshCeiling), z);
        }
    }
}