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
        [SerializeField] private DecoratedTerrain[] decoratedTerrains = default;
        [SerializeField] private List<InspectorInterface<IWritable<ITerrainable>>> listeners = default;

        private ITerrainable terrainable;
        private int index;

        private void Awake()
        {
            listeners.ForEach(x => x.Setup());
        }

        private void Start()
        {
            Refresh();
        }

        /*private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Refresh();
        }*/

        private void Refresh()
        {
            DecoratedTerrain curr = decoratedTerrains[index];
            
            terrainable = curr.baseTerrain;
            
            for (int i = 0; i < curr.decorators.Length; i++)
            {
                curr.decorators[i].Decorate(terrainable);
                terrainable = curr.decorators[i];
            }

            listeners.ForEach(x => x.attached.Write(terrainable));
        }

        [System.Serializable]
        public struct DecoratedTerrain 
        {
            public BaseTerrain baseTerrain;
            public TerrainDecorator[] decorators;
        }
    }
}