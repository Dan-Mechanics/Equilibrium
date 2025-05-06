using System.Collections.Generic;
using UnityEngine;

namespace Equilibrium
{
    public class Terraformer : MonoBehaviour, IPassable<Vector3[]>, IDataGettable<Vector3[]>, IUpdatable, IWritable<ITerrainable>
    {
        public Vector3[] Data => verticies;

        [SerializeField] private MeshFilter filter = default;
        [SerializeField] private Camera cam = default;
        //[SerializeField] private TerrainData data = default;
        [SerializeField] private List<InspectorInterface<IPassable<Vector3[]>>> listeners = default;

        [Header("Settings")]
        [SerializeField] private RaycastSettings raycastSettings = default;
        [SerializeField] private int brushSize = default;
        [SerializeField] private float brushStrength = default;
        [SerializeField] private float brushInterval = default;

        private ITerrainable terrainable;
        private FixedTicks fixedTicks;
        private Vector3[] verticies;
        private bool hasChanged;

        private void Awake()
        {
            listeners.ForEach(x => x.Setup());
            fixedTicks = new FixedTicks(brushInterval);
        }

        public void Write(ITerrainable terrainable) => this.terrainable = terrainable;
        public void Pass(ref Vector3[] verts) => verticies = verts;

        public void DoUpdate()
        {
            hasChanged = false;

            for (int i = 0; i < fixedTicks.GetTicksCount(Time.deltaTime); i++)
            {
                DoRaycast();
            }

            if (!hasChanged)
                return;

            // so now we're yapping to the generator and decorations.
            listeners.ForEach(x => x.attached.Pass(ref verticies));
        }

        /// <summary>
        /// https://discussions.unity.com/t/how-to-raycast-from-camera-through-mouse-position/565021/3
        /// </summary>
        private void DoRaycast()
        {
            if (!Input.GetKey(KeyCode.Mouse0))
                return;

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, raycastSettings.range, raycastSettings.mask, QueryTriggerInteraction.Ignore))
                TryChangeTerrain(hit.point);
        }

        // need tools for this ish.
        // maybe state machine esque object.
        private void TryChangeTerrain(Vector3 point) 
        {
            // because we want mesh space.
            point -= filter.transform.position;
            Vector2 offset = Vector2.zero;

            for (int x = -brushSize; x <= brushSize; x++)
            {
                for (int z = -brushSize; z <= brushSize; z++)
                {
                    Utils.SetVector2(ref offset, x, z);

                    if (Vector2.Distance(Vector2.zero, offset) > brushSize)
                        continue;

                    if (!Utils.TryGetIndexFromPos(Mathf.RoundToInt(point.x + x), Mathf.RoundToInt(point.z + z),
                        terrainable.GetSize(), terrainable.GetSize(), out int index))
                        continue;

                    Terraform(index, brushStrength * brushInterval * (Input.GetKey(KeyCode.LeftShift) ? -1f : 1f));
                    hasChanged = true;
                }
            }
        }

        private void Terraform(int index, float upwardsMeters)
        {
            //verticies[index].y = data.ClampTerrainHeight(verticies[index].y + upwardsMeters);
            verticies[index].y = terrainable.GetHeightAtPoint(verticies[index].x, verticies[index].y + upwardsMeters, verticies[index].z);
        }

        /*private void Flatten(int index, float y)
        {
            if (y > verticies[index].y)
                verticies[index].y += brushInterval * 10f;

            if (y < verticies[index].y)
                verticies[index].y -= brushInterval * 10f;

            //verticies[index].y = data.ClampTerrainHeight(y);
        }*/
    }
}