using System.Collections.Generic;
using UnityEngine;

namespace OuterWilds
{
    /// <summary>
    /// This class is responsible for generating the mesh, i could make another script that actually generates the perinl
    /// and such. Cool idea: classes talk tuah eachother via interfaces.
    /// </summary>
    public class TerrainGenerator : MonoBehaviour, IPassable<Vector3[]>
    {
        [SerializeField] private TerrainData data = default;
        [SerializeField] private MeshFilter filter = default;
        [SerializeField] private MeshCollider coll = default;

        [SerializeField] private InspectorInterface<IPassable<Vector3>> newCenterMeshListener = default;
        [SerializeField] private List<InspectorInterface<IPassable<Vector3[]>>> listeners = default;

        private Mesh mesh;
        private int[] triangles;

        private readonly MeshColliderCookingOptions cookingOptions =
        MeshColliderCookingOptions.UseFastMidphase & MeshColliderCookingOptions.CookForFasterSimulation;

        private void Awake()
        {
            listeners.ForEach(x => x.Setup());
            newCenterMeshListener.Setup();
        }

        private void Start()
        {
            mesh = new Mesh();
            filter.mesh = mesh;
            coll.cookingOptions = cookingOptions;

            GenerateStarterTerrain();
        }

        /*private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                GenerateStarterTerrain();
        }*/

        [ContextMenu(nameof(GenerateStarterTerrain))]
        private void GenerateStarterTerrain() 
        {
            Vector3[] verts = GenerateMesh();
            //UpdateMesh(ref verts);

            // i would like to keep it so that the mesh is always 0,0,0 so less bs with conversions and such.
            transform.position = new Vector3(-data.sizeX / 2f, 0f, -data.sizeZ / 2f);

            listeners.ForEach(x => x.attached.Pass(ref verts));
            //listeners.Clear();
        }

        private Vector3[] GenerateMesh()
        {
            Vector3[] verticies = new Vector3[(data.sizeX + 1) * (data.sizeZ + 1)];

            int i = 0;
            for (int z = 0; z <= data.sizeZ; z++)
            {
                for (int x = 0; x <= data.sizeX; x++)
                {
                    /*if (i < 150)
                        print($"{x} {z}");*/

                    verticies[i] = new Vector3(x, 0f, z);
                    i++;
                }
            }

            triangles = new int[data.sizeX * data.sizeZ * 6];

            int vert = 0;
            int tris = 0;

            for (int z = 0; z < data.sizeZ; z++)
            {
                for (int x = 0; x < data.sizeX; x++)
                {
                    triangles[tris + 0] = vert + 0;
                    triangles[tris + 1] = vert + data.sizeX + 1;
                    triangles[tris + 2] = vert + 1;

                    triangles[tris + 3] = vert + 1;
                    triangles[tris + 4] = vert + data.sizeX + 1;
                    triangles[tris + 5] = vert + data.sizeX + 2;

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

            Vector3 newMeshCenter = Vector3.up * ((mesh.bounds.min.y + mesh.bounds.max.y) / 2f);
            newCenterMeshListener.attached.Pass(ref newMeshCenter);
        }
    }
}