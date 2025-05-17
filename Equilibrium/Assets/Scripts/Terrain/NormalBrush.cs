using UnityEngine;

namespace Equilibrium
{
    [CreateAssetMenu(menuName = "ScriptableObject/" + nameof(NormalBrush), fileName = "New " + nameof(NormalBrush))]
    public class NormalBrush : BaseBrush
    {
        public override float GetBrushMod(float dist, float brushSize)
        {
            return 1f;
        }
    }
}