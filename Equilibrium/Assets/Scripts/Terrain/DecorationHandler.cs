using System.Collections.Generic;
using UnityEngine;

namespace OuterWilds
{
    /// <summary>
    /// Maybe try make this class mor general ?? Open and closed vibes.
    /// NOTE: this script will prolly not work for changing sizes of the terrain.
    /// </summary>
    public class DecorationHandler : MonoBehaviour, IPassable<Vector3[]>, IWritable<ITerrainable>
    {
        //[SerializeField] TerrainData data = default;
        [SerializeField] private Spawner[] spawners = default;
        [SerializeField] private bool spawnDecorations = default;

        private readonly List<Decoration> decorations = new List<Decoration>();
        private ITerrainable terrainable;

        public void Pass(ref Vector3[] verts)
        {
            if (!spawnDecorations)
                return;
            
            if (decorations.Count <= 0)
                SpawnDecorations(ref verts);

            for (int i = 0; i < decorations.Count; i++)
            {
                Place(decorations[i], ref verts);
            }
        }

        private void SpawnDecorations(ref Vector3[] verts)
        {
            for (int i = 0; i < spawners.Length; i++)
            {
                GameObject[] newDecorations = spawners[i].Spawn(Vector3.zero);

                for (int j = 0; j < newDecorations.Length; j++)
                {
                    decorations.Add(new Decoration(newDecorations[j].transform, Random.Range(0, verts.Length), spawners[i].Data));
                }
            }
        }

        private void Place(Decoration decoration, ref Vector3[] verts)
        {
            //decoration.transform.gameObject.isStatic = false;

            decoration.transform.position = Utils.GetVertexWorldSpace(decoration.vertexIndex, ref verts, terrainable) + decoration.spawnData.spawnOffset;
            decoration.transform.gameObject.SetActive(verts[decoration.vertexIndex].y > terrainable.GetWaterHeight());

            // lol XD
            //decoration.transform.gameObject.isStatic = true;
        }

        public void Write(ITerrainable terrainable) => this.terrainable = terrainable;

        private struct Decoration
        {
            public Transform transform;
            public int vertexIndex;
            public SpawnData spawnData;

            public Decoration(Transform transform, int vertexIndex, SpawnData decoration)
            {
                this.transform = transform;
                this.vertexIndex = vertexIndex;
                this.spawnData = decoration;
            }
        }
    }
}