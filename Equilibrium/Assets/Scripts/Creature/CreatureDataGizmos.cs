using UnityEngine;

namespace Equilibrium
{
    public class CreatureDataGizmos : MonoBehaviour
    {
        [SerializeField] private CreatureSettings settings = default;
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, settings.eatingRange);
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, settings.foodSeeingRange);
        }
    }
}