using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Equilibrium
{
    [CreateAssetMenu(menuName = "ScriptableObject/" + nameof(GradientDecorator), fileName = "New " + nameof(GradientDecorator))]
    public class GradientDecorator : TerrainDecorator
    {
        [Header("Gradient")]
        public Gradient gradient;
        [Min(1)] public int colorFidelity;

        public override Texture2D GetTexture()
        {
            Texture2D texture = new Texture2D(colorFidelity, 1)
            {
                filterMode = FilterMode.Point,
                wrapMode = TextureWrapMode.Clamp
            };

            for (int i = 0; i < texture.width; i++)
            {
                texture.SetPixel(i, 0, gradient.Evaluate((float)i / colorFidelity));
            }

            texture.Apply();

            return texture;
        }
    }
}