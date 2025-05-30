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
        public int min;
        public int max;

        public override Texture2D GetTexture()
        {
            Texture2D tex = null;
            Graphics.CopyTexture(base.GetTexture(), tex);
            
            for (int i = min; i <= max; i++)
            {
                tex.SetPixel(i, 0, beachColor);
            }

            tex.Apply();
            return tex;
        }
    }
}