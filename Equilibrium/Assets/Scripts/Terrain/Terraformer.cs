using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Equilibrium
{
    /// <summary>
    /// This class is getting too big.
    /// 
    /// Please refactor with smallstate and other memes.
    /// </summary>
    public class Terraformer : MonoBehaviour, IPassable<Vector3[]>, IDataGettable<Vector3[]>, IUpdatable, IWritable<ITerrainable>, IWritable<BaseTerrain>
    {
        public Vector3[] Data => verticies;

        [SerializeField] private MeshFilter filter = default;
        [SerializeField] private Camera cam = default;
        [SerializeField] private InspectorInterface<IDataGettable<State>> state = default;
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
        private BaseTerrain baseTerrain;

        private void Awake()
        {
            listeners.ForEach(x => x.Setup());
            state.Setup();
            fixedTicks = new FixedTicks(brushInterval);
        }

        public void Write(ITerrainable terrainable) => this.terrainable = terrainable;
        public void Pass(ref Vector3[] verts) => verticies = verts;

        public void DoUpdate()
        {
            for (int i = 0; i < fixedTicks.GetTicksCount(Time.deltaTime); i++)
            {
                if (state.attached.Data == State.Dragging)
                    continue;
                
                if (Input.GetKey(KeyCode.Mouse0))
                    DoRaycast();

                // this too.
                if (Input.GetKey(KeyCode.UpArrow))
                    Move(8f);

                if (Input.GetKey(KeyCode.DownArrow))
                    Move(-8f);
            }

            // this is for debug.
            if (Input.GetKeyDown(KeyCode.F) && state.attached.Data != State.Dragging)
                Fill();

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
            //Debug.Log("hello");
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, raycastSettings.range, raycastSettings.mask, QueryTriggerInteraction.Ignore))
                TryChangeTerrain(hit.point, state.attached.Data == State.Mountain ? 1f : -1f);
        }

        private void Fill()
        {
            Move(1000f);
        }

        private void TryChangeTerrain(Vector3 point, float dir)
        {
           // Debug.Log("hello");

            // Because we want mesh space.
            point -= filter.transform.position;
            Vector2 offset = Vector2.zero;
            
            for (int x = -brushSize; x <= brushSize; x++)
            {
                for (int z = -brushSize; z <= brushSize; z++)
                {
                    Utils.SetVector2(ref offset, x, z);

                    float dist = Vector2.Distance(Vector2.zero, offset);

                    if (dist > brushSize)
                        continue;

                    if (!Utils.TryGetIndexFromPos((int)point.x + x, (int)point.z + z,
                        terrainable.GetSize(), terrainable.GetSize(), out int index))
                        continue;

                    /*if (!Utils.TryGetIndexFromPos(baseTerrain.brush.GetRound(point.x, x), baseTerrain.brush.GetRound(point.z, z),
                        terrainable.GetSize(), terrainable.GetSize(), out int index))
                        continue;*/

                    Terraform(index, brushStrength * baseTerrain.brush.GetBrushMod(dist, brushSize) * brushInterval * dir);
                }
            }
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