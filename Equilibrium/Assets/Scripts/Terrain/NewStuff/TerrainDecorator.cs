using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OuterWilds
{
    public abstract class TerrainDecorator : ScriptableObject, ITerrain
    {
        protected ITerrain wrappedTerrain;

        public void Decorate(ITerrain wrappedTerrain) 
        {
            this.wrappedTerrain = wrappedTerrain;
        }

        public virtual float ClampHeight(float y)
        {
            return wrappedTerrain.ClampHeight(y);
        }

        public virtual float GetPerlin(float x, float z)
        {
            return wrappedTerrain.GetPerlin(x, z);
        }
    }
}