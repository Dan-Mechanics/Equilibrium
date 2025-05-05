using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace OuterWilds
{
    /// <summary>
    /// https://www.youtube.com/watch?v=o5Iwu5wpINQ
    /// </summary>
    public abstract class TerrainDecorator : ScriptableObject, ITerrainable
    {
        protected ITerrainable terrainable;

        /// <summary>
        /// This COULD also return a new Iterrainalbe which might make sense but now 
        /// it doesnt because its about the method huh.
        /// </summary>
        /// <param name="terrainable"></param>
        public void Decorate(ITerrainable terrainable) 
        {
            if (ReferenceEquals(this, terrainable))
                throw new InvalidOperationException("Cannot decorate self.");

            if (this.terrainable is TerrainDecorator decorator)
            {
                decorator.Decorate(terrainable);
                return;
            }

            this.terrainable = terrainable;
        }

        public virtual float GetHeightAtPoint(float x, float z) => terrainable.GetHeightAtPoint(x, z);
        public virtual float GetColorFloor(ref Mesh mesh) => terrainable.GetColorFloor(ref mesh);
        public virtual float GetColorCeiling(ref Mesh mesh) => terrainable.GetColorCeiling(ref mesh);

        // can make these virtual in the future.
        // im not that good about future specfualtion
        public Biome GetBiome() => terrainable.GetBiome();
        public int GetColorFidelity() => terrainable.GetColorFidelity();
        public Gradient GetGradient() => terrainable.GetGradient();
        public int GetSize() => terrainable.GetSize();
        public float GetWaterHeight() => terrainable.GetWaterHeight();
    }
}