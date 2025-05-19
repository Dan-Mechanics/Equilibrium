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
        public float randomSpeedIncrease;
        public float deathPitHeight;
        [Min(0f)] public float chaseBias;
        [Min(0f)] public float runBias;
        [Min(0.02f)] public float aliveTimeWithoutFood;
        [Min(0.02f)] public float processInterval;

        public float GetSpeed(Biome biome) 
        {
            float speed = baseSpeed + Random.value * randomSpeedIncrease;

            // Or something idk.
            switch (biome)
            {
                case Biome.Mesa:
                    speed *= 0.75f;
                    break;
                case Biome.Icey:
                    speed *= 1.25f;
                    break;
                case Biome.Serene:
                    break;
                default:
                    break;
            }

            return speed;
        }
    }
}