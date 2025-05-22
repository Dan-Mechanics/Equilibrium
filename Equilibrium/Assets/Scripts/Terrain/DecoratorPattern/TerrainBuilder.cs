using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Equilibrium
{
    /// <summary>
    /// TEMP NAME
    /// There's nothing more permanent than a temporary name.
    /// 
    /// This class's responsiblitties are a little large lol XD.
    /// But then it means i would need to make a new class that is like 
    /// an intermediary and that takes too much time i think right about now but maybe thats exactly what i need tho.
    /// </summary>
    public class TerrainBuilder : MonoBehaviour
    {
        [SerializeField] private int currentMap = default;
        [SerializeField] private DecoratedTerrain[] maps = default;

        [SerializeField] private List<InspectorInterface<IWritable<ITerrainable>>> terrainListeners = default;
        [SerializeField] private List<InspectorInterface<IWritable<ITerrainableColorable>>> colorListeners = default;
        [SerializeField] private List<InspectorInterface<IWritable<BaseTerrain>>> baseListeners = default;
        [SerializeField] private UnityEvent onRefresh = default;
        [SerializeField] private UnityEvent onUpperLimitReached = default;

        private void Awake()
        {
            terrainListeners.ForEach(x => x.Setup());
            colorListeners.ForEach(x => x.Setup());
            baseListeners.ForEach(x => x.Setup());

            /*MonoBehaviour[] monoBehaviours = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
            IWritable<ITerrainable>[] terrainables = FindObjectsByType<IWritable<ITerrainable>>(FindObjectsSortMode.None);*/
        }

        private void Refresh()
        {
            if (currentMap < 0)
                currentMap = 0;

            if (currentMap >= maps.Length) 
            {
                onUpperLimitReached?.Invoke();
                currentMap = maps.Length - 1;
                return;
            }

            BaseTerrain baseTerrain = maps[currentMap].baseTerrain;
            baseListeners.ForEach(x => x.attached.Write(baseTerrain));

            ITerrainable terrainable = baseTerrain;
            ITerrainableColorable colorable = baseTerrain;

            EventManager<TitleHandler.TitleMessage>.RaiseEvent(EventManager.EventType.TITLE,
                    new TitleHandler.TitleMessage(maps[currentMap].baseTerrain.name, maps[currentMap].baseTerrain.iconicColor, false));

            for (int i = 0; i < maps[currentMap].decorators.Length; i++)
            {
                terrainable = maps[currentMap].decorators[i].Decorate(terrainable);
            }

            for (int i = 0; i < maps[currentMap].colorDecorators.Length; i++)
            {
                colorable = maps[currentMap].colorDecorators[i].Decorate(colorable);
            }

            // The order of these is important !!
            colorListeners.ForEach(x => x.attached.Write(colorable));
            terrainListeners.ForEach(x => x.attached.Write(terrainable));

            onRefresh?.Invoke();
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

        [System.Serializable]
        public class DecoratedTerrain 
        {
            public BaseTerrain baseTerrain;
            public TerrainDecorator[] decorators;
            public TerrainColorDecorator[] colorDecorators;
        }
    }
}