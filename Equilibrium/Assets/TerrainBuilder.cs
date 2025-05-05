using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OuterWilds
{
    /// <summary>
    /// TEMP NAME
    /// </summary>
    public class TerrainBuilder : MonoBehaviour
    {
        // but then expand this to supprot multible terrians for mesa and icy.
        [SerializeField] private BaseTerrain terrainBase = default;
        [SerializeField] private TerrainDecorator[] decorators = default;

        private ITerrainable terrainable;

        [SerializeField] private List<InspectorInterface<IWritable<ITerrainable>>> listeners = default;

        private void Awake()
        {
            listeners.ForEach(x => x.Setup());
        }

        private void Start()
        {
            terrainable = terrainBase;
            
            for (int i = 0; i < decorators.Length; i++)
            {
                decorators[i].Decorate(terrainable);
                terrainable = decorators[i];
            }

            listeners.ForEach(x => x.attached.Write(terrainable));
        }
    }
}