using UnityEngine;

namespace Equilibrium
{
    public class Mover : MonoBehaviour, IWritable<Vector3>, IWritable<float>
    {
        [SerializeField] private Rigidbody rb = default;

        private Vector3 idealVelocity;
        private float dragValue;

        private void Start()
        {
            rb.sleepThreshold = 0f;
        }

        public void Write(Vector3 velocity)
        {
            idealVelocity = velocity;
        }

        public void Write(float dragValue) 
        {
            this.dragValue = dragValue;
        }

        private void FixedUpdate()
        {
            /*if (idealVelocity == Vector3.zero)
                return;*/

            rb.AddForce(idealVelocity - Utils.Flatten(rb.velocity, rb.velocity.y / dragValue), ForceMode.VelocityChange);
        }

        private void OnEnable()
        {
            rb.velocity = Vector3.zero;
        }
    }
}