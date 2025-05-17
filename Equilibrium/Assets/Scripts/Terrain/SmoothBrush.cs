using UnityEngine;

namespace Equilibrium
{
    [CreateAssetMenu(menuName = "ScriptableObject/" + nameof(SmoothBrush), fileName = "New " + nameof(SmoothBrush))]
    public class SmoothBrush : BaseBrush
    {
        public override float GetBrushMod(float dist, float brushSize)
        {
            dist = 1f - (dist / Utils.Root(brushSize));
            dist *= 1.5f;

            return dist;
        }
    }
}