using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Equilibrium
{
    [CreateAssetMenu(menuName = "ScriptableObject/" + nameof(BeachDecorator), fileName = "New " + nameof(BeachDecorator))]
    public class BeachDecorator : TerrainColorDecorator
    {
        /// <summary>
        /// Maybe add something abt waterheight ??
        /// </summary>
        public Color beachColor;
        [Range(0f, 1f)] public float minPercentage;
        [Range(0f, 1f)] public float maxPercentage;

        public override Texture2D GetTexture()
        {
            Texture2D src = base.GetTexture();
            Texture2D copyTexture = new Texture2D(src.width, src.height);
            copyTexture.filterMode = src.filterMode;
            copyTexture.SetPixels32(src.GetPixels32());
            copyTexture.Apply();

            int min = Mathf.FloorToInt(minPercentage * src.width);
            int max = Mathf.CeilToInt(maxPercentage * src.width);


            for (int i = min; i < max; i++)
            {
                copyTexture.SetPixel(i, 0, beachColor);
            }

            copyTexture.Apply();
            return copyTexture;
        }
    }
}