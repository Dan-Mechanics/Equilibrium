using System.Collections.Generic;
using UnityEngine;

namespace OuterWilds
{
    public class PerlinGenerator : MonoBehaviour, IPassable<Vector3[]>
    {
        [SerializeField] private TerrainData data = default;
        [SerializeField] private List<InspectorInterface<IPassable<Vector3[]>>> listeners = default;

        private void Awake()
        {
            listeners.ForEach(x => x.Setup());
        }

        public void Pass(ref Vector3[] verts)
        {
            for (int i = 0; i < verts.Length; i++)
            {
                verts[i].y = data.GetPerlinHeight(verts[i].x, verts[i].z);
            }

            // This is because it wont work for forreach.
            for (int i = 0; i < listeners.Count; i++)
            {
                listeners[i].attached.Pass(ref verts);
            }
        }
    }
}