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
        [SerializeField] private DecoratedTerrain[] maps = default;
        [SerializeField] private List<InspectorInterface<IWritable<ITerrainable>>> listeners = default;

        private ITerrainable terrainable;
        private int currentMap;

        private void Awake()
        {
            listeners.ForEach(x => x.Setup());
        }

        private void Start()
        {
            Refresh();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Refresh();
        }

        private void Refresh()
        {
            terrainable = maps[currentMap].baseTerrain;
            TerrainDecorator[] decorators = maps[currentMap].decorators;

            for (int i = decorators.Length - 1; i >= 0; i--)
            {
                terrainable = decorators[i].Decorate(terrainable);
            }

            listeners.ForEach(x => x.attached.Write(terrainable));
        }

        [System.Serializable]
        public class DecoratedTerrain 
        {
            public BaseTerrain baseTerrain;
            public TerrainDecorator[] decorators;
        }
    }
}