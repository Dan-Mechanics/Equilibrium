using UnityEngine;

namespace Equilibrium
{
    /// <summary>
    /// Note: use interfaces for beter performance prolly.
    /// 
    /// Improvements:
    /// interfaces 
    /// proper object pooling system ish
    /// maybe working with spawner is a little more intuative but its fine.
    /// 
    /// Tally update is a little better.
    /// 
    /// 
    /// YOU COULd perchance implemenet something which limits the vertical speed of the thing.
    /// </summary>
    public class Creature : MonoBehaviour
    {
        private bool IsAwake => Utils.IsTime(awakeTime);

        [SerializeField] private CreatureData data = default;
        [SerializeField] private MeshRenderer rend = default;
        [SerializeField] private int firstLayerIndex = default;
        [SerializeField] private InspectorInterface<IWritable<Vector3>> idealVelocityWriter = default;
        [SerializeField] private InspectorInterface<IWritable<float>> speedWriter = default;
        [SerializeField] private Material[] materials = default;
        // [SerializeField] private bool updateRotation = default;

        private float speedOffset;
        private LayerMask foodMask;
        private LayerMask dangerMask;
        private int factionIndex;
        private float awakeTime;
        private Vector3 runVelocity;
        private Vector3 chaseVelocity;
        //private FixedTicks fixedTicks;
        private IDieCallback dieCallback;
        private IClaimCallback claimCallback;
        private float dieTime;

        /// <summary>
        /// USE INTERFACE !!!
        /// </summary>
        /// <param name="handler"></param>
        public void Setup(IDieCallback dieCallback, IClaimCallback claimCallback)
        {
            idealVelocityWriter.Setup();
            speedWriter.Setup();

            //fixedTicks = new FixedTicks(data.processInterval);
            transform.localScale = Vector3.one * data.size;
            this.dieCallback = dieCallback;
            this.claimCallback = claimCallback;

            speedWriter.attached.Write(data.dragValue);

            if (materials.Length != data.factionsCount)
                Debug.LogError("if(materials.Length != data.factionsCount)");
        }

        public void ProcessFixedFrame()
        {
            // note: if it doesnt work, here is why:
            if (Utils.IsTime(dieTime)) 
            {
                if (Utils.RandomBool())
                {
                    Die();
                    return;
                }
                else
                {
                    SetDieTime();
                }
            }
            
            if (transform.position.y <= data.deathPitHeight)
            {
                Die();
                return;
            }

            if (TryFindClosestOfMask(foodMask, data.foodSeeingRange, out Utils.ClosestPair closestFood))
            {
                chaseVelocity = data.chaseBias * data.GetSpeed(speedOffset) * Utils.Flatten(closestFood.transform.position - transform.position).normalized;
                TryEat(ref closestFood);
            }

            if (TryFindClosestOfMask(dangerMask, data.dangerSeeingRange, out Utils.ClosestPair closestDanger))
            {
                runVelocity = data.runBias * data.GetSpeed(speedOffset) * Utils.Flatten(transform.position - closestDanger.transform.position).normalized;
            }

            // NEW NEW N NEWENWE E !!!
            if (idealVelocityWriter.attached == null)
                return;

            Vector3 idealVelocity = chaseVelocity + runVelocity;
            idealVelocityWriter.attached.Write(idealVelocity);

            if (idealVelocity != Vector3.zero)
                transform.forward = idealVelocity;
        }

        private void TryEat(ref Utils.ClosestPair closestFood)
        {
            if (!IsAwake)
                return;

            if (closestFood.distance > data.eatingRange)
                return;

            SetDieTime();

            closestFood.component.GetComponent<Creature>().ClaimByCreature(factionIndex);
            MakeAsleep();
        }

        private bool TryFindClosestOfMask(LayerMask mask, float seeingRange, out Utils.ClosestPair closest)
        {
            closest = null;

            // Garbage collector GOOO !!
            Collider[] found = Physics.OverlapSphere(transform.position, seeingRange, mask, QueryTriggerInteraction.Ignore);

            if (found.Length <= 0)
                return false;

            closest = Utils.GetClosest(found, transform.position);

            return true;
        }

        private void ClaimByCreature(int factionIndex)
        {
            /*if (!IsAwake)
                return;*/

            Claim(factionIndex);

            claimCallback.ClaimCallback(factionIndex);
        }

        public int ResetCreature()
        {
            int faction = Random.Range(0, data.factionsCount);
            Claim(faction);
            gameObject.SetActive(true);
            SetDieTime();

            return faction;
        }

        private void SetDieTime()
        {
            dieTime = Time.time + data.aliveTimeWithoutFood;
        }

        private void Claim(int factionIndex) 
        {
            this.factionIndex = factionIndex;
            rend.material = materials[factionIndex];

            gameObject.layer = firstLayerIndex + factionIndex;
            foodMask = 1 << (firstLayerIndex + Utils.WrapIndex(factionIndex, 1, materials.Length));
            dangerMask = 1 << (firstLayerIndex + Utils.WrapIndex(factionIndex, 2, materials.Length));

            MakeAsleep();
        }

        private void MakeAsleep()
        {
            speedOffset = Random.value * data.randomSpeedIncrease;
            //speedWriter.attached.Write(data.dragValue);
            awakeTime = Time.time + data.asleepTime;
        }

        /// <summary>
        /// https://discussions.unity.com/t/how-to-manually-pause-unpause-by-script/515260
        /// </summary>
        private void Die() 
        {
            gameObject.SetActive(false);
            dieCallback.DieCallback(factionIndex);
        }
    }
}