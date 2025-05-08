using System.Collections.Generic;
using UnityEngine;

namespace Equilibrium
{
    public class Spawner : MonoBehaviour, IDataGettable<SpawnData>, IWritable<Vector3>
    {
        public SpawnData Data => spawnData;

        [SerializeField] private bool fromStart = default;
        [SerializeField] private SpawnData spawnData = default;

        private void Start()
        {
            if (fromStart)
                Spawn(transform.position);
        }

        public Transform[] Spawn(Vector3 pos)
        {
            return SpawnWithData(pos, new SpawnData[] { spawnData });
        }

        public Transform SpawnSingle(Vector3 pos)
        {
            return SpawnWithDataSingle(pos, spawnData);
        }

        public Transform[] SpawnWithData(Vector3 pos, SpawnData[] spawnDatas)
        {
            List<Transform> spawned = new();

            for (int i = 0; i < spawnDatas.Length; i++)
            {
                for (int j = 0; j < spawnDatas[i].count; j++)
                {
                    spawned.Add(SpawnWithDataSingle(pos, spawnDatas[i]));
                }
            }

            return spawned.ToArray();
        }

        public Transform SpawnWithDataSingle(Vector3 pos, SpawnData spawnData)
        {
            GameObject go = Instantiate(spawnData.prefab, pos + spawnData.spawnOffset, spawnData.prefab.transform.rotation);

            Utils.GiveRandomUpwardsRotation(go.transform);
            if (go.TryGetComponent(out ISetupable setupable))
                setupable.Setup();

            return go.transform;
        }

        public void SetPrefab(GameObject prefab) 
        {
            spawnData.prefab = prefab;
        }

        public void Write(Vector3 t) => Spawn(t);
    }
}