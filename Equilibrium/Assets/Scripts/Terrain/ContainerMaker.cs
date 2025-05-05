using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OuterWilds
{
    public class ContainerMaker : MonoBehaviour
    {
        [SerializeField] private TerrainData data = default;
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
        private void Start()
        {
            // Water.
            GameObject water = Instantiate(waterPrefab, Vector3.up * data.waterHeight, Quaternion.identity);
            water.transform.localScale = new Vector3(data.sizeX * waterScaleFactor, 1f, data.sizeZ * waterScaleFactor);

            // Ceiling.
            SetupCube(new Vector3(data.sizeX + horizontalContainerMargin, 1f, data.sizeZ + horizontalContainerMargin),
                Vector3.up * (data.meshCeilingHeight + (verticalContainerMargin * 0.5f) - 0.5f));

            // Walls.
            for (int x = -1; x <= 1; x++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    if (Mathf.Abs(x) == Mathf.Abs(z))
                        continue;

                    float dist = Vector3.Distance(Vector3.up * data.meshFloorHeight, Vector3.up * data.meshCeilingHeight) + verticalContainerMargin;
                    Vector3 scale = dist * Vector3.up;
                    Vector3 pos;

                    if (x == 0)
                    {
                        scale += Vector3.right * (data.sizeX + horizontalContainerMargin);
                        scale += Vector3.forward;

                        pos = 0.5f * data.sizeZ * new Vector3(x, 0f, z) + (outreach * z * Vector3.forward);
                    }
                    else
                    {
                        scale += Vector3.forward * (data.sizeZ + horizontalContainerMargin);
                        scale += Vector3.right;

                        pos = 0.5f * data.sizeX * new Vector3(x, 0f, z) + (outreach * x * Vector3.right);
                    }

                    pos.y = (data.meshFloorHeight + data.meshCeilingHeight) / 2f;

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
    }
}