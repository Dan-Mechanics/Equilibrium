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

        //public void SetNext(ITerrainable) { }

        public virtual void SetHeightTerraform(float x, ref float y, float z) => terrainable.SetHeightTerraform(x, ref y, z);
        public virtual void SetHeightStartup(float x, ref float y, float z) => terrainable.SetHeightStartup(x, ref y, z);
        public int GetSize() => terrainable.GetSize();
        public float GetWaterHeight() => terrainable.GetWaterHeight();
    }
}