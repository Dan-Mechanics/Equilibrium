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
    public class Terraformer : MonoBehaviour, IWritable<Vector3[]>, IGetter<Vector3[]>, IUpdatable, IWritable<ITerrainable>, IWritable<BaseTerrain>
    {
        [SerializeField] private MeshFilter filter = default;
        [SerializeField] private Camera cam = default;
        [SerializeField] private InspectorInterface<IGetter<State>> state = default;
        [SerializeField] private List<InspectorInterface<IWritable<Vector3[]>>> listeners = default;

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
        public void Write(Vector3[] verts) => verticies = verts;
        public void Write(BaseTerrain baseTerrain) => this.baseTerrain = baseTerrain;
        public Vector3[] Get() => verticies;

        public void DoUpdate()
        {
            if (state.attached.Get() == State.Dragging)
                return;

            for (int i = 0; i < fixedTicks.GetTicksCount(Time.deltaTime); i++)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                    DoRaycast();
            }

            if (Input.GetKey(KeyCode.LeftShift))
                DoDebug();

            if (!hasChanged)
                return;

            // so now we're yapping to the generator and decorations.
            listeners.ForEach(x => x.attached.Write(verticies));
            hasChanged = false;
        }

        /// <summary>
        /// https://discussions.unity.com/t/how-to-raycast-from-camera-through-mouse-position/565021/3
        /// </summary>
        private void DoRaycast()
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, raycastSettings.range, raycastSettings.mask, QueryTriggerInteraction.Ignore))
                TryChangeTerrain(hit.point, state.attached.Get() == State.Mountain ? 1f : -1f);
        }

        private void DoDebug() 
        {
            if (Input.GetKeyDown(KeyCode.F))
                Move(1000f);

            // this too.
            if (Input.GetKeyDown(KeyCode.UpArrow))
                Move(50f);

            if (Input.GetKeyDown(KeyCode.DownArrow))
                Move(-50f);
        }

        private void TryChangeTerrain(Vector3 point, float dir)
        {
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

        public void WipeClean() 
        {
            for (int i = 0; i < verticies.Length; i++)
            {
                verticies[i].y = 0f;
            }

            hasChanged = true;
        }
    }
}