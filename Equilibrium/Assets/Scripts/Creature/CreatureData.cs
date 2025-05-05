using UnityEngine;

namespace OuterWilds
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
        [Min(0f)] public float dragValue;
        [Min(1)] public int factionsCount;
        [Min(1)] public int creatureSpawnCount;
        public float randomSpeedIncrease;
        public float deathPitHeight;
        [Min(0f)] public float chaseBias;
        [Min(0f)] public float runBias;
        [Min(0.02f)] public float aliveTimeWithoutFood;
        [Min(0.02f)] public float processInterval;

        /// <summary>
        /// prolly should move these to utils.
        /// </summary>
        public float GetSpeed(float speedOffset) 
        {
            return baseSpeed + speedOffset;
        }

        public void OnValidate()
        {
            //eatingRange = Mathf.Pow(Mathf.Pow(size + 0.1f, 2f) + Mathf.Pow(size + 0.1f, 2f), 0.5f) + 0.1f;
        }
    }
}