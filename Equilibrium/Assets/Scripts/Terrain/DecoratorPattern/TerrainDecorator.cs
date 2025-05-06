using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Equilibrium
{
    /// <summary>
    /// https://www.youtube.com/watch?v=o5Iwu5wpINQ
    /// 
    /// It might be smart to seperate differennt vibes
    /// so like material and mesh are different for performance but whatever.
    /// </summary>
    public abstract class TerrainDecorator : ScriptableObject, ITerrainable
    {
        protected ITerrainable terrainable;

        /// <summary>
        /// This COULD also return a new Iterrainalbe which might make sense but now 
        /// it doesnt because its about the method huh.
        /// </summary>
        /// <param name="terrainable"></param>
        public TerrainDecorator Decorate(ITerrainable terrainable) 
        {
            // why do i need to disable this ffs?

            /*if (ReferenceEquals(this, terrainable))
                throw new InvalidOperationException("Cannot decorate self.");

            if (this.terrainable is TerrainDecorator decorator)
            {
                decorator.Decorate(terrainable);
                return this;
            }*/

            this.terrainable = terrainable;
            return this;
        }

        public virtual float GetHeightAtPoint(float x, float y, float z) => terrainable.GetHeightAtPoint(x, y, z);
        /*public virtual float GetColorFloor(float min) => terrainable.GetColorFloor(min);
        public virtual float GetColorCeiling(float max) => terrainable.GetColorCeiling(max);
        public virtual Texture2D GetTexture() => terrainable.GetTexture();*/

        // can make these virtual in the future.
        // im not that good about future specfualtion
        public Biome GetBiome() => terrainable.GetBiome();
        public int GetSize() => terrainable.GetSize();
        public float GetWaterHeight() => terrainable.GetWaterHeight();
    }
}