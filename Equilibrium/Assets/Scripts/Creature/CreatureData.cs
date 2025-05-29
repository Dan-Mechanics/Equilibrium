using UnityEngine;

namespace Equilibrium
{
    [CreateAssetMenu(menuName = "CreatureData")]
    public class CreatureData : ScriptableObject
    {
        [Min(0f)] public float baseSpeed;
        [Min(0f)] public float foodSeeingRange;
        [Min(0f)] public float dangerSeeingRange;
        [Min(0f)] public float eatingRange;
        [Min(0f)] public float asleepTime;
        [Min(0f)] public float size;
        [Min(1f)] public float fallingSpeed;
        [Min(1)] public int factionsCount;
        [Min(1)] public int creatureSpawnCount;
        [Min(1)] public int creatureSearchBufferSize;
        public float randomSpeedIncrease;
        public float deathPitHeight;
        [Min(0f)] public float chaseBias;
        [Min(0f)] public float runBias;
        [Min(0.02f)] public float aliveTimeWithoutFood;
        [Min(0.02f)] public float processInterval;
        public Material[] materials;

        /// <summary>
        /// Gotta check if this works in build tho.
        /// </summary>
        [HideInInspector] public Collider[] foundColliders;

        /// <summary>
        /// Consider removing this and just giving the environment a base speed thing.
        /// </summary>
        public float GetSpeed(float speedMod) 
        {
            float speed = baseSpeed + Random.value * randomSpeedIncrease;
            speed *= speedMod;

            return speed;
        }
    }
}