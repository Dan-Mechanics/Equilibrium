using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Equilibrium
{
    [CreateAssetMenu(menuName = "ScriptableObject/" + nameof(TextureDecorator), fileName = "New " + nameof(TextureDecorator))]
    public class TextureDecorator : TerrainColorDecorator
    {
        [Header("Texture2D")]
        public Texture2D texture;

        public override Texture2D GetTexture()
        {
            return texture;
        }
    }
}