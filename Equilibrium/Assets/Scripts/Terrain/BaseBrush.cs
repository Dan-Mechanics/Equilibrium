using UnityEngine;

namespace Equilibrium
{
    public abstract class BaseBrush : ScriptableObject 
    {
        public abstract float GetBrushMod(float dist, float brushSize);
    }
}