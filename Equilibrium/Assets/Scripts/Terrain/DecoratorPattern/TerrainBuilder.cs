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
        [SerializeField] private List<Map> maps = default;

        [SerializeField] private List<InspectorInterface<IWritable<ITerrainable>>> terrainListeners = default;
        [SerializeField] private List<InspectorInterface<IWritable<ITerrainableColorable>>> colorListeners = default;
        [SerializeField] private List<InspectorInterface<IWritable<BaseTerrain>>> baseListeners = default;

        [SerializeField] private List<MonoBehaviour> terrainListenersMono = default;
        [SerializeField] private List<MonoBehaviour> colorListenersMono = default;
        [SerializeField] private List<MonoBehaviour> baseListenersMono = default;

        private List<IWritable<ITerrainable>> terrainListeners2;
        private List<IWritable<BaseTerrain>> baseListeners2;
        private List<IWritable<ITerrainableColorable>> colorListeners2;

        [SerializeField] private UnityEvent onRefresh = default;
        [SerializeField] private UnityEvent onUpperLimitReached = default;

        private void Awake()
        {
            /*terrainListeners.ForEach(x => x.Setup());
            colorListeners.ForEach(x => x.Setup());
            baseListeners.ForEach(x => x.Setup());*/

            Utils.PipeTo(terrainListenersMono, terrainListeners2);
            Utils.PipeTo(colorListenersMono, colorListeners2);
            Utils.PipeTo(baseListenersMono, baseListeners2);

            /*MonoBehaviour[] monoBehaviours = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
            IWritable<ITerrainable>[] terrainables = FindObjectsByType<IWritable<ITerrainable>>(FindObjectsSortMode.None);*/
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
            baseListeners.ForEach(x => x.attached.Write(baseTerrain));

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