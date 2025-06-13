using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Equilibrium
{
    public class TerrainBuilder : MonoBehaviour
    {
        [SerializeField] private int currentMap = default;
        [SerializeField] private List<Map> maps = default;

        [SerializeField] private List<MonoBehaviour> baseListenersMono = default;
        [SerializeField] private List<MonoBehaviour> terrainListenersMono = default;
        [SerializeField] private List<MonoBehaviour> colorListenersMono = default;

        [SerializeField] private UnityEvent onRefresh = default;
        [SerializeField] private UnityEvent onUpperLimitReached = default;

        private readonly List<IWritable<ITerrainable>> terrainListeners = new();
        private readonly List<IWritable<BaseTerrain>> baseListeners = new();
        private readonly List<IWritable<ITerrainableColorable>> colorListeners = new();
        
        private void Awake()
        {
            Utils.PipeTo(terrainListenersMono, terrainListeners);
            Utils.PipeTo(baseListenersMono, baseListeners);
            Utils.PipeTo(colorListenersMono, colorListeners);
        }

        private void Refresh()
        {
            if (currentMap < 0)
                currentMap = 0;

            if (currentMap >= maps.Count)
            {
                onUpperLimitReached?.Invoke();
                currentMap = maps.Count - 1;
                return;
            }

            InitializeNewMap(currentMap);
            onRefresh?.Invoke();
        }

        private void InitializeNewMap(int index)
        {
            BaseTerrain baseTerrain = maps[index].baseTerrain;
            baseListeners.ForEach(x => x.Write(baseTerrain));

            ITerrainable terrainable = baseTerrain;
            ITerrainableColorable colorable = baseTerrain;

            EventManager<TitleHandler.TitleMessage>.RaiseEvent(EventManager.EventType.TITLE,
                    new TitleHandler.TitleMessage(maps[index].baseTerrain.name, maps[index].baseTerrain.iconicColor, false));

            for (int i = 0; i < maps[index].decorators.Count; i++)
            {
                terrainable = maps[index].decorators[i].Decorate(terrainable);
            }

            for (int i = 0; i < maps[index].colorDecorators.Count; i++)
            {
                colorable = maps[index].colorDecorators[i].Decorate(colorable);
            }

            // The order of these is important !!
            colorListeners.ForEach(x => x.Write(colorable));
            terrainListeners.ForEach(x => x.Write(terrainable));
        }

        public void GoPreviousMap()
        {
            currentMap--;
            Refresh();
        }

        public void GoNextMap() 
        {
            currentMap++;
            Refresh();
        }
    }
}