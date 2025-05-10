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

        public override void SetHeightAtPoint(float x, ref float y, float z)
        {
            y = Mathf.Clamp(y, meshFloor, meshCeiling);
        }
    }
}