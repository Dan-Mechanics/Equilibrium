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

        public override void SetHeightTerraform(float x, ref float y, float z)
        {
            base.SetHeightTerraform(x, ref y, z);
            y = Mathf.Clamp(y, meshFloor, meshCeiling);
        }

        public override void SetHeightStartup(float x, ref float y, float z)
        {
            base.SetHeightStartup(x, ref y, z);
            y = Mathf.Clamp(y, meshFloor, meshCeiling);
        }
    }
}