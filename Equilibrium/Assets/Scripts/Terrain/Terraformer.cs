using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Equilibrium
{
    public class Terraformer : MonoBehaviour, IPassable<Vector3[]>, IDataGettable<Vector3[]>, IUpdatable, IWritable<ITerrainable>, IWritable<BaseTerrain>
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
        [SerializeField] private UnityEvent<Vector3> onClickSomehwere = default;

        private ITerrainable terrainable;
        private FixedTicks fixedTicks;
        private Vector3[] verticies;
        private bool hasChanged;
        private BaseTerrain baseTerrain;

        private void Awake()
        {
            listeners.ForEach(x => x.Setup());
            fixedTicks = new FixedTicks(brushInterval);
        }

        public void Write(ITerrainable terrainable) => this.terrainable = terrainable;
        public void Pass(ref Vector3[] verts) => verticies = verts;

        public void DoUpdate()
        {
            

            for (int i = 0; i < fixedTicks.GetTicksCount(Time.deltaTime); i++)
            {
                if (Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Mouse1))
                    DoRaycast();

                if (Input.GetKey(KeyCode.UpArrow))
                    Move(8f);

                if (Input.GetKey(KeyCode.DownArrow))
                    Move(-8f);
            }

            if (Input.GetKeyDown(KeyCode.F))
                Fill();

            /*if (Input.GetKey(KeyCode.UpArrow))
                Move(8f);

            if (Input.GetKey(KeyCode.DownArrow))
                Move(-8f);*/

            if (!hasChanged)
                return;

            // so now we're yapping to the generator and decorations.
            listeners.ForEach(x => x.attached.Pass(ref verticies));

            hasChanged = false;
        }

        /// <summary>
        /// https://discussions.unity.com/t/how-to-raycast-from-camera-through-mouse-position/565021/3
        /// </summary>
        private void DoRaycast()
        {
            /*if (!Input.GetKey(KeyCode.Mouse0))
                return;*/

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, raycastSettings.range, raycastSettings.mask, QueryTriggerInteraction.Ignore))
                TryChangeTerrain(hit.point);
        }

        private void Fill()
        {
            Move(1000f);
        }

        // need tools for this ish.
        // maybe state machine esque object.

        /// <summary>
        /// Make this part of terraform object??
        /// </summary>
        /// <param name="point"></param>
        private void TryChangeTerrain(Vector3 point)
        {
            // onClickSomehwere?.Invoke(point);

            // because we want mesh space.
            point -= filter.transform.position;
            Vector2 offset = Vector2.zero;

            for (int x = -brushSize; x <= brushSize; x++)
            {
                for (int z = -brushSize; z <= brushSize; z++)
                {
                    Utils.SetVector2(ref offset, x, z);

                    switch (baseTerrain.biome)
                    {
                        case Biome.Mesa:
                            DoMesa(offset, point, x, z);
                            break;
                        case Biome.Icey:
                            DoNormal(offset, point, x, z);
                            break;
                        case Biome.Serene:
                            DoNormal(offset, point, x, z);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// We could move this into the decorator
        /// </summary>
        private void DoNormal(Vector2 offset, Vector3 point, int x, int z)
        {
            float dist = Vector2.Distance(Vector2.zero, offset);

            if (dist > brushSize)
                return;

            if (!Utils.TryGetIndexFromPos(Mathf.RoundToInt(point.x) + x, Mathf.RoundToInt(point.z) + z,
                terrainable.GetSize(), terrainable.GetSize(), out int index))
                return;

            Terraform(index, brushStrength * brushInterval * (Input.GetKey(KeyCode.LeftShift) ? -1f : 1f));
        }

        private void DoMesa(Vector2 offset, Vector3 point, int x, int z)
        {
            float dist = Vector2.Distance(Vector2.zero, offset);

            if (dist > brushSize)
                return;

            if (!Utils.TryGetIndexFromPos(Mathf.RoundToInt(point.x) + x, Mathf.RoundToInt(point.z) + z,
                terrainable.GetSize(), terrainable.GetSize(), out int index))
                return;

            dist = 1f - (dist / Utils.Root(brushSize));
            dist *= 1.5f;

            Terraform(index, dist * brushStrength * brushInterval * (Input.GetKey(KeyCode.LeftShift) ? -1f : 1f));
        }

        private void Terraform(int index, float upwardsMeters)
        {
            verticies[index].y += upwardsMeters;
            terrainable.SetHeightTerraform(verticies[index].x, ref verticies[index].y, verticies[index].z);

            hasChanged = true;
        }

        private void Move(float amount)
        {
            for (int i = 0; i < verticies.Length; i++)
            {
                Terraform(i, amount * brushInterval);
            }
        }

        [ContextMenu(nameof(WipeClean))]
        public void WipeClean() 
        {
            print(nameof(WipeClean));

            for (int i = 0; i < verticies.Length; i++)
            {
                verticies[i].y = 0f;
            }

            hasChanged = true;
        }

        public void Write(BaseTerrain baseTerrain) => this.baseTerrain = baseTerrain;

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