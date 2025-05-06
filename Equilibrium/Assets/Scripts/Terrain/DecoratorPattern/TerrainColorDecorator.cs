using UnityEngine;

namespace Equilibrium
{
    public abstract class TerrainColorDecorator : ScriptableObject, ITerrainableColorable
    {
        protected ITerrainableColorable colorable;

        public TerrainColorDecorator Decorate(ITerrainableColorable colorable) 
        {
            this.colorable = colorable;
            return this;
        }

        // or the default should be checking if its null would be better maybe.
        public virtual float GetColorFloor(float min) => colorable.GetColorFloor(min);
        public virtual float GetColorCeiling(float max) => colorable.GetColorCeiling(max);
        public virtual Texture2D GetTexture() => colorable.GetTexture();
    }
}