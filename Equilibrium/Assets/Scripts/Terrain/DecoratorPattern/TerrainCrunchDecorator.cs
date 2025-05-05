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
        public float colorFloor;       // min max etc etc
        public float colorCeiling;     // min max etc etc

        public override float GetHeightAtPoint(float x, float y, float z)
        {
            return base.GetHeightAtPoint(x, Mathf.Clamp(y, meshFloor, meshCeiling), z);
        }

        public override float GetColorCeiling(ref Mesh mesh)
        {
            return colorCeiling;
        }

        public override float GetColorFloor(ref Mesh mesh)
        {
            return colorFloor;
        }
    }
}