using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OuterWilds
{
    /// <summary>
    /// Make suer this shit doesnt break like crazy when you add more than 1 terrain types bascially.
    /// </summary>
    public class ContainerMaker : MonoBehaviour, IWritable<ITerrainable>
    {
        public const float MAX_CONTAINER_VERTICAL_EXTENT = 10000f;
        
        //[SerializeField] private TerrainData data = default;
        [SerializeField] private GameObject waterPrefab = default;
        [SerializeField] private GameObject wallPrefab = default;
        [SerializeField] private float verticalContainerMargin = default;
        [SerializeField] private float horizontalContainerMargin = default;
        [SerializeField] private float waterScaleFactor = default;
        [SerializeField] private float outreach = default;
        [SerializeField] private bool visible = default;

        /// <summary>
        /// Classic example of my current style of coding:
        /// have some data and then have behaviour that always works
        /// for all possible combinations of the data. Idk how to do it better
        /// maybe decorator or builder idk.
        /// </summary>
        private void Spawn(ITerrainable terrainable)
        {
            // LOLOLOLOL, now this is what i would like to call a hack fix guys.
            float meshCeilingHeight = terrainable.GetHeightAtPoint(0f, MAX_CONTAINER_VERTICAL_EXTENT, 0f);
            float meshFloorHeight = terrainable.GetHeightAtPoint(0f, -MAX_CONTAINER_VERTICAL_EXTENT, 0f);

            // Water.
            GameObject water = Instantiate(waterPrefab, Vector3.up * terrainable.GetWaterHeight(), Quaternion.identity);
            water.transform.localScale = new Vector3(terrainable.GetSize() * waterScaleFactor, 1f, terrainable.GetSize() * waterScaleFactor);

            // Ceiling.
            SetupCube(new Vector3(terrainable.GetSize() + horizontalContainerMargin, 1f, terrainable.GetSize() + horizontalContainerMargin),
                Vector3.up * (meshCeilingHeight + (verticalContainerMargin * 0.5f) - 0.5f));

            // Walls.
            for (int x = -1; x <= 1; x++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    if (Mathf.Abs(x) == Mathf.Abs(z))
                        continue;

                    float dist = Vector3.Distance(Vector3.up * meshFloorHeight, Vector3.up * meshCeilingHeight) + verticalContainerMargin;
                    Vector3 scale = dist * Vector3.up;
                    Vector3 pos;

                    if (x == 0)
                    {
                        scale += Vector3.right * (terrainable.GetSize() + horizontalContainerMargin);
                        scale += Vector3.forward;

                        pos = 0.5f * terrainable.GetSize() * new Vector3(x, 0f, z) + (outreach * z * Vector3.forward);
                    }
                    else
                    {
                        scale += Vector3.forward * (terrainable.GetSize() + horizontalContainerMargin);
                        scale += Vector3.right;

                        pos = 0.5f * terrainable.GetSize() * new Vector3(x, 0f, z) + (outreach * x * Vector3.right);
                    }

                    pos.y = (meshFloorHeight + meshCeilingHeight) / 2f;

                    SetupCube(scale, pos);
                }
            }
        }

        private void SetupCube(Vector3 scale, Vector3 pos)
        {
            GameObject cube = Instantiate(wallPrefab, pos, Quaternion.identity);
            cube.transform.localScale = scale;
            cube.GetComponent<MeshRenderer>().enabled = visible;
        }

        public void Write(ITerrainable obj) => Spawn(obj);
    }
}