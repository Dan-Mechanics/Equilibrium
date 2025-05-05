using UnityEngine;

namespace OuterWilds
{
    /// <summary>
    /// Need to make something that spawns in the borders so they cant run off the map.
    /// </summary>
    [RequireComponent(typeof(Renderer))]
    public class TerrainMaterial : MonoBehaviour
    {
        [SerializeField] private TerrainData data = default;
        [SerializeField] private Material material = default;

        private Texture2D texture;

        private void Start()
        {
            GetComponent<Renderer>().material = material;

            MakeTexture();

            UpdateShaderProperties(data.colorFloorHeight, data.colorCeilingHeight);

            // or keep it if u wanna change materials in the future !!
            //Destroy(this);
        }

        private void MakeTexture() 
        {
            texture = new Texture2D(data.colorFidelity, 1)
            {
                filterMode = FilterMode.Point,
                wrapMode = TextureWrapMode.Clamp
            };

            for (int i = 0; i < texture.width; i++)
            {
                texture.SetPixel(i, 0, data.gradient.Evaluate((float)i / data.colorFidelity));
            }

            texture.Apply();
        }

        private void UpdateShaderProperties(float floor, float ceiling) 
        {
            /*if (floor > ceiling)
                floor = ceiling;*/

            material.SetFloat("_WorldFloorHeight", floor);
            material.SetFloat("_WorldCeilingHeight", ceiling);

            /*if (useGradient)
                WriteGradientTexture();*/

            material.SetTexture("_Texture", texture);
        }
    }
}