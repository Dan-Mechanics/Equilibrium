using UnityEngine;

namespace Equilibrium
{
    [CreateAssetMenu(menuName = "ScriptableObject/" + nameof(MixedBrush), fileName = "New " + nameof(MixedBrush))]
    public class MixedBrush : BaseBrush
    {
        /*public override float GetBrushMod(float dist, float brushSize)
        {
            float result = 1f;
            if (dist < 3f)
                result += 0.3f;

            return (1f + result + _GetBrushMod(dist, brushSize)) / 2f;
        }*/

        public override float GetBrushMod(float dist, float brushSize)
        {
            dist = 1f - (dist / Utils.Root(brushSize));
            dist *= 1.32f;

            if (dist < 3f)
                dist += 0.6f;

            return dist;
        }
    }
}