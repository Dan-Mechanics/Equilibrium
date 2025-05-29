using UnityEngine;

namespace Equilibrium
{
    /// <summary>
    /// AKA creature behaviour
    /// </summary>
    public class CreatureBehaviour : MonoBehaviour
    {
        private bool IsAwake => Utils.IsTime(awakeTime);

        [SerializeField] private CreatureData data = default;
        [SerializeField] private MeshRenderer rend = default;
        [SerializeField] private int firstLayerIndex = default;

        [SerializeField] private InspectorInterface<IWritable<Vector3>> idealVelocityWriter = default;
        [SerializeField] private InspectorInterface<IWritable<float>> fallingSpeedWriter = default;

        private float speed;
        private LayerMask foodMask;
        private LayerMask dangerMask;
        private int factionIndex;
        private float awakeTime;
        private Vector3 runVelocity;
        private Vector3 chaseVelocity;
        private ICreatureCallbacks creatureCallback;
        private float dieTime;
        private float speedMod;
        private Vector3 idealVelocity;
        private Utils.ClosestPair closest;

        public void Setup(ICreatureCallbacks creatureCallback)
        {
            idealVelocityWriter.Setup();
            fallingSpeedWriter.Setup();

            transform.localScale = Vector3.one * data.size;
            this.creatureCallback = creatureCallback;

            fallingSpeedWriter.attached.Write(data.fallingSpeed);
        }

        public void ProcessFixedFrame()
        {
            if (CheckDeath())
                return;

            if (TryFindClosestOfMask(foodMask, data.foodSeeingRange))
            {
                chaseVelocity = data.chaseBias * speed * Utils.Flatten(closest.transform.position - transform.position).normalized;
                TryEat();
            }

            if (TryFindClosestOfMask(dangerMask, data.dangerSeeingRange))
                runVelocity = data.runBias * speed * Utils.Flatten(transform.position - closest.transform.position).normalized;

            idealVelocity = chaseVelocity + runVelocity;
            idealVelocityWriter.attached.Write(idealVelocity);

            if (idealVelocity != Vector3.zero)
                transform.forward = idealVelocity;
        }

        private bool CheckDeath() 
        {
            // note: if it doesnt work, here is why:
            if (Utils.IsTime(dieTime))
            {
                // CHANGE CHANGE CHANGE HERE !!!
                /*Die();
                return true;*/

                if (Utils.RandomBool())
                {
                    Die();
                    return true;
                }
                else
                {
                    SetDieTime();
                }
            }

            if (transform.position.y <= data.deathPitHeight)
            {
                Die();
                return true;
            }

            return false;
        }

        private void TryEat()
        {
            if (!IsAwake)
                return;

            if (closest.distance > data.eatingRange)
                return;

            SetDieTime();

            closest.component.GetComponent<CreatureBehaviour>().ClaimByCreature(factionIndex);
            Refresh();
        }

        private bool TryFindClosestOfMask(LayerMask mask, float seeingRange)
        {
            int foundCount = Physics.OverlapSphereNonAlloc(transform.position, seeingRange, data.foundColliders, mask, QueryTriggerInteraction.Ignore);

            Utils.GetClosest(data.foundColliders, foundCount, transform.position, out closest);

            return foundCount > 0;
        }

        private void ClaimByCreature(int factionIndex)
        {
            Claim(factionIndex);
            creatureCallback.ClaimCallback(factionIndex);
        }

        public int ResetCreature(float mod)
        {
            this.speedMod = mod;

            int faction = Random.Range(0, data.factionsCount);
            Claim(faction);
            gameObject.SetActive(true);
            SetDieTime();

            return faction;
        }

        /// <summary>
        /// This should be called when we are eaten also.
        /// </summary>
        private void SetDieTime()
        {
            dieTime = Time.time + data.aliveTimeWithoutFood;
        }

        private void Claim(int factionIndex) 
        {
            this.factionIndex = factionIndex;
            rend.material = data.materials[factionIndex];

            gameObject.layer = firstLayerIndex + factionIndex;
            foodMask = 1 << (firstLayerIndex + Utils.WrapIndex(factionIndex, 1, data.materials.Length));
            dangerMask = 1 << (firstLayerIndex + Utils.WrapIndex(factionIndex, 2, data.materials.Length));

            Refresh();
        }

        private void Refresh()
        {
            speed = data.GetSpeed(speedMod);
            awakeTime = Time.time + data.asleepTime;

            // NEW NEW NEW.
            //SetDieTime();
        }

        /// <summary>
        /// https://discussions.unity.com/t/how-to-manually-pause-unpause-by-script/515260
        /// </summary>
        private void Die() 
        {
            gameObject.SetActive(false);
            creatureCallback.DieCallback(factionIndex);
        }
    }
}