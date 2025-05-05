using UnityEngine;

namespace Equilibrium 
{
    /// <summary>
    /// You could add spread and flatten here but thats not required for this game really.
    /// </summary>
    [System.Serializable]
    public struct SpawnData
    {
        public GameObject prefab;
        [Min(1)] public int count;
        public Vector3 spawnOffset;
        public bool randomRot;
    }
}