using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OuterWilds
{
    public abstract class TerrainDecorator : ScriptableObject, ITerrainable
    {
        protected ITerrainable terrainable;

        public void Decorate(ITerrainable terrainable) 
        {
            this.terrainable = terrainable;
        }

        public virtual float GetHeightAtPoint(float x, float z) => terrainable.GetHeightAtPoint(x, z);

        public virtual float GetColorFloor(ref Mesh mesh) => terrainable.GetColorFloor(ref mesh);
        public virtual float GetColorCeiling(ref Mesh mesh) => terrainable.GetColorCeiling(ref mesh);

        public virtual Biome GetBiome() => terrainable.GetBiome();
        public virtual int GetColorFidelity() => terrainable.GetColorFidelity();
        public virtual Gradient GetGradient() => terrainable.GetGradient();
        public virtual float GetSizeX() => terrainable.GetSizeX();
        public virtual float GetSizeZ() => terrainable.GetSizeZ();
        public virtual float GetWaterHeight() => terrainable.GetWaterHeight();
    }
}