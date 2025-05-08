using System.Collections.Generic;
using UnityEngine;

namespace Equilibrium
{
    /// <summary>
    /// Maybe try make this class mor general ?? Open and closed vibes.
    /// NOTE: this script will prolly not work for changing sizes of the terrain.
    /// </summary>
    public class DecorationHandler : MonoBehaviour, IPassable<Vector3[]>, IWritable<ITerrainable>, IWritable<BaseTerrain>
    {
        //[SerializeField] private Spawner[] spawners = default;

        [SerializeField] private Spawner spawner = default;
        [SerializeField] private bool spawnDecorations = default;

        private readonly List<Decoration> decorations = new();
        private ITerrainable terrainable;

        public void Write(ITerrainable terrainable) => this.terrainable = terrainable;
        public void Pass(ref Vector3[] verts) => TryPlaceAll(ref verts);

        private void TryPlaceAll(ref Vector3[] verts)
        {
            if (terrainable == null)
                return;

            for (int i = 0; i < decorations.Count; i++)
            {
                PlaceDecoration(decorations[i], ref verts);
            }
        }

        private void PlaceDecoration(Decoration decoration, ref Vector3[] verts)
        {
            if (terrainable == null)
                return;

            decoration.transform.position = Utils.GetVertexWorldSpace(decoration.vertexIndex, ref verts, terrainable) + decoration.offset;
            decoration.transform.gameObject.SetActive(verts[decoration.vertexIndex].y > terrainable.GetWaterHeight());
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

        public void Write(BaseTerrain baseTerrain)
        {
            if (decorations.Count > 0)
                RemoveAllDecorations();

            if (!spawnDecorations)
                return;

            Transform[] transforms = spawner.SpawnWithData(Vector3.zero, baseTerrain.spawnDatas);

            for (int i = 0; i < transforms.Length; i++)
            {
                decorations.Add(new Decoration(transforms[i], Random.Range(0, verts.Length), spawners[i].Data));
            }

            /*if (decorations.Count > 0)
            {
                for (int i = 0; i < decorations.Count; i++)
                {
                    Destroy(decorations[i].transform.gameObject);
                }

                decorations.Clear();
            }*/
        }

        private void RemoveAllDecorations()
        {
            for (int i = 0; i < decorations.Count; i++)
            {
                Destroy(decorations[i].transform.gameObject);
            }

            decorations.Clear();
        }

        private struct Decoration
        {
            public Transform transform;
            public int vertexIndex;
            public Vector3 offset;

            public Decoration(Transform transform, int vertexIndex, Vector3 offset)
            {
                this.transform = transform;
                this.vertexIndex = vertexIndex;
                this.offset = offset;
            }
        }
    }
}