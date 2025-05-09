using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Equilibrium
{
    /// <summary>
    /// TEMP NAME
    /// </summary>
    public class TerrainBuilder : MonoBehaviour
    {
        [SerializeField] private int currentMap = default;
        //[SerializeField] private float introductionTime = default;
        [SerializeField] private DecoratedTerrain[] maps = default;
        [SerializeField] private List<InspectorInterface<IWritable<ITerrainable>>> terrainListeners = default;
        [SerializeField] private List<InspectorInterface<IWritable<ITerrainableColorable>>> colorListeners = default;
        [SerializeField] private List<InspectorInterface<IWritable<BaseTerrain>>> baseListeners = default;
        [SerializeField] private UnityEvent onRefresh = default;

        private ITerrainable terrainable;
        private ITerrainableColorable colorable;

        private void Awake()
        {
            terrainListeners.ForEach(x => x.Setup());
            colorListeners.ForEach(x => x.Setup());
            baseListeners.ForEach(x => x.Setup());

            /*MonoBehaviour[] monoBehaviours = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
            IWritable<ITerrainable>[] terrainables = FindObjectsByType<IWritable<ITerrainable>>(FindObjectsSortMode.None);*/
        }

        private void Start() => Refresh();

        private void Refresh()
        {
            if (currentMap < 0 || currentMap >= maps.Length)
                return;

            onRefresh?.Invoke();
            baseListeners.ForEach(x => x.attached.Write(maps[currentMap].baseTerrain));

            terrainable = maps[currentMap].baseTerrain;
            colorable = maps[currentMap].baseTerrain;

            EventManager<TitleHandler.TitleMessage>.RaiseEvent(EventManager.EventType.TITLE,
                    new TitleHandler.TitleMessage(maps[currentMap].baseTerrain.name, maps[currentMap].baseTerrain.iconicColor, false));

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