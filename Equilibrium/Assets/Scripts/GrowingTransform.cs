using UnityEngine;

namespace Equilibrium
{
    public class GrowingTransform : MonoBehaviour
    {
        [SerializeField] private Transform growTarget = default;
        //[SerializeField] private float growTargetDestroyLead = default;
        //[SerializeField] private float highlightTime = default;

        private float startTime;
        [SerializeField] private Vector3 scale = default;
        [SerializeField] private float growTime = default;
       // private float startHighlightTime;

        private void Start()
        {
            Destroy(this.growTarget.gameObject, this.growTime);
            startTime = Time.time;
        }

        private void FixedUpdate()
        {
            if (this.growTarget == null)
                return;
            
            float lerpValue = (Time.time - this.startTime) / this.growTime;
            this.growTarget.localScale = Vector3.Lerp(Vector3.zero, scale, lerpValue);
        }
    }
}