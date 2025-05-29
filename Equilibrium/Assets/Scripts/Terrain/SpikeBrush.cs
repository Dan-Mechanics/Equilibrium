using UnityEngine;

namespace Equilibrium
{
    [CreateAssetMenu(menuName = "ScriptableObject/" + nameof(SpikeBrush), fileName = "New " + nameof(SpikeBrush))]
    public class SpikeBrush : BaseBrush
    {
        public override float GetBrushMod(float dist, float brushSize)
        {
            float result = 1f;
            if (dist < 3f)
                result += 0.3f;

            return 1f + result;
        }
    }
}