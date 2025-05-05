using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OuterWilds
{
    public abstract class TerrainMeshDecorator : ITerrainMeshable
    {
        protected ITerrainMeshable wrappedTerrain;

        public void Decorate(ITerrainMeshable wrappedTerrain) 
        {
            this.wrappedTerrain = wrappedTerrain;
        }

        public virtual float GetVertexHeight(float x, float y, float z)
        {
            return wrappedTerrain.GetVertexHeight(x, y, z);
        }
    }
}