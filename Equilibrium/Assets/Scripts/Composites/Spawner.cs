using System.Collections.Generic;
using UnityEngine;

namespace OuterWilds
{
    public class Spawner : MonoBehaviour, IReadable<SpawnData>, IWritable<Vector3>
    {
        public SpawnData Data => spawnData;

        [SerializeField] private bool fromStart = default;
        [SerializeField] private SpawnData spawnData = default;

        private void Start()
        {
            if (fromStart)
                Spawn(transform.position);
        }

        public GameObject[] Spawn(Vector3 pos)
        {
            GameObject[] spawned = new GameObject[spawnData.count];
            
            for (int i = 0; i < spawnData.count; i++)
            {
                spawned[i] = SpawnSingle(pos);
            }

            return spawned;
        }

        public GameObject SpawnSingle(Vector3 pos)
        {
            GameObject go = Instantiate(spawnData.prefab, pos + spawnData.spawnOffset, spawnData.prefab.transform.rotation);

            Utils.GiveRandomUpwardsRotation(go.transform);
            if (go.TryGetComponent(out ISetupable setupable))
                setupable.Setup();

            return go;
        }

        public void Write(Vector3 t) => Spawn(t);
    }
}