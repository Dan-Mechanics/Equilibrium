using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Equilibrium
{
    /// <summary>
    /// TEMP NAME
    /// </summary>
    public class TerrainBuilder : MonoBehaviour
    {
        [SerializeField] private DecoratedTerrain[] maps = default;
        [SerializeField] private List<InspectorInterface<IWritable<ITerrainable>>> terrainListeners = default;
        [SerializeField] private List<InspectorInterface<IWritable<ITerrainableColorable>>> colorListeners = default;

        private ITerrainable terrainable;
        private ITerrainableColorable colorable;
        private int currentMap;

        private void Awake()
        {
            terrainListeners.ForEach(x => x.Setup());
            colorListeners.ForEach(x => x.Setup());
        }

        private void Start()
        {
            Refresh();
        }

        private void Refresh()
        {
            terrainable = maps[currentMap].baseTerrain;
            colorable = maps[currentMap].baseTerrain;

            for (int i = maps[currentMap].decorators.Length - 1; i >= 0; i--)
            {
                terrainable = maps[currentMap].decorators[i].Decorate(terrainable);
            }

            for (int i = maps[currentMap].colorDecorators.Length - 1; i >= 0; i--)
            {
                colorable = maps[currentMap].colorDecorators[i].Decorate(colorable);
            }

            // the order of these is important !!
            colorListeners.ForEach(x => x.attached.Write(colorable));
            terrainListeners.ForEach(x => x.attached.Write(terrainable));
        }

        [System.Serializable]
        public class DecoratedTerrain 
        {
            public BaseTerrain baseTerrain;
            public TerrainDecorator[] decorators;
            public TerrainColorDecorator[] colorDecorators;
        }
    }
}