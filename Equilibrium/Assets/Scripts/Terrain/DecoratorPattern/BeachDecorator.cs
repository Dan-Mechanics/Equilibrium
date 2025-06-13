using UnityEngine;

namespace Equilibrium
{
    [CreateAssetMenu(menuName = "ScriptableObject/" + nameof(BeachDecorator), fileName = "New " + nameof(BeachDecorator))]
    public class BeachDecorator : TerrainColorDecorator
    {
        public Color beachColor;
        [Range(0f, 1f)] public float minPercentage;
        [Range(0f, 1f)] public float maxPercentage;

        public override Texture2D GetTexture()
        {
            Texture2D src = base.GetTexture();
            Texture2D copyTexture = new Texture2D(src.width, src.height);
            copyTexture.filterMode = src.filterMode;
            copyTexture.wrapMode = src.wrapMode;
            copyTexture.SetPixels32(src.GetPixels32());

            int min = Mathf.FloorToInt(minPercentage * src.width);
            int max = Mathf.FloorToInt(maxPercentage * src.width);
            for (int i = min; i <= max; i++)
            {
                copyTexture.SetPixel(i, 0, beachColor);
            }

            //copyTexture.SetPixel(0, 0, beachColor);

            copyTexture.Apply();
            return copyTexture;
        }
    }
}