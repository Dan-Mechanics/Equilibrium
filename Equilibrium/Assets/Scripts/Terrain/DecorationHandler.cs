using System.Collections.Generic;
using UnityEngine;

namespace Equilibrium
{
    /// <summary>
    /// Maybe try make this class mor general ?? Open and closed vibes.
    /// NOTE: this script will prolly not work for changing sizes of the terrain.
    /// </summary>
    public class DecorationHandler : MonoBehaviour, IPassable<Vector3[]>, IWritable<BaseTerrain>, IWritable<ITerrainable>
    {
        [SerializeField] private InspectorInterface<ISpawnable> spawner = default;
        [SerializeField] private bool hasDecorations = default;

        private readonly List<Decoration> decorations = new();
        private ITerrainable terrainable;
        private BaseTerrain baseTerrain;

        private void Awake() => spawner.Setup();
        public void Write(ITerrainable terrainable) => this.terrainable = terrainable;
        public void Pass(ref Vector3[] verts) => TryPlaceAll(ref verts);

        public void Write(BaseTerrain baseTerrain)
        {
            if (decorations.Count > 0)
                RemoveAllDecorations();

            this.baseTerrain = baseTerrain;
        }

        private void TryPlaceAll(ref Vector3[] verts)
        {
            if (baseTerrain == null)
                return;

            if (!hasDecorations)
                return;

            // Make sure to write <= instead of <.
            if (baseTerrain != null && decorations.Count <= 0)
                SpawnNewDecorations(verts.Length);

            for (int i = 0; i < decorations.Count; i++)
            {
                PlaceDecoration(decorations[i], ref verts);
            }
        }

        private void SpawnNewDecorations(int vertsCount)
        {
            for (int i = 0; i < baseTerrain.spawnDatas.Length; i++)
            {
                Transform[] transforms = spawner.attached.Spawn(baseTerrain.spawnDatas[i]);

                for (int j = 0; j < transforms.Length; j++)
                {
                    decorations.Add(new Decoration(transforms[j], Random.Range(0, vertsCount), baseTerrain.spawnDatas[i].spawnOffset));
                }
            }
        }

        private void PlaceDecoration(Decoration decoration, ref Vector3[] verts)
        {
            if (terrainable == null)
                return;

            decoration.transform.position = Utils.GetVertexWorldSpace(decoration.vertexIndex, ref verts, terrainable) + decoration.offset;
            decoration.transform.gameObject.SetActive(verts[decoration.vertexIndex].y > terrainable.GetWaterHeight());
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