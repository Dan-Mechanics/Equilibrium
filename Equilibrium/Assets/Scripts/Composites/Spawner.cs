using System.Collections.Generic;
using UnityEngine;

namespace Equilibrium
{
    /// <summary>
    /// Remove these frikcign shit.
    /// 
    /// Vibe: make this work with some interfaces? / clean the logic a little ?
    /// </summary>
    public class Spawner : MonoBehaviour, ISpawnable
    {
        //public SpawnData SpawnData => spawnData;

        [SerializeField] private bool fromStart = default;
        [SerializeField] private SpawnData spawnData = default;

        private void Start()
        {
            if (!fromStart)
                return;

            Spawn(transform.position);
        }

        public void Write(Vector3 t) => Spawn(t);

        public Transform SpawnSingleWithData(Vector3 pos, SpawnData spawnData)
        {
            GameObject go = Instantiate(spawnData.prefab, pos + spawnData.spawnOffset, spawnData.prefab.transform.rotation);

            Utils.GiveRandomUpwardsRotation(go.transform);
            if (go.TryGetComponent(out ISetupable setupable))
                setupable.Setup();

            return go.transform;
        }

        public Transform[] SpawnWithData(Vector3 pos, SpawnData spawnData)
        {
            Transform[] transforms = new Transform[spawnData.count];

            for (int i = 0; i < spawnData.count; i++)
            {
                transforms[i] = SpawnSingleWithData(pos, spawnData);
            }

            return transforms;
        }

        public Transform[] Spawn(Vector3 pos)
        {
            return SpawnWithData(pos, spawnData);
        }

        public Transform SpawnSingle(Vector3 pos)
        {
            return SpawnSingleWithData(pos, spawnData);
        }

        public Transform[] Spawn(SpawnData spawnData) => SpawnWithData(Vector3.zero, spawnData);
        public Transform SpawnSingle(SpawnData spawnData) => SpawnSingleWithData(Vector3.zero, spawnData);
    }
}