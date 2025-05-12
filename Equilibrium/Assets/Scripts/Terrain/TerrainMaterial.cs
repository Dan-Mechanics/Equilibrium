using UnityEngine;

namespace Equilibrium
{
    /// <summary>
    /// Need to make something that spawns in the borders so they cant run off the map.
    /// </summary>
    //[RequireComponent(typeof(Renderer))]
    public class TerrainMaterial : MonoBehaviour, IWritable<ITerrainableColorable>, IWritable<float, float>
    {
        [SerializeField] private Material material = default;
        [SerializeField] private Renderer _renderer = default;
        [SerializeField] private bool keepUpdatingShader = default;

        private ITerrainableColorable colorable;
        private bool hasBeenUpdated;

        private void Awake() => _renderer.material = material;

        public void Write(ITerrainableColorable terrainable)
        {
            this.colorable = terrainable;
            UpdateShaderTexture();
            UpdateShaderBounds();
        }

        public void Write(float min, float max) => UpdateShaderBounds(min, max);

        private void UpdateShaderTexture() 
        {
            Texture2D texture = colorable.GetTexture();

            if (texture == null)
                return;

            material.SetTexture("_Texture", texture);
        }

        private void UpdateShaderBounds(float min, float max) 
        {
            if (!keepUpdatingShader && hasBeenUpdated)
                return;

            hasBeenUpdated = true;

            min = colorable.GetColorFloor(min);
            max = colorable.GetColorCeiling(max);

            /*print(min);
            print(max);*/

            material.SetFloat("_WorldFloorHeight", min);
            material.SetFloat("_WorldCeilingHeight", max);
        }
    }
}