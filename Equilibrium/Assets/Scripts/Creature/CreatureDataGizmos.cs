using UnityEngine;

namespace Equilibrium
{
    public class CreatureDataGizmos : MonoBehaviour
    {
        [SerializeField] private CreatureData data = default;
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, data.eatingRange);
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, data.foodSeeingRange);
        }
    }
}