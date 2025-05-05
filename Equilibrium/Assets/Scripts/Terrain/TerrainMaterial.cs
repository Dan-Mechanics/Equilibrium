using UnityEngine;

namespace OuterWilds
{
    /// <summary>
    /// Need to make something that spawns in the borders so they cant run off the map.
    /// </summary>
    [RequireComponent(typeof(Renderer))]
    public class TerrainMaterial : MonoBehaviour, IWritable<ITerrainable, Mesh>
    {
        [SerializeField] private Material material = default;

        private void Awake() => GetComponent<Renderer>().material = material;

        public void Write(ITerrainable terrainable, Mesh mesh)
        {
            UpdateShaderProperties(terrainable, ref mesh);
        }

        private void UpdateShaderProperties(ITerrainable terrainable, ref Mesh mesh) 
        {
            material.SetFloat("_WorldFloorHeight", terrainable.GetColorFloor(ref mesh));
            material.SetFloat("_WorldCeilingHeight", terrainable.GetColorFloor(ref mesh));

            material.SetTexture("_Texture", MakeTerrainTexture(terrainable));
        }

        private Texture2D MakeTerrainTexture(ITerrainable terrainable) 
        {
            Texture2D texture = new Texture2D(terrainable.GetColorFidelity(), 1)
            {
                filterMode = FilterMode.Point,
                wrapMode = TextureWrapMode.Clamp
            };

            for (int i = 0; i < texture.width; i++)
            {
                texture.SetPixel(i, 0, terrainable.GetGradient().Evaluate((float)i / terrainable.GetColorFidelity()));
            }

            texture.Apply();

            return texture;
        }
    }
}