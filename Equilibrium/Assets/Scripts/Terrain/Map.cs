using System.Collections.Generic;
using UnityEngine;

namespace Equilibrium
{
    [CreateAssetMenu(menuName = "ScriptableObject/" + nameof(Map), fileName = "New " + nameof(Map))]
    public class Map : ScriptableObject
    {
        public BaseTerrain baseTerrain;
        public List<TerrainDecorator> decorators;
        public List<TerrainColorDecorator> colorDecorators;
    }
}