using UnityEngine;

namespace Equilibrium
{
    public class Creature : MonoBehaviour
    {
        private bool IsAwake => Utils.IsTime(awakeTime);

        [SerializeField] private CreatureData data = default;
        [SerializeField] private MeshRenderer rend = default;
        [SerializeField] private int firstLayerIndex = default;
        [SerializeField] private InspectorInterface<IWritable<Vector3>> idealVelocityWriter = default;
        [SerializeField] private InspectorInterface<IWritable<float>> fallingSpeedWriter = default;
        [SerializeField] private Material[] materials = default;

        private float speedOffset;
        private LayerMask foodMask;
        private LayerMask dangerMask;
        private int factionIndex;
        private float awakeTime;
        private Vector3 runVelocity;
        private Vector3 chaseVelocity;
        private IDieCallback dieCallback;
        private IClaimCallback claimCallback;
        private float dieTime;
        private Biome biome;

        public void Setup(IDieCallback dieCallback, IClaimCallback claimCallback)
        {
            idealVelocityWriter.Setup();
            fallingSpeedWriter.Setup();

            transform.localScale = Vector3.one * data.size;
            this.dieCallback = dieCallback;
            this.claimCallback = claimCallback;

            fallingSpeedWriter.attached.Write(data.fallingSpeed);

            if (materials.Length != data.factionsCount)
                Debug.LogError("if(materials.Length != data.factionsCount)");
        }

        public void ProcessFixedFrame()
        {
            if (CheckDeath())
                return;

            if (TryFindClosestOfMask(foodMask, data.foodSeeingRange, out Utils.ClosestPair closestFood))
            {
                chaseVelocity = data.chaseBias * data.GetSpeed(speedOffset) * Utils.Flatten(closestFood.transform.position - transform.position).normalized;
                TryEat(ref closestFood);
            }

            if (TryFindClosestOfMask(dangerMask, data.dangerSeeingRange, out Utils.ClosestPair closestDanger))
                runVelocity = data.runBias * data.GetSpeed(speedOffset) * Utils.Flatten(transform.position - closestDanger.transform.position).normalized;

            // NEW NEW N NEWENWE E !!!
            /*if (idealVelocityWriter.attached == null)
                return;*/

            Vector3 idealVelocity = chaseVelocity + runVelocity;

            /*switch (biome)
            {
                case Biome.Mesa:
                    idealVelocity *= 0.5f;
                    break;
                case Biome.Icey:
                    idealVelocity *= 1.5f;
                    break;
                case Biome.Serene:
                    break;
                default:
                    break;
            }*/

            idealVelocityWriter.attached.Write(idealVelocity);

            if (idealVelocity != Vector3.zero)
                transform.forward = idealVelocity;
        }

        private bool CheckDeath() 
        {
            // note: if it doesnt work, here is why:
            if (Utils.IsTime(dieTime))
            {
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
            Claim(factionIndex);
            claimCallback.ClaimCallback(factionIndex);
        }

        public int ResetCreature(Biome biome)
        {
            int faction = Random.Range(0, data.factionsCount);
            Claim(faction);
            gameObject.SetActive(true);
            SetDieTime();
            this.biome = biome;

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