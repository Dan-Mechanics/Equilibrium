using System.Collections.Generic;
using UnityEngine;

namespace Equilibrium
{
    /// <summary>
    /// This class is responsible for generating the mesh, i could make another script that actually generates the perinl
    /// and such. Cool idea: classes talk tuah eachother via interfaces.
    /// </summary>
    public class TerrainGenerator : MonoBehaviour, IPassable<Vector3[]>, IWritable<ITerrainable>
    {
        [SerializeField] private MeshFilter filter = default;
        [SerializeField] private MeshCollider coll = default;

        [SerializeField] private InspectorInterface<IWritable<Vector3>> transformSetter = default;
        [SerializeField] private InspectorInterface<IWritable<ITerrainable, Mesh>> terrainMaterial = default;
        [SerializeField] private List<InspectorInterface<IPassable<Vector3[]>>> listeners = default;

        private Mesh mesh;
        private int[] triangles;
        private ITerrainable globalTerrainable;
        private readonly MeshColliderCookingOptions cookingOptions =
        MeshColliderCookingOptions.UseFastMidphase & MeshColliderCookingOptions.CookForFasterSimulation;

        private void Awake()
        {
            listeners.ForEach(x => x.Setup());
            terrainMaterial.Setup();
            transformSetter.Setup();
        }

        private Vector3[] GenerateMesh(ITerrainable terrainable)
        {
            Vector3[] verticies = new Vector3[(terrainable.GetSize() + 1) * (terrainable.GetSize() + 1)];

            int i = 0;
            for (int z = 0; z <= terrainable.GetSize(); z++)
            {
                for (int x = 0; x <= terrainable.GetSize(); x++)
                {
                    verticies[i] = new Vector3(x, terrainable.GetHeightAtPoint(x, 0f, z), z);
                    i++;
                }
            }

            triangles = new int[terrainable.GetSize() * terrainable.GetSize() * 6];

            int vert = 0;
            int tris = 0;

            for (int z = 0; z < terrainable.GetSize(); z++)
            {
                for (int x = 0; x < terrainable.GetSize(); x++)
                {
                    triangles[tris + 0] = vert + 0;
                    triangles[tris + 1] = vert + terrainable.GetSize() + 1;
                    triangles[tris + 2] = vert + 1;

                    triangles[tris + 3] = vert + 1;
                    triangles[tris + 4] = vert + terrainable.GetSize() + 1;
                    triangles[tris + 5] = vert + terrainable.GetSize() + 2;

                    vert++;
                    tris += 6;
                }

                vert++;
            }

            return verticies;
        }

        public void Pass(ref Vector3[] verts) => UpdateMesh(ref verts);

        private void UpdateMesh(ref Vector3[] verticies)
        {
            mesh.Clear();

            mesh.vertices = verticies;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();

            // idk if this is faster but ok.
            Physics.BakeMesh(mesh.GetInstanceID(), false, cookingOptions);

            coll.sharedMesh = mesh;

            // For camera pivot center.
            transformSetter.attached.Write(Vector3.up * ((mesh.bounds.min.y + mesh.bounds.max.y) / 2f));

            // NOTE: this means that the mesh wil not hot change.
            //terrainMaterial.attached.Write(globalTerrainable, mesh);
        }

        public void Write(ITerrainable terrainable)
        {
            globalTerrainable = terrainable;

            mesh = new Mesh();
            filter.mesh = mesh;
            coll.cookingOptions = cookingOptions;

            // ----

            Vector3[] verts = GenerateMesh(terrainable);

            transform.position = new Vector3(-terrainable.GetSize() / 2f, 0f, -terrainable.GetSize() / 2f);

            listeners.ForEach(x => x.attached.Pass(ref verts));

            UpdateMesh(ref verts);

            terrainMaterial.attached.Write(globalTerrainable, mesh);
        }
    }
}